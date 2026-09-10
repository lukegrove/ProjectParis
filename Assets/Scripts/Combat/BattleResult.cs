public class BattleResult
{
    public BattleState Result { get; }
	public int AllyShipsLost { get; }
	public int EnemyShipsLost { get; }

	public BattleResult(BattleState result, int allyShipsLost, int enemyShipsLost)
	{
		Result = result;
		AllyShipsLost = allyShipsLost;
		EnemyShipsLost = enemyShipsLost;
	}
}