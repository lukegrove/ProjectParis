using UnityEngine;

namespace Perseus.Combat
{
    public class Unit : MonoBehaviour
    {
        void Start()
        {
            Debug.Log("Unit created.");
            UnitSelectionManager.Instance.allUnitsList.Add(gameObject);
        }

        private void OnDestroy()
        {
            Debug.Log("Unit lost!");
            UnitSelectionManager.Instance.allUnitsList.Remove(gameObject);
        }
    }
}
