using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Perseus.Combat
{
    /// <summary>
    /// Singleton class for managing units.
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    public class UnitSelectionManager : MonoBehaviour
    {
        private InputAction multiSelectAction;
        private Camera camera;
        public static UnitSelectionManager Instance { get; set; }
        public List<GameObject> allUnitsList = new();
        public List<GameObject> unitsSelected = new();
        public LayerMask clickable;
        public LayerMask ground;
        public GameObject groundMarker;

        void Start()
        {
            camera = Camera.main;
            PlayerInput playerInput = GetComponent<PlayerInput>();
            InputActionMap playerActions = playerInput.actions.FindActionMap("Player", true);
            multiSelectAction = playerActions.FindAction("MultiSelect", true);
            playerActions.Enable();
        }

        void Update()
        {
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                Vector2 mousePosition = Mouse.current.position.ReadValue();

                // If we are hitting a clickable object
                if (Physics.Raycast(camera.ScreenPointToRay(mousePosition), out RaycastHit hit, Mathf.Infinity, clickable))
                {
                    if (multiSelectAction.IsPressed())
                    {
                        MultiSelect(hit.collider.gameObject);
                    }
                    else
                    {
                        SelectByClicking(hit.collider.gameObject);
                    }
                }
                else
                {
                    if (!multiSelectAction.IsPressed())
                    {
                        DeselectAll();
                    }
                }
            }

            if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
            {
                Vector2 mousePosition = Mouse.current.position.ReadValue();

                if (Physics.Raycast(camera.ScreenPointToRay(mousePosition), out RaycastHit hit, Mathf.Infinity, ground))
                {
                    groundMarker.transform.position = hit.point;
                    groundMarker.SetActive(false);
                    groundMarker.SetActive(true);
                }
            }
        }

        /// <summary>
        /// Verifies we only have one manager active
        /// </summary>
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        /// <summary>
        /// Enables or disables unit movement.
        /// </summary>
        /// <param name="unit">GameObject</param>
        /// <param name="enabled">Enabled?</param>
        private void EnableUnitMovement(GameObject unit, bool enabled)
        {
            unit.GetComponent<UnitMovement>().enabled = enabled;
        }

        /// <summary>
        /// Deselects all selected units.
        /// </summary>
        private void DeselectAll()
        {
            foreach (GameObject unit in unitsSelected)
            {
                DeselectUnit(unit);
            }

            unitsSelected.Clear();
        }

        private void DeselectUnit(GameObject unit)
        {
            EnableUnitMovement(unit, false);
            EnableSelectionIndicator(unit, false);
        }

        /// <summary>
        /// Selects a unit that has been clicked on.
        /// </summary>
        /// <param name="unit">GameObject</param>
        private void SelectByClicking(GameObject unit)
        {
            unitsSelected.Add(unit);
            EnableUnitMovement(unit, true);
            EnableSelectionIndicator(unit, true);
        }

        /// <summary>
        /// Selects all selected units.
        /// </summary>
        /// <param name="unit">GameObject</param>
        private void MultiSelect(GameObject unit)
        {
            if (!unitsSelected.Contains(unit))
            {
                SelectByClicking(unit);
            }
            else
            {
                unitsSelected.Remove(unit);
                DeselectUnit(unit);
            }
        }

        /// <summary>
        /// Enables or disables selection indicator on unit.
        /// </summary>
        /// <param name="unit">GameObject</param>
        /// <param name="enabled">Enabled?</param>
        private void EnableSelectionIndicator(GameObject unit, bool enabled)
        {
            unit.transform.GetChild(0).gameObject.SetActive(enabled);
        }

    }
}