using UnityEngine;

public class WoodPlacement : MonoBehaviour
{
    private ParticleSystem[] waterUnderneath;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            // Get all ParticleSystems under the water object, including children
            waterUnderneath = other.GetComponentsInChildren<ParticleSystem>(true);
            Debug.Log($"Wood placed on water: {other.name}, found {waterUnderneath.Length} particle systems");
        }
    }

    public void StopWaterUnderneath()
    {
        if (waterUnderneath == null || waterUnderneath.Length == 0)
        {
            Debug.Log("No water to stop!");
            return;
        }

        foreach (ParticleSystem ps in waterUnderneath)
        {
            if (ps.isPlaying)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                Debug.Log($"Stopped water particle: {ps.name}");
            }
        }
    }
}
