using UnityEngine;

public class CampaignSession : MonoBehaviour
{
    private Party Party;

    void Awake()
    {
        Party = GetComponent<Party>();
    }
}