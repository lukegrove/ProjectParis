using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BattleManager))]
public class BattleSession : MonoBehaviour
{
    private Battle Battle;
    private BattleResult BattleResult;
    private BattleManager BattleManager;
    
    void Awake()
    {
        Battle = new();

        BattleManager = GetComponent<BattleManager>();
    }

    void Start()
    {
        ShipController[] ships = FindObjectsByType<ShipController>();

        foreach (ShipController ship in ships)
        {
            Battle.RegisterShip(ship);
        }

        Debug.Log($"Starting {Battle.AllyShips.Count} vs {Battle.EnemyShips.Count}");
        Battle.BattleState = BattleState.InProgress;
    }

    void Update()
    {
        //
    }

    public void ReportLoss(ShipController ship)
    {
        if (Battle.ReportLoss(ship))
        {
            if (ship.Faction == Faction.Faction1)
            {
                Debug.Log($"Allied ship lost.");
            }
            else
            {
                Debug.Log($"Enemy ship destroyed.");
            }

            if (Battle.BattleState == BattleState.Victory)
            {
                Debug.Log("Battle won!");
                Debug.Log($"You lost {Battle.AllyShipsLost} ships and destroyed {Battle.EnemyShipsLost} enemy ships.");
                BattleResult = Battle.CreateResults(); 
                BattleManager.EndBattle(BattleResult);
            }
            else if (Battle.BattleState == BattleState.Defeat)
            {
                Debug.Log("Battle lost!");
                Debug.Log($"You lost {Battle.AllyShipsLost} ships and destroyed {Battle.EnemyShipsLost} enemy ships.");
                BattleResult = Battle.CreateResults();
                BattleManager.EndBattle(BattleResult);
            }
        }
    }
}