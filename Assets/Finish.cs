using UnityEngine;
using UnityEngine.UI; // Needed for UI

public class HammerStopWater : MonoBehaviour
{
    [Header("Settings")]
    public string waterParticleTag = "WaterParticle";
    public float velocityThreshold = 0.5f;
    public float hitCooldown = 0.1f;

    [Header("UI")]
    public GameObject victoryUI; // Assign your "Congratulations" panel here

    private VRVelocityTracker velocityTracker;
    private float lastHitTime = 0f;

    void Start()
    {
        velocityTracker = GetComponent<VRVelocityTracker>();
        if (velocityTracker == null)
            Debug.LogWarning("VRVelocityTracker missing on hammer!");

        // Hide UI at start
        if (victoryUI != null)
            victoryUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Cooldown check
        if (Time.time - lastHitTime < hitCooldown) return;
        lastHitTime = Time.time;

        float impactSpeed = velocityTracker != null ? velocityTracker.Velocity.magnitude : 0f;
        if (impactSpeed < velocityThreshold) return;

        // Only act on water particles
        if (other.CompareTag(waterParticleTag))
        {
            ParticleSystem ps = other.GetComponent<ParticleSystem>();
            if (ps != null && ps.isPlaying)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                Debug.Log($"Stopped water particle: {ps.name}");
            }

            // Check if all water particles are stopped
            CheckVictoryCondition();
        }
    }

    private void CheckVictoryCondition()
    {
        // Find all active water particles in the scene
        GameObject[] waterObjects = GameObject.FindGameObjectsWithTag(waterParticleTag);
        foreach (GameObject obj in waterObjects)
        {
            ParticleSystem ps = obj.GetComponent<ParticleSystem>();
            if (ps != null && ps.isPlaying)
            {
                return; // At least one particle is still active
            }
        }

        // If we reach here, all particles are stopped
        if (victoryUI != null)
        {
            victoryUI.SetActive(true);
            Debug.Log("Congratulations! You saved your boat!");
        }
    }
}
