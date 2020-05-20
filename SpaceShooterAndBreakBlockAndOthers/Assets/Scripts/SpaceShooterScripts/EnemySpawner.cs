using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Order of waves
    Spawn each wave
    Spawn each enemy in the wave
*/
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;

    
    [SerializeField] bool loopWave = false; // For looping wave
    // Start is called before the first frame update
    // For looping wave
    // Cannot do in update (update too fast and coroutine don't know how long it takes)
    // void can't yield
    // DO NOT VOID AND DO WHILE LOOP WITH COROUTINE WITOUT YIELD
    // Coroutine need a delay -> else after yield in SpawnWave it loop StartCoroutien too soon-> infinite loop until crash
    IEnumerator Start()
    {
        do{
            yield return StartCoroutine(SpawnAllWaves());
        }while(loopWave);
        
    }
    
    private IEnumerator SpawnAllWaves(){
        for(int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++){
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig){
        for(int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++){
            var newEnemy = Instantiate( waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);

            // get configuration of the wave to pass on enemypathing
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());
        }
    }

    
}
