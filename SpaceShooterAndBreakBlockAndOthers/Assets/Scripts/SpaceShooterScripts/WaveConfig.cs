using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Path's waypoint data
    Enemy prefab to use
    Spawn speed for wave
    Number of enemies in wave
    Movement speed of wave
*/

[CreateAssetMenu(fileName = "WaveConfig", menuName = "ProjectMullover/WaveConfig", order = 0)]
public class WaveConfig : ScriptableObject {
    
  [SerializeField] GameObject enemyPrefab;
  [SerializeField] GameObject pathPrefab;
  [SerializeField] float timeBetweenWave = 0.5f;
  [SerializeField] float waveRandomFactor = 0.3f;
  [SerializeField] int numberOfEnemies = 5;
  [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab(){ return enemyPrefab; } 
    public List<Transform> GetWaypoints(){
        var waveWaypoints = new List<Transform>();

        foreach(Transform child in pathPrefab.transform){
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }
    public float GetTimeBetweenSpawn(){ return timeBetweenWave; }
    public float GetWaveRandomFactor(){ return waveRandomFactor; }
    public  int GetNumberOfEnemies(){ return numberOfEnemies; }
    public float GetMoveSpeed(){ return moveSpeed; }
}
