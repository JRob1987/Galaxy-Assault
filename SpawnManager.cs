//**This script defines the spawn manager for level 1 of the game***
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab; //variable for enemy prefab object
    [SerializeField] private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    [SerializeField] private GameObject[] _powerUp;
    [SerializeField] private GameObject _asteroidPrefab;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine()); //runs this coroutine at the start of the game
        StartCoroutine(SpawnPowerUpRoutine());
        StartCoroutine(AsteroidSpawnRoutine());
       
    }

    

        
    //method is for spawning the enemy every 5 seconds.
     IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        //game loop when player is still alive, so enemies can keep spawning
        while (_stopSpawning == false)
        {
            //instantiate enemyprefab
            Vector3 randomSpawn = new Vector3(Random.Range(-9.45f, 9.45f), 6.63f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, randomSpawn , Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            //yield wait for 5 seconds
            yield return new WaitForSeconds(5.0f);
            
        }
        
        
    }
    
    //method for spawning the triple shot powerup every 5 seconds
    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(4.0f);
        while (_stopSpawning == false)
        {            
            Vector3 positionSpawn = new Vector3(Random.Range(-9.45f, 9.45f), 6.63f, 0);
            int powerUpSelection = Random.Range(0, 3);
            GameObject newTripleShotPowerUp = Instantiate(_powerUp[powerUpSelection], positionSpawn, Quaternion.identity);
            yield return new WaitForSeconds(5.0f); //yield wait for 5 seconds
        }
    }

    //method for spawning the asteroid every 7 seconds
    IEnumerator AsteroidSpawnRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        while (_stopSpawning == false)
        {
            Vector3 randomAsteroidSpawn = new Vector3(Random.Range(-9.45f, 9.45f), 6.63f, 0);
            GameObject newAsteroidSpawn = Instantiate(_asteroidPrefab, randomAsteroidSpawn, Quaternion.identity);
            yield return new WaitForSeconds(7.0f);
        }
    }

    
    //method to stop enemy and powerup spawning after the player dies
    public void OnPlayerDead()
    {
        _stopSpawning = true;
        //Debug.Log("Game Over!");
    }
}
