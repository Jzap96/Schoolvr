using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SawInteraction : MonoBehaviour
{
    public GameObject objectToSpawn; // e.g. WoodLog prefab
    private XRGrabInteractable grabInteractable;
    private bool isHeld = false;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDestroy()
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

    private void OnCollisionEnter(Collision collision)
    {
        if (!isHeld) return; // Only works if player is holding the saw

        if (collision.gameObject.CompareTag("Tree"))
        {
            // Store position & rotation before destroying
            Vector3 spawnPos = collision.transform.position;
            Quaternion spawnRot = collision.transform.rotation;

            // Destroy the tree
            Destroy(collision.gameObject);

            // Spawn the new object
            if (objectToSpawn != null)
            {
                Instantiate(objectToSpawn, spawnPos, spawnRot);
            }
        }
    }
}
