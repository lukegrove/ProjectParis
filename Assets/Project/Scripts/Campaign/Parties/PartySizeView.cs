using TMPro;
using UnityEngine;

namespace ProjectParis.Campaign.Parties
{
    [RequireComponent(typeof(TMP_Text))]
    public class PartySizeView : MonoBehaviour
    {
        public PartyController PartyController;
        private TMP_Text Text;

        public void Initialize(PartyController partyController)
        {
            PartyController = partyController;
            Text = GetComponent<TMP_Text>();
            RefreshCount();
        }

        /*public void Awake()
        {
            Text = GetComponent<TMP_Text>();
        }*/

        public void RefreshCount()
        {
            Text.SetText($"{PartyController.GetShipCount()}");
        }
    }
}