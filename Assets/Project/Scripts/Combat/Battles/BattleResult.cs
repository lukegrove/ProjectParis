using System.Collections.Generic;
using ProjectParis.Combat.Ships;

namespace ProjectParis.Combat.Battles
{
    public class BattleResult
    {
    	public List<Ship> AllyShipsLost { get; }
    	public List<Ship> EnemyShipsLost { get; }
    	public BattleState BattleState { get; }

    	public BattleResult(BattleState battleState, List<Ship> allyShipsLost, List<Ship> enemyShipsLost)
    	{
    		BattleState = battleState;
    		AllyShipsLost = allyShipsLost;
    		EnemyShipsLost = enemyShipsLost;
    	}
    }
}