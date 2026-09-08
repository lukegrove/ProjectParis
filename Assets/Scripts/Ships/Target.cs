using UnityEngine;

public interface IInteractable
{
    public void Interact();
}

namespace Perseus.Ships
{
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
}
