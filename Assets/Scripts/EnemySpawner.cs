using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] Enemy EnemyPrefab;
    [SerializeField] int EnemiesMaxPoolCount = 10;
    [SerializeField] float minSpawnDelay  = 3, maxSpawnDelay = 10;
    [SerializeField] Transform[] spawningPosArray;
    [SerializeField] float levelDuration = 10;
    Enemy[] enemiesPoolArray;
    float currentPlayTime = 0;
    float currentSpawnDelay => Mathf.Lerp(maxSpawnDelay, minSpawnDelay , Mathf.InverseLerp(0 , levelDuration ,currentPlayTime));
    int currentEnemyIndex = 0;
    Enemy.EnemyTypesEnum currentEnemyType;
    Enemy currentEnemy;
    Vector3 currnetSpawnPos;
    List<Enemy.EnemyTypesEnum> typesArray = new List<Enemy.EnemyTypesEnum>();
    void Start()
    {
        var allTypes = Enum.GetValues(typeof(Enemy.EnemyTypesEnum));
        for (int i = 1; i < allTypes.Length; i++)
        {
            typesArray.Add((Enemy.EnemyTypesEnum)allTypes.GetValue(i));
        }
        enemiesPoolArray = new Enemy[10];
        for (int i = 0; i < enemiesPoolArray.Length; i++)
        {
            enemiesPoolArray[i] = Instantiate(EnemyPrefab);
        }
        StartCoroutine(Counter());
        StartCoroutine(Spawning());
    }
    IEnumerator Counter()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            currentPlayTime++;
        }
    }
    IEnumerator Spawning()
    {
        while (true)
        {
            currentEnemy = enemiesPoolArray[currentEnemyIndex];
            currentEnemyType = typesArray[UnityEngine.Random.Range(0, typesArray.Count)];
            currnetSpawnPos = spawningPosArray[UnityEngine.Random.Range(0, spawningPosArray.Length)].position;
            currentEnemyIndex++;
            if (currentEnemyIndex >= enemiesPoolArray.Length)
            {
                currentEnemyIndex = 0;
            }
            float currnetDelay = currentSpawnDelay;
            Debug.Log("currentSpawnDelay " + currnetDelay);
            yield return new WaitForSeconds(currnetDelay);

            currentEnemy.Set(currentEnemyType, currnetSpawnPos, Level.Instance.Tower.transform);
        }
    }
}
