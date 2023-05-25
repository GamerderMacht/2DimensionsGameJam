using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [Header("List of enemies alive")]
    public List<GameObject> alivelist = new List<GameObject>();

    [Header("Wave Timer")]
    [SerializeField] float nextWaveStart;

    [Header("Wave")]
    public Transform[] SpawnPoints;
    [SerializeField] List<WaveConfigSO> waveConfigs;
    WaveConfigSO currentWave;
    public int finalWaveCount;

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        foreach (WaveConfigSO wave in waveConfigs)
        {
            currentWave = wave;
            for (int i = 0; i < currentWave.GetEnemyCount(); i++)
            {
                var randomSpawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length - 1)];
                GameObject enemy;
                enemy = Instantiate(currentWave.GetEnemyPrefab(0), randomSpawnPoint.position, Quaternion.identity);
                Debug.Log("Enemy Alive");
                alivelist.Add(enemy);
                yield return new WaitForSeconds(wave.GetSpawnTime());
            }
            if (wave == null && currentWave.GetEnemyCount() < finalWaveCount)
            {
                Debug.Log("No more waves");
                for (int j = 0; j < currentWave.GetEnemyCount(); j++)
                {
                    var randomSpawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length - 1)];
                    GameObject enemy;
                    enemy = Instantiate(currentWave.GetEnemyPrefab(0), randomSpawnPoint.position, Quaternion.identity);
                    Debug.Log("Enemy Alive");
                    alivelist.Add(enemy);
                    yield return new WaitForSeconds(wave.GetSpawnTime());
                }
                Debug.Log("Infinite wave incoming");
            }
            while (alivelist.Count > 0)
            {
                yield return null;
            }
            yield return new WaitForSeconds(nextWaveStart);
        }
    }
}
