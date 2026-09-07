using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Perseus.Combat
{
    /// <summary>
    /// Class for managing unit movement.
    /// </summary>
    public class UnitMovement : MonoBehaviour
    {
        Camera camera;
        NavMeshAgent agent;
        public LayerMask ground;

        private void Start()
        {
            camera = Camera.main;
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
            {
                Vector2 mousePosition = Mouse.current.position.ReadValue();
                if (Physics.Raycast(camera.ScreenPointToRay(mousePosition), out RaycastHit hit, Mathf.Infinity, ground))
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
    }
}