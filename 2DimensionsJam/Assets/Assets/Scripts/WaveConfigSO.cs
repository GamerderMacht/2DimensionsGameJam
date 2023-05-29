using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WaveConfig", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs;

    [SerializeField] float spawnTime = 1f;

    [SerializeField] List<GameObject> weaponPrefabs;

    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }

    public float GetSpawnTime()
    {
        return spawnTime;
    }

    // Weapon 
    public int GetWeaponCount()
    {
        return weaponPrefabs.Count;
    }

    public GameObject GetWeaponPrefab(int index)
    {
        return weaponPrefabs[index];
    }
}
