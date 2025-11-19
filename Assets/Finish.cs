using UnityEngine;

public class HammerNailInteraction : MonoBehaviour
{
    [Header("References")]
    public ParticleSystem waterParticles;
    public string nailTag = "Nail";

    [Header("Hit Settings")]
    public float velocityThreshold = 0.5f;
    public int hitsToStopParticles = 3;
    public float hitCooldown = 0.1f;

    [Header("Nail Settings")]
    public float nailMoveDistance = 0.01f;
    public float maxNailDepth = 0.03f;

    private VRVelocityTracker velocityTracker;
    private float lastHitTime = 0f;

    private int goodHits = 0;
    private float currentDepth = 0f;

    void Start()
    {
        velocityTracker = GetComponent<VRVelocityTracker>();
        if (velocityTracker == null)
            Debug.LogWarning("VRVelocityTracker missing on hammer!");
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag(nailTag))
            return;

        // cooldown between hits
        if (Time.time - lastHitTime < hitCooldown)
            return;

        lastHitTime = Time.time;

        float impactSpeed = velocityTracker.Velocity.magnitude;

        if (impactSpeed >= velocityThreshold && currentDepth < maxNailDepth)
        {
            goodHits++;
            Debug.Log($"Good hit #{goodHits}! Speed: {impactSpeed:F2}");

            MoveNail(other.gameObject);

            if (goodHits >= hitsToStopParticles)
                StopWaterParticles();
        }
    }

    private void MoveNail(GameObject nailPart)
    {
        Transform nailRoot = nailPart.transform.root;

        // 1. Calculate remaining distance
        float remaining = maxNailDepth - currentDepth;
        if (remaining <= 0f) return; // nail fully hammered

        // 2. Move nail by step
        float moveStep = Mathf.Min(nailMoveDistance, remaining);
        currentDepth += moveStep;

        nailRoot.position += nailRoot.forward * moveStep; // move into wood

        Debug.Log($"Moved nail {nailRoot.name} by {moveStep}. Current depth: {currentDepth}");

        // 3. Check for WoodPlacement and stop water
        WoodPlacement wood = nailRoot.GetComponentInParent<WoodPlacement>();
        if (wood != null && goodHits >= hitsToStopParticles)
        {
            wood.StopWaterUnderneath();
            Debug.Log("Stopped water under this wood!");
        }
    }



    private void StopWaterParticles()
    {
        if (waterParticles != null && waterParticles.isPlaying)
        {
            waterParticles.Stop();
            Debug.Log("Water particle system stopped after max hits!");
        }
    }
}
