using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    public void Interact();
}

[RequireComponent(typeof(PlayerInput))]
public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;

    private InputAction interactAction;

    void Start()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        InputActionMap playerActions = playerInput.actions.FindActionMap("Player", true);
        interactAction = playerActions.FindAction("Interact", true);
        playerActions.Enable();
    }

    void Update()
    {
        if (interactAction.WasPressedThisFrame())
        {
            Ray r = new(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }
}