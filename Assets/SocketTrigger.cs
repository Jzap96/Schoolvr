using UnityEngine;

public class SocketTrigger : MonoBehaviour
{
    [Header("Object Requirements")]
    public string requiredObjectTag = "Bottle";   // Tag on the object that must be inserted

    [Header("Animation")]
    public Animator targetAnimator;                 // Animator to trigger
    public string animationTriggerName = "Play";    // Trigger parameter in the Animator

    private void OnTriggerEnter(Collider other)
    {
        // Check if the correct object entered the socket
        if (other.CompareTag(requiredObjectTag))
        {
            Debug.Log("Correct object inserted!");

            if (targetAnimator != null)
            {
                targetAnimator.SetTrigger(animationTriggerName);
            }
        }
    }
}
