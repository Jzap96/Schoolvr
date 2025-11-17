using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SawInteraction : MonoBehaviour
{
    [Header("Object to activate when cutting the CORRECT mast")]
    public GameObject correctReplacementObject;

    [Header("Animator to play when correct mast is cut")]
    public Animator animationToPlay;
    public string animationTriggerName = "PlayCutAnimation";

    [Header("Tag for the correct mast")]
    public string correctMastTag = "CorrectMast";

    [Header("Tag for a wrong mast")]
    public string wrongMastTag = "WrongMast";

    [Header("Game Over UI")]
    public GameObject gameOverUI;  // <-- UI that will become visible on loss

    private XRGrabInteractable grabInteractable;
    private bool isHeld = false;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
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

            if (correctReplacementObject != null)
                correctReplacementObject.SetActive(true);

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
            LoseGame();
        }
    }

    // --------------------------
    // LOSS LOGIC (UI ONLY)
    // --------------------------
    private void LoseGame()
    {
        Debug.Log("GAME OVER!");

        // 1. Turn on UI
        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        // ✔ Player can still move & interact.
        // Nothing else is disabled.
    }
}
