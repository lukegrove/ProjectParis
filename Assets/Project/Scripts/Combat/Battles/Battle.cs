using System.Collections.Generic;
using ProjectParis.Combat.Ships;

namespace ProjectParis.Combat.Battles
{
    public class Battle
    {
        public List<Ship> AllyShips = new(), EnemyShips = new();
        public List<Ship> AllyCasualties = new(), EnemyCasualties = new();
        public BattleState BattleState;

        public void RegisterShip(Ship ship)
        {
            if (ship == null)
            {
                return;
            }

            if (AllyShips.Contains(ship) || EnemyShips.Contains(ship))
            {
                return;
            }

            if (ship.faction == Faction.Faction1)
            {
                AllyShips.Add(ship);
            }
            else
            {
                EnemyShips.Add(ship);
            }
        }

        public bool ReportLoss(Ship ship)
        {
            if (ship == null)
            {
                return false;
            }

            if (BattleState != BattleState.InProgress)
            {
                return false;
            }

            if (ship.faction == Faction.Faction1)
            {
                if (!AllyShips.Remove(ship))
                {
                    return false;
                }

                AllyCasualties.Add(ship);
            }
            else
            {
                if (!EnemyShips.Remove(ship))
                {
                    return false;
                }

                EnemyCasualties.Add(ship);
            }

            UpdateBattleState();
        
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
            return new BattleResult(BattleState, AllyCasualties, EnemyCasualties);
        }
    }
}