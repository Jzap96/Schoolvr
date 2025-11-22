using UnityEngine;
using UnityEngine.InputSystem;

public class XRMovementController : MonoBehaviour
{
    [Header("XR Rig")]
    public Transform xrRig;          // XR Rig root
    public Transform head;           // XR Camera / center eye (for reference, optional)

    [Header("Input")]
    public InputActionReference turnAction;         // Vector2 stick for snap turn
    public InputActionReference crouchPrimaryButton; // Primary button for crouch
    public InputActionReference crouchSecondaryButton; // Secondary button for crouch (optional)

    [Header("Snap Turn Settings")]
    public float snapAngle = 45f;
    public float inputThreshold = 0.8f;

    [Header("Crouch Settings")]
    public float crouchHeight = 0.5f; // How much to lower XR Rig for crouch

    // Internal state
    private bool hasSnappedThisPress = false;
    private bool isCrouching = false;
    private Vector3 xrRigOriginalPos;

    private void OnEnable()
    {
        if (turnAction != null) turnAction.action.Enable();
        if (crouchPrimaryButton != null)
        {
            crouchPrimaryButton.action.Enable();
            crouchPrimaryButton.action.performed += OnCrouchPressed;
        }
        if (crouchSecondaryButton != null)
        {
            crouchSecondaryButton.action.Enable();
            crouchSecondaryButton.action.performed += OnCrouchPressed;
        }
    }

    private void OnDisable()
    {
        if (turnAction != null) turnAction.action.Disable();
        if (crouchPrimaryButton != null)
        {
            crouchPrimaryButton.action.performed -= OnCrouchPressed;
            crouchPrimaryButton.action.Disable();
        }
        if (crouchSecondaryButton != null)
        {
            crouchSecondaryButton.action.performed -= OnCrouchPressed;
            crouchSecondaryButton.action.Disable();
        }
    }

    private void Start()
    {
        if (xrRig != null)
            xrRigOriginalPos = xrRig.localPosition;
    }

    private void Update()
    {
        HandleSnapTurn();
    }

    // Snap Turn Logic
    private void HandleSnapTurn()
    {
        if (turnAction == null || xrRig == null) return;

        Vector2 input = turnAction.action.ReadValue<Vector2>();
        float horizontal = input.x;

        if (Mathf.Abs(horizontal) > inputThreshold)
        {
            if (!hasSnappedThisPress)
            {
                float direction = Mathf.Sign(horizontal);
                xrRig.Rotate(0f, snapAngle * direction, 0f);
                hasSnappedThisPress = true;
            }
        }
        else
        {
            hasSnappedThisPress = false;
        }
    }

    // Crouch toggle logic
    private void OnCrouchPressed(InputAction.CallbackContext context)
    {
        if (xrRig == null) return;

        isCrouching = !isCrouching;

        Vector3 currentPos = xrRig.localPosition;

        if (isCrouching)
            currentPos.y -= Mathf.Abs(crouchHeight);
        else
            currentPos.y += Mathf.Abs(crouchHeight);

        xrRig.localPosition = currentPos;
    }
}
