using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SendHeldObjectToSocket : MonoBehaviour
{
    [Header("References")]
    public XRDirectInteractor handInteractor;
    public XRSocketInteractor targetSocket;

    [Header("Input Action")]
    public InputActionProperty secondaryButtonAction; // Bind to /input/secondaryButton

    private XRInteractionManager interactionManager;

    private void Start()
    {
        if (handInteractor != null)
            interactionManager = handInteractor.interactionManager;
    }

    private void OnEnable()
    {
        if (secondaryButtonAction != null)
            secondaryButtonAction.action.performed += OnSecondaryButtonPressed;
    }

    private void OnDisable()
    {
        if (secondaryButtonAction != null)
            secondaryButtonAction.action.performed -= OnSecondaryButtonPressed;
    }

    private void OnSecondaryButtonPressed(InputAction.CallbackContext ctx)
    {
        if (handInteractor == null || targetSocket == null || interactionManager == null)
            return;

        // Is hand holding something?
        if (!handInteractor.hasSelection)
            return;

        var heldInteractable = handInteractor.firstInteractableSelected as XRBaseInteractable;
        if (heldInteractable == null)
            return;

        // Release the object
        interactionManager.SelectExit(handInteractor, heldInteractable);

        // Move object to socket's attach transform
        Transform attachTransform = targetSocket.attachTransform != null ? targetSocket.attachTransform : targetSocket.transform;
        heldInteractable.transform.SetPositionAndRotation(attachTransform.position, attachTransform.rotation);

        // Force socket to grab the object
        interactionManager.SelectEnter(targetSocket, heldInteractable);

        Debug.Log($"[SendHeldObjectToSocket] Moved {heldInteractable.name} to {targetSocket.name}");
    }
}