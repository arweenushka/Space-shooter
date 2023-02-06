using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    [Header("Waves info")]
    [SerializeField] List<WaveConfig> waveConfigs = null;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    private int waveNumber = 0;

    // Use this for initialization
    IEnumerator Start()
    {
        do
        {
            //wait 3 sec before start waves
            yield return new WaitForSeconds(3);
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    public void Update()
    {
        //if it is the last wave , when last enemy is dissapear load next level
        if (waveNumber == waveConfigs.Count-1)
        {
            LoadNextLevel();
        }
    }

    // get enemies from config file and spawn with certain time coroutine and certain amout of enemies
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            //initiate new enemy object and set wave path for it from config file
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    //loop enemy waves. spawn waves one by one depends of how much we have in the list. waves were pre added manually in editor 
    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            //start first wave then before next waves wait for 3 sec
            StartCoroutine(SpawnAllEnemiesInWave(currentWave));
            yield return new WaitForSeconds(3);
            //set  wave name
            waveNumber = waveIndex;


        }
    }

    //check if there are enemies on the scene then load exlenet scene and next level or win scene
    private void LoadNextLevel()
    {
        if(GameObject.FindWithTag("Enemy") == null)
        {
            if (!SceneManager.GetActiveScene().name.Equals("Level 3"))
            {
                //save index of current scene
                FindObjectOfType<GameSession>().SetSceneIndex();
                //load excelent scene and next level
                FindObjectOfType<GameManager>().LoadExcelent();
            }
            else
            {
                //load win scene if it currnet scene is Level 3(last one)
                FindObjectOfType<GameManager>().LoadWin();
            }
        }
    }
}
