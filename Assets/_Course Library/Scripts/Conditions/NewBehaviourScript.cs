using UnityEngine;
using UnityEngine.InputSystem;

public class TestInput : MonoBehaviour
{
    public InputActionProperty secondaryButton;

    private void OnEnable()
    {
        secondaryButton.action.performed += ctx => Debug.Log("✅ Secondary button pressed!");
    }

    private void OnDisable()
    {
        secondaryButton.action.performed -= ctx => Debug.Log("✅ Secondary button pressed!");
    }
}