using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    public GameObject player;
    private PlayerController playerController;
    private float spawnRange  = 9.0f;
    public int enemyCount;
    public int waveNumber = 1;
    // Start is called before the first frame update

    void Start()
    {
      player = GameObject.Find("Player");
      if (player != null)
      {
          playerController = player.GetComponent<PlayerController>();
      }
      SpawnEnemyWave(waveNumber);
      Instantiate(powerUpPrefab, randomSpawnManager(), powerUpPrefab.transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
      enemyCount = FindObjectsOfType<Enemy>().Length;
      if(enemyCount == 0 && playerController.isGameOn == true){
        waveNumber++;
        SpawnEnemyWave(waveNumber);
        Instantiate(powerUpPrefab, randomSpawnManager(), powerUpPrefab.transform.rotation);
      }
    }

    void SpawnEnemyWave(int enemiesToSpawn){
      for(int i = 0; i < enemiesToSpawn;i++){
        Instantiate(enemyPrefab, randomSpawnManager(), enemyPrefab.transform.rotation);
      }
    }

    private Vector3 randomSpawnManager()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }
}
