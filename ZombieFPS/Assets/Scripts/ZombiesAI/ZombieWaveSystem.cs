using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ZombieWaveSystem : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public Transform[] spawnPoints;
    public float timeBetweenWaves = 10f;
    [SerializeField] private float waveTimer = 0f;
    private int waveNumber = 1;
    public int zombiePerWave = 4;
    [Header("UI")]
    public Text WaveNumber;
    public Text WaveTimer;
     void Update()
    {
        LoadSetting();
        if (waveNumber == 10)
            return;

        waveTimer += Time.deltaTime;
        int intValue = Mathf.RoundToInt(waveTimer);
        WaveTimer.text = intValue.ToString();
        if(waveTimer>=timeBetweenWaves)
        {
            StartNewWave();
        }
       
    }
    void StartNewWave()
    {
        waveTimer = 0f;
        zombiePerWave += 2;

        float minDistance = 4f;

        for(int i=0;i<zombiePerWave;i++)
        {
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomSpawnIndex];
            GameObject randomZombiePrefab = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];

            Vector3 spawnPosition = spawnPoint.position + Random.insideUnitSphere * minDistance;

            spawnPosition.y = spawnPoint.position.y;
            Instantiate(randomZombiePrefab, spawnPosition, spawnPoint.rotation);
        }
        waveNumber++;
        WaveNumber.text = waveNumber.ToString();
    }
    void LoadSetting()
    {
        if (PlayerPrefs.HasKey("TimeBetweenWaves"))
        {
            timeBetweenWaves = PlayerPrefs.GetFloat("TimeBetweenWaves");
        }
        if (PlayerPrefs.HasKey("ZombiesPerWave"))
        {
            zombiePerWave = PlayerPrefs.GetInt("ZombiesPerWave");
        }
    }
}