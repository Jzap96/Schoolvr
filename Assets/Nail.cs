using UnityEngine;

public class Nail : MonoBehaviour
{
    [Header("Water Particle for this Nail")]
    public ParticleSystem waterParticle;

    [Header("Nail Settings")]
    public float maxDepth = 0.03f;
    public float moveStep = 0.01f;
    public int hitsToStopParticle = 3;

    [HideInInspector] public int currentHits = 0;
    [HideInInspector] public float currentDepth = 0f;

    public void Hit()
    {
        if (currentDepth >= maxDepth) return;

        currentHits++;
        float remaining = maxDepth - currentDepth;
        float step = Mathf.Min(moveStep, remaining);
        currentDepth += step;

        transform.root.position += transform.root.forward * step;

        if (currentHits >= hitsToStopParticle)
        {
            StopParticle();
        }

        Debug.Log($"Hit {name}: depth={currentDepth:F3}, hits={currentHits}");
    }

    private void StopParticle()
    {
        if (waterParticle != null && waterParticle.isPlaying)
        {
            waterParticle.Stop();
            Debug.Log($"Stopped water particle for {name}");
        }
    }
}
