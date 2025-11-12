using UnityEngine;

public class HammerNailInteraction : MonoBehaviour
{
    [Header("References")]
    public ParticleSystem waterParticles;   // Water particle system to stop
    public string nailTag = "Nail";         // Tag of the nail object

    [Header("Hit Settings")]
    public float velocityThreshold = 1.5f;  // Minimum impact speed to count as a “good hit”
    public int hitsToStopParticles = 3;     // Number of good hits required

    private Rigidbody rb;                   // Hammer’s Rigidbody
    private int goodHits = 0;               // Counter for successful hits

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("No Rigidbody found on hammer!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Only count hits on the nail
        if (!collision.gameObject.CompareTag(nailTag))
            return;

        // Measure how fast the hammer was moving on impact
        float impactSpeed = rb.velocity.magnitude;

        if (impactSpeed >= velocityThreshold)
        {
            goodHits++;
            Debug.Log($"Good hit #{goodHits}! Velocity: {impactSpeed:F2}");

            if (goodHits >= hitsToStopParticles)
            {
                StopWaterParticles();
            }
        }
        else
        {
            Debug.Log($"Weak hit ignored (velocity: {impactSpeed:F2})");
        }
    }

    private void StopWaterParticles()
    {
        if (waterParticles != null && waterParticles.isPlaying)
        {
            waterParticles.Stop();
            Debug.Log("Water particle system stopped after 3 good hits!");
        }
    }
}
