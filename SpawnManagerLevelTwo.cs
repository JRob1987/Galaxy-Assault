using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerLevelTwo : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject[] _powerUps;
    [SerializeField] private GameObject _nemesisPrefab;
    [SerializeField] private GameObject _asteroidPrefab;
    [SerializeField] private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
        StartCoroutine(SpeedPowerupSpawnRoutine());
        StartCoroutine(ShieldPowerupSpawnRoutine());
        StartCoroutine(NemesisSpawnRoutine());
        StartCoroutine(AsteroidSpawnRoutine());

    }

    
    //method for spawning the enemy
    IEnumerator EnemySpawnRoutine()
    {
        
        while(_stopSpawning == false)
        {
            Vector3 randomSpawn = new Vector3(Random.Range(-9.44f, 9.44f), 9.5f, 0);
           GameObject newEnemy = Instantiate(_enemyPrefab, randomSpawn, Quaternion.identity);
            yield return new WaitForSeconds(2.0f);
        }
    }

    //method for spawning nemesis
    IEnumerator NemesisSpawnRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        while(_stopSpawning == false)
        {
            Vector3 randomSpawn = new Vector3(Random.Range(-9.44f, 9.44f), 9.5f, 0);
            GameObject newNemesis = Instantiate(_nemesisPrefab, randomSpawn, Quaternion.identity);
            yield return new WaitForSeconds(10.0f);
        }
       
    }

    //Coroutine for  spawning tripleshot powerup 
    IEnumerator PowerUpSpawnRoutine()
    {
        yield return new WaitForSeconds(8.0f);
        while(_stopSpawning == false)
        {
            Vector3 randomSpawn = new Vector3(Random.Range(-9.44f, 9.44f), 9.5f, 0);
            GameObject newTripleShotPowerUp = Instantiate(_powerUps[0], randomSpawn, Quaternion.identity);
            yield return new WaitForSeconds(7.0f);
        }
        
    }

    //Coroutine for spawning speed powerup
    IEnumerator SpeedPowerupSpawnRoutine()
    {
        yield return new WaitForSeconds(6.0f);
        while(_stopSpawning == false)
        {
            Vector3 randomSpawn = new Vector3(Random.Range(-9.44f, 9.44f), 9.5f, 0);
            GameObject newSpeedPowerUp = Instantiate(_powerUps[1], randomSpawn, Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    //Coroutine for spawning speed powerup
    IEnumerator ShieldPowerupSpawnRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        while (_stopSpawning == false)
        {
            Vector3 randomSpawn = new Vector3(Random.Range(-9.44f, 9.44f), 9.5f, 0);
            GameObject newSpeedPowerUp = Instantiate(_powerUps[2], randomSpawn, Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
       
    //spawning the asteroid
    IEnumerator AsteroidSpawnRoutine()
    {
        yield return new WaitForSeconds(8.0f);
        while(_stopSpawning == false)
        {
            Vector3 randomSpawn = new Vector3(Random.Range(-9.44f, 9.44f), 9.5f, 0);
            GameObject newAsteroid = Instantiate(_asteroidPrefab, randomSpawn, Quaternion.identity);
            yield return new WaitForSeconds(7.0f);
        }
    }

    //method to stop spawning once player is dead
    public void OnPlayerDead()
    {
       
        _stopSpawning = true;
        
    }
}
