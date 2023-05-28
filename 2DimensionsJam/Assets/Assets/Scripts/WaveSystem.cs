using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [Header("List of enemies alive")]
    public List<GameObject> aliveList = new List<GameObject>();

    [Space]
    
    [Header("Wave Timer")]
    [SerializeField] float nextWaveStart;
    
    [Space]
    
    [Header("Weapon Spawning")]
    public Transform[] WeaponSpawnPoints;
    public List<GameObject> weaponList = new List<GameObject>();
    
    [Space]
    
    [Header("Enemy Wave Spawning")]
    public Transform[] EnemySpawnPoints;
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
                    var randomEnemySpawnPoint = EnemySpawnPoints[Random.Range(0, EnemySpawnPoints.Length - 1)];
                    GameObject enemy;
                    enemy = Instantiate(currentWave.GetEnemyPrefab(0), randomEnemySpawnPoint.position, Quaternion.identity);
                    Debug.Log("Enemy Alive");
                    aliveList.Add(enemy);
                    yield return new WaitForSeconds(wave.GetSpawnTime());

                }
            }
            for (int j = 0; j < currentWave.GetWeaponCount(); j++)
            {
                var randomWeaponSpawnPoint = WeaponSpawnPoints[Random.Range(0, WeaponSpawnPoints.Length - 1)];
                GameObject weapon;
                weapon = Instantiate(currentWave.GetWeaponPrefab(0), randomWeaponSpawnPoint.position, Quaternion.identity);
                weaponList.Add(weapon);
                Debug.Log("Weapon in scene and weapon spawn points");
            }
            while (aliveList.Count > 25)
            {
                canSpawn = false;
            }
            while (aliveList.Count > 0)
            {
                yield return null;
            }
            yield return new WaitForSeconds(nextWaveStart);
        }
    }
}
