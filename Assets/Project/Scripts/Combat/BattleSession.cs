using System;
using System.Collections.Generic;
using Perseus.Ships;
using Unity.InferenceEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BattleManager))]
public class BattleSession : MonoBehaviour
{
    [SerializeField] private GameObject initiatorSpawn;
    [SerializeField] private GameObject targetSpawn;
    [SerializeField] private GameObject allyShipPrefab;
    [SerializeField] private GameObject enemyShipPrefab;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private int shipsPerRow = 3;
    [SerializeField] private float shipSpacing = 4f;
    private Battle Battle;
    //private BattleResult BattleResult;
    private BattleManager BattleManager;
    
    void Awake()
    {
        Battle = new();

        BattleManager = GetComponent<BattleManager>();
    }

    void Start()
    {
        Scene battleScene = gameObject.scene;
        if (!battleScene.IsValid() || !battleScene.isLoaded || !SceneManager.SetActiveScene(battleScene))
        {
            Debug.LogError("Could not make the loaded Battle scene active; ships were not spawned.");
            return;
        }

        // Ally ships - Need to move implicit association between ally and initiator
        RegisterShips(BattleTransfer.Initiator.Ships, allyShipPrefab, initiatorSpawn.transform, BattleTransfer.Initiator.Faction);
        // Enemey ships
        RegisterShips(BattleTransfer.Target.Ships, enemyShipPrefab, targetSpawn.transform, BattleTransfer.Target.Faction);

        Debug.Log($"Starting {Battle.AllyShips.Count} vs {Battle.EnemyShips.Count}");
        Battle.BattleState = BattleState.InProgress;
    }

    void Update()
    {
        //
    }

    private void RegisterShips(List<Ship> ships, GameObject prefab, Transform spawn, Faction faction)
    {
        int index = 0;

        foreach (Ship ship in ships)
        {
            ship.Stop();
            GameObject shipObject = Instantiate(prefab, GetFormationPosition(spawn, index, ships.Count), Quaternion.identity);
            SceneManager.MoveGameObjectToScene(shipObject, gameObject.scene);

            if (shipObject.scene != gameObject.scene)
            {
                Debug.LogError($"Spawned ship {shipObject.name} into scene {shipObject.scene.name}, not Battle.");
                Destroy(shipObject);
                continue;
            }

            ShipController shipController = shipObject.GetComponent<ShipController>();
            shipController.Initialize(ship, faction, targetLayer);
            Battle.RegisterShip(shipController);
            index++;
        }
    }

    private Vector3 GetFormationPosition(Transform spawnPoint, int shipIndex, int shipCount)
    {
        int row = shipIndex / shipsPerRow;
        int firstShipInRow = row * shipsPerRow;
        int shipsInRow = Mathf.Min(shipsPerRow, shipCount - firstShipInRow);
        int column = shipIndex % shipsPerRow;

        float xOffset = (column - (shipsInRow - 1) / 2f) * shipSpacing;
        float zOffset = -row * shipSpacing;

        return spawnPoint.TransformPoint(new Vector3(xOffset, 0f, zOffset));
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
                BattleResult result = Battle.CreateResults(); 
                Debug.Log("Battle won!");
                Debug.Log($"You lost {result.AllyShipsLost.Count} ships and destroyed {result.EnemyShipsLost.Count} enemy ships.");
                BattleManager.EndBattle(result);
            }
            else if (Battle.BattleState == BattleState.Defeat)
            {
                BattleResult result = Battle.CreateResults(); 
                Debug.Log("Battle lost!");
                Debug.Log($"You lost {result.AllyShipsLost.Count} ships and destroyed {result.EnemyShipsLost.Count} enemy ships.");
                BattleManager.EndBattle(result);
            }
        }
    }
}