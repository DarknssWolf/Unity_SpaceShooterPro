using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int _spawnEnemy = 1;
    public float _spawnRate = 5.0f;
    public GameObject PlayerPreFab;
    public GameObject EnemyPreFab;
    public GameObject AsteroidPreFab;
    public GameObject EnemyContainerPreFab;
    public GameObject[] PowerUpPreFabs;

    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(PlayerPreFab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    private void Update()
    {
        if(_stopSpawning && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Game");
        }

        if (_stopSpawning && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Game Quited");
            Application.Quit();
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        //StartCoroutine(SpawnAsteroidRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        Debug.Log("Starting enemy Coroutine");
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            GameObject newEnemy = Instantiate(EnemyPreFab,  new Vector3(Random.Range(-8f, 8f), 8, 0), Quaternion.identity, EnemyContainerPreFab.transform);
            //Can also setup parent by using the variable
            //newEnemy.transform.parent.SetParent(EnemyContainer.transform);

            yield return new WaitForSeconds(_spawnRate);
        }
    }

    //IEnumerator SpawnAsteroidRoutine()
    //{
    //    while (_stopSpawning == false)
    //    {
    //        GameObject newAsteroid = Instantiate(AsteroidPreFab, new Vector3(Random.Range(-8f, 8f), 8, 0), Quaternion.identity, EnemyContainerPreFab.transform);
    //        //Can also setup parent by using the variable
    //        //newEnemy.transform.parent.SetParent(EnemyContainer.transform);

    //        yield return new WaitForSeconds(_spawnRate);
    //    }
    //}

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            //randomly instantiate powerup
            int pwrUp = Random.Range(0, 3);

            Instantiate(PowerUpPreFabs[pwrUp], new Vector3(Random.Range(-8f, 8f), 8, 0), Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void PlayerDeath()
    {
        _stopSpawning = true;
    }
}
