using UnityEngine;

public class ChestDisappear : MonoBehaviour
{
    [Header("References")]
    public GameObject keyObject;          // The key GameObject (drag in from Inspector)
    public Transform keySpot;             // The target spot near the chest
    public float activationDistance = 0.5f;  // How close the key must be to trigger the chest
    public GameObject chest;              // The chest GameObject to make disappear

    private bool chestGone = false;

    void Update()
    {
        if (chestGone) return;

        // Check distance between key and target spot
        float distance = Vector3.Distance(keyObject.transform.position, keySpot.position);

        // If key is close enough, make chest disappear
        if (distance <= activationDistance)
        {
            chestGone = true;
            MakeChestDisappear();
        }
    }

    void MakeChestDisappear()
    {
        if (chest != null)
        {
            // You can either deactivate or destroy it
            // Option 1: make it invisible
            chest.SetActive(false);

            // Option 2: remove it completely
            // Destroy(chest);
        }

        Debug.Log("Chest disappeared!");
    }
}