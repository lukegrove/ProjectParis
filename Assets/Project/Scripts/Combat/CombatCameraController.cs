using UnityEngine;
using UnityEngine.InputSystem;

public class CombatCameraController : MonoBehaviour
{
    [SerializeField] 
    private float moveSpeed = 15f;

    private void Update()
    {
        var keyboard = Keyboard.current;

        if (keyboard == null)
        {
            return;
        }

        Vector3 move = Vector3.zero;

        if (keyboard.wKey.isPressed)
        {
            move.z += 1f;
        }

        if (keyboard.sKey.isPressed)
        {
            move.z -= 1f;
        } 

        if (keyboard.dKey.isPressed)
        {
            move.x += 1f;
        }
        
        if (keyboard.aKey.isPressed)
        {
            move.x -= 1f;
        }

        transform.position += moveSpeed * Time.deltaTime * move.normalized;
    }
}