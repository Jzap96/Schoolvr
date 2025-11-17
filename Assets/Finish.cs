using UnityEngine;

public class HammerNailInteraction : MonoBehaviour
{
    [Header("References")]
    public ParticleSystem waterParticles;   // Water particle system to stop
    public string nailTag = "Nail";         // Tag of the nail object

    [Header("Hit Settings")]
    public float velocityThreshold = 0f;  // Minimum impact speed to count as a “good hit”
    public int hitsToStopParticles = 3;     // Number of good hits required

    [Header("Nail Settings")]
    public float nailMoveDistance = 0.05f; // How much the nail moves per hit

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
        Vector3 impactVelocity = collision.relativeVelocity;
        float impactSpeed = impactVelocity.magnitude;

        if (impactSpeed >= velocityThreshold)
        {
            goodHits++;
            Debug.Log($"Good hit #{goodHits}! Velocity: {impactSpeed:F2}");

            // Move the nail slightly forward into the wood
            MoveNail(collision.gameObject);

            if (goodHits >= hitsToStopParticles)
            {
                StopWaterParticles();
            }
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

    private void MoveNail(GameObject nail)
    {
        float moveDistance = 1.0f; // adjust for each hit
    nail.transform.position += nail.transform.forward * moveDistance;
    }

}
