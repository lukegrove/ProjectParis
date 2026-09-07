using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class RTSCameraController : MonoBehaviour
{
    public static RTSCameraController instance;

    // If we want to select an item to follow, inside the item script add:
    // public void OnMouseDown(){
    //   CameraController.instance.followTransform = transform;
    // }

    [Header("General")]
    [SerializeField] Transform cameraTransform;
    public Transform followTransform;
    Vector3 newPosition;
    Vector3 dragStartPosition;
    Vector3 dragCurrentPosition;

    [Header("Optional Functionality")]
    [SerializeField] bool moveWithKeyboad;
    [SerializeField] bool moveWithEdgeScrolling;
    [SerializeField] bool moveWithMouseDrag;

    [Header("Keyboard Movement")]
    [SerializeField] float fastSpeed = 0.05f;
    [SerializeField] float normalSpeed = 0.01f;
    [SerializeField] float movementSensitivity = 1f; // Hardcoded Sensitivity
    float movementSpeed;

    [Header("Edge Scrolling Movement")]
    [SerializeField] float edgeSize = 50f;
    bool isCursorSet = false;
    public Texture2D cursorArrowUp;
    public Texture2D cursorArrowDown;
    public Texture2D cursorArrowLeft;
    public Texture2D cursorArrowRight;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private Keyboard keyboard;
    private Mouse mouse;

    CursorArrow currentCursor = CursorArrow.DEFAULT;
    enum CursorArrow
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        DEFAULT
    }

    private void Start()
    {
        instance = this;

        newPosition = transform.position;
        movementSpeed = normalSpeed;

        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            InputActionMap playerActions = playerInput.actions.FindActionMap("Player", true);
            if (playerActions != null)
            {
                moveAction = playerActions.FindAction("Move", true);
                if (moveAction != null)
                {
                    playerActions.Enable();
                }
            }
        }

        keyboard = Keyboard.current;
        mouse = Mouse.current;
    }

    private void Update()
    {
        // Allow Camera to follow Target
        if (followTransform != null)
        {
            transform.position = followTransform.position;
        }
        // Let us control Camera
        else
        {
            HandleCameraMovement();
        }

        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            followTransform = null;
        }
    }

    void HandleCameraMovement()
    {
        // Mouse Drag
        if (moveWithMouseDrag)
        {
            HandleMouseDragInput();
        }

        // Keyboard Control
        if (moveWithKeyboad)
        {
            movementSpeed = IsFastMovementPressed() ? fastSpeed : normalSpeed;

            Vector2 moveInput = GetMoveInput();

            if (moveInput.y > 0f)
            {
                newPosition += transform.forward * movementSpeed;
            }
            if (moveInput.y < 0f)
            {
                newPosition += transform.forward * -movementSpeed;
            }
            if (moveInput.x > 0f)
            {
                newPosition += transform.right * movementSpeed;
            }
            if (moveInput.x < 0f)
            {
                newPosition += transform.right * -movementSpeed;
            }
        }

        // Edge Scrolling
        if (moveWithEdgeScrolling)
        {
            if (mouse == null)
            {
                return;
            }

            Vector2 mousePosition = mouse.position.ReadValue();

            // Move Right
            if (mousePosition.x > Screen.width - edgeSize)
            {
                newPosition += transform.right * movementSpeed;
                ChangeCursor(CursorArrow.RIGHT);
                isCursorSet = true;
            }

            // Move Left
            else if (mousePosition.x < edgeSize)
            {
                newPosition += transform.right * -movementSpeed;
                ChangeCursor(CursorArrow.LEFT);
                isCursorSet = true;
            }

            // Move Up
            else if (mousePosition.y > Screen.height - edgeSize)
            {
                newPosition += transform.forward * movementSpeed;
                ChangeCursor(CursorArrow.UP);
                isCursorSet = true;
            }

            // Move Down
            else if (mousePosition.y < edgeSize)
            {
                newPosition += transform.forward * -movementSpeed;
                ChangeCursor(CursorArrow.DOWN);
                isCursorSet = true;
            }
            else if (isCursorSet)
            {
                ChangeCursor(CursorArrow.DEFAULT);
                isCursorSet = false;
            }
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementSensitivity);

        Cursor.lockState = CursorLockMode.Confined; // If we have an extra monitor we don't want to exit screen bounds
    }

    private bool IsFastMovementPressed()
    {
        if (keyboard == null)
        {
            return false;
        }

        return keyboard.leftShiftKey.isPressed ||
               keyboard.rightShiftKey.isPressed ||
               keyboard.leftCtrlKey.isPressed ||
               keyboard.rightCtrlKey.isPressed ||
               keyboard.leftCommandKey.isPressed ||
               keyboard.rightCommandKey.isPressed;
    }

    private Vector2 GetMoveInput()
    {
        if (moveAction != null)
        {
            return moveAction.ReadValue<Vector2>();
        }

        if (keyboard == null)
        {
            return Vector2.zero;
        }

        Vector2 input = Vector2.zero;

        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
            input.y += 1f;
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
            input.y -= 1f;
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
            input.x += 1f;
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
            input.x -= 1f;

        return input;
    }

    private void ChangeCursor(CursorArrow newCursor)
    {
        // Only change cursor if its not the same cursor
        if (currentCursor != newCursor)
        {
            switch (newCursor)
            {
                case CursorArrow.UP:
                    Cursor.SetCursor(cursorArrowUp, Vector2.zero, CursorMode.Auto);
                    break;
                case CursorArrow.DOWN:
                    Cursor.SetCursor(cursorArrowDown, new Vector2(cursorArrowDown.width, cursorArrowDown.height), CursorMode.Auto); // So the Cursor will stay inside view
                    break;
                case CursorArrow.LEFT:
                    Cursor.SetCursor(cursorArrowLeft, Vector2.zero, CursorMode.Auto);
                    break;
                case CursorArrow.RIGHT:
                    Cursor.SetCursor(cursorArrowRight, new Vector2(cursorArrowRight.width, cursorArrowRight.height), CursorMode.Auto); // So the Cursor will stay inside view
                    break;
                case CursorArrow.DEFAULT:
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    break;
            }

            currentCursor = newCursor;
        }
    }

    private void HandleMouseDragInput()
    {
        if (mouse == null)
        {
            return;
        }

        if (mouse.middleButton.wasPressedThisFrame && EventSystem.current.IsPointerOverGameObject() == false)
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }

        if (mouse.middleButton.isPressed && EventSystem.current.IsPointerOverGameObject() == false)
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);
                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }
    }
}