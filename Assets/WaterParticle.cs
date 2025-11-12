using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [Header("Particle Settings")]
    public ParticleSystem particleSystemToActivate;
    public float delay = 3f;

    [Header("Gameplay Objects")]
    public string woodTag = "Wood";
    public string hammerTag = "Hammer";

    private bool woodPlaced = false;
    private bool nailsHit = false;

    private void Start()
    {
        StartCoroutine(ActivateParticleAfterDelay());
    }

    private System.Collections.IEnumerator ActivateParticleAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        if (particleSystemToActivate != null)
            particleSystemToActivate.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detect wood placement
        if (other.CompareTag(woodTag))
        {
            woodPlaced = true;
            Debug.Log("Wood placed in area!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Detect hammer hits (nailing action)
        if (collision.gameObject.CompareTag(hammerTag))
        {
            nailsHit = true;
            Debug.Log("Hammer hit detected!");
        }

        // When both conditions met, stop the particles
        if (woodPlaced && nailsHit)
        {
            TurnOffParticles();
        }
    }

    private void TurnOffParticles()
    {
        if (particleSystemToActivate != null && particleSystemToActivate.isPlaying)
        {
            particleSystemToActivate.Stop();
            Debug.Log("Particles turned off after successful wood + hammer action!");
        }
    }
}
