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
    public bool canSpawn;

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
                if (canSpawn)
                {
                    var randomSpawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length - 1)];
                    GameObject enemy;
                    enemy = Instantiate(currentWave.GetEnemyPrefab(0), randomSpawnPoint.position, Quaternion.identity);
                    Debug.Log("Enemy Alive");
                    alivelist.Add(enemy);
                    yield return new WaitForSeconds(wave.GetSpawnTime());

                }
            }
            while (alivelist.Count > 50)
            {
                canSpawn = false;
            }
            while (alivelist.Count > 0)
            {
                yield return null;
            }
            yield return new WaitForSeconds(nextWaveStart);
        }
    }
}
