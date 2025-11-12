using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SawInteraction : MonoBehaviour
{
    [Header("What object should appear after cutting?")]
    public GameObject objectToSpawn; // Prefab to spawn (e.g., WoodLog)

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
        // Only works if player is holding the saw
        if (!isHeld) return;

        // Check if it’s the correct object
        if (other.CompareTag("Tree"))
        {
            Vector3 spawnPos = other.transform.position;
            Quaternion spawnRot = other.transform.rotation;

            Destroy(other.gameObject);

            if (objectToSpawn != null)
            {
                Instantiate(objectToSpawn, spawnPos, spawnRot);
            }

            Debug.Log("Tree cut and replaced!");
        }
    }
}
