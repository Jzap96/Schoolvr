using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SawInteraction : MonoBehaviour
{
    [Header("Objects to activate when cutting the CORRECT mast")]
    public GameObject[] correctReplacementObjects; // <-- Changed to an array

    [Header("Animator to play when correct mast is cut")]
    public Animator animationToPlay;
    public string animationTriggerName = "PlayCutAnimation";

    [Header("Tag for the correct mast")]
    public string correctMastTag = "CorrectMast";

    [Header("Tag for a wrong mast")]
    public string wrongMastTag = "WrongMast";

    [Header("Game Over UI")]
    public GameObject gameOverUI;

    [Header("Sound Effects")]
    public AudioSource audioSource;
    public AudioClip correctCutSFX;
    public AudioClip wrongCutSFX;

    private XRGrabInteractable grabInteractable;
    private bool isHeld = false;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);

        if (gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        isHeld = true;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isHeld) return;

        // -----------------------------------------
        // ✔ PLAYER CUT THE CORRECT MAST
        // -----------------------------------------
        if (other.CompareTag(correctMastTag))
        {
            Debug.Log("Correct mast cut!");

            // Play correct SFX
            if (audioSource != null && correctCutSFX != null)
                audioSource.PlayOneShot(correctCutSFX);

            // Activate all replacement objects
            if (correctReplacementObjects != null)
            {
                foreach (GameObject obj in correctReplacementObjects)
                {
                    if (obj != null)
                        obj.SetActive(true);
                }
            }

            if (animationToPlay != null)
                animationToPlay.SetTrigger(animationTriggerName);

            other.gameObject.SetActive(false);
            return;
        }

        // -----------------------------------------
        // ✖ PLAYER CUT THE WRONG MAST
        // -----------------------------------------
        if (other.CompareTag(wrongMastTag))
        {
            Debug.Log("Wrong mast cut! Player loses.");

            // Play wrong SFX
            if (audioSource != null && wrongCutSFX != null)
                audioSource.PlayOneShot(wrongCutSFX);

            LoseGame();
        }
    }

    // --------------------------
    // LOSS LOGIC
    // --------------------------
    private void LoseGame()
    {
        Debug.Log("GAME OVER!");

        if (gameOverUI != null)
            gameOverUI.SetActive(true);
    }
}
