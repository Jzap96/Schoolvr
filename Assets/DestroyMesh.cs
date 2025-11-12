using System.Collections;
using UnityEngine;

public class DestroyMesh : MonoBehaviour
{
    public GameObject key;

    void Start()
    {
            key.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.name);

        if (other.CompareTag("Bottle"))
        {
            Debug.Log("Bottle trigger detected!");
           
                key.SetActive(true);
        }
    }
}
