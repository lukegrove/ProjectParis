using System.Collections.Generic;
using UnityEngine;

enum BattleState
{
    InProgress,
    Victory,
    Defeat,
    Draw
}

public class BattleSession : MonoBehaviour
{
    private List<ShipController> AllyShips = new(), EnemyShips = new();
    private BattleState BattleState;
    private int AllyShipsLost, EnemyShipsLost;

    void Start()
    {
        ShipController[] ships = FindObjectsByType<ShipController>();

        foreach (ShipController ship in ships)
        {
            RegisterShip(ship);
        }

        Debug.Log($"Starting {AllyShips.Count} vs {EnemyShips.Count}");
        BattleState = BattleState.InProgress;
    }

    void Update()
    {
        //
    }

    private void RegisterShip(ShipController ship)
    {
        if (ship == null)
        {
            return;
        }

        if (AllyShips.Contains(ship) || EnemyShips.Contains(ship))
        {
            return;
        }

        if (ship.GetFaction() == Faction.Faction1)
        {
            AllyShips.Add(ship);
        }
        else
        {
            EnemyShips.Add(ship);
        }
    }

    public void ReportLoss(ShipController ship)
    {
        if (ship == null)
        {
            return;
        }

        if (BattleState != BattleState.InProgress)
        {
            return;
        }

        if (ship.Faction == Faction.Faction1)
        {
            Debug.Log($"Allied ship lost.");
            AllyShips.Remove(ship);
            AllyShipsLost++;
            CheckBattleState();
        }
        else
        {
            Debug.Log($"Enemy ship destroyed.");
            EnemyShips.Remove(ship);
            EnemyShipsLost++;
            CheckBattleState();
        }
    }

    public void CheckBattleState()
    {
        if (BattleState == BattleState.InProgress && AllyShips.Count == 0)
        {
            Debug.Log("Battle lost!");
            Debug.Log($"You lost {AllyShipsLost} ships and destroyed {EnemyShipsLost} enemy ships.");
            BattleState = BattleState.Defeat;
        }
        
        if (BattleState == BattleState.InProgress && EnemyShips.Count == 0)
        {
            Debug.Log("Battle won!");
            Debug.Log($"You lost {AllyShipsLost} ships and destroyed {EnemyShipsLost} enemy ships.");
            BattleState = BattleState.Victory;
        }
    }
}