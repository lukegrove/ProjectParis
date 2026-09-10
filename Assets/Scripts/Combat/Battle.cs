using System.Collections.Generic;

public enum BattleState
{
    InProgress,
    Victory,
    Defeat,
    Draw
}

public class Battle
{
    public List<ShipController> AllyShips = new(), EnemyShips = new();
    public BattleState BattleState;
    public int AllyShipsLost, EnemyShipsLost;

    public void RegisterShip(ShipController ship)
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

    public bool ReportLoss(ShipController ship)
    {
        if (ship == null)
        {
            return false;
        }

        if (BattleState != BattleState.InProgress)
        {
            return false;
        }

        if (ship.Faction == Faction.Faction1)
        {
            AllyShips.Remove(ship);
            AllyShipsLost++;
            UpdateBattleState();
        }
        else
        {
            EnemyShips.Remove(ship);
            EnemyShipsLost++;
            UpdateBattleState();
        }

        return true;
    }

    public void UpdateBattleState()
    {
        if (BattleState == BattleState.InProgress && AllyShips.Count == 0)
        {
            BattleState = BattleState.Defeat;
        }
        
        if (BattleState == BattleState.InProgress && EnemyShips.Count == 0)
        {
            BattleState = BattleState.Victory;
        }
    }

    public BattleResult CreateResults()
    {
        return new BattleResult(BattleState, AllyShipsLost, EnemyShipsLost);
    }
}