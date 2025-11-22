using UnityEngine;

public class TriggerMultipleCharacters : MonoBehaviour
{
    [Header("Characters to Animate")]
    public Animator[] characterAnimators; // Assign all character animators here
    public string triggerName = "Play";   // Trigger parameter in Animator

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bottle"))
        {
            // Loop through all assigned animators and trigger the animation
            foreach (Animator animator in characterAnimators)
            {
                if (animator != null)
                    animator.SetTrigger(triggerName);
            }

            Debug.Log("Triggered animation on all characters!");
        }
    }
}
