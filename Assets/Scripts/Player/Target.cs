using UnityEngine;

/// <summary>
/// Practice target.
/// </summary>
public class Target : MonoBehaviour, IInteractable
{
    public GameObject target;
    public int health;

    public void Interact()
    {
        health -= 1;
        Debug.Log(health);

        if (health <= 0)
        {
            Destroy(target);
        }
    }
}
