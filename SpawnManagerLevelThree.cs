using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerLevelThree : MonoBehaviour
{
    [SerializeField] private bool _stopSpawning;
    [SerializeField] private GameObject _enemyShip;
    [SerializeField] private GameObject _asteroid;
    [SerializeField] private GameObject _nemesisShip;
    [SerializeField] private GameObject _speedBoostPowerup;
    [SerializeField] private GameObject _tripleShotPowerup;
    [SerializeField] private GameObject _shieldPowerUp;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(AsteroidSpawnRoutine());
        StartCoroutine(NemesisSpawnRoutine());
        StartCoroutine(SpeedBoostPowerUpSpawn());
        StartCoroutine(TripleShotPowerUpSpawn());
        StartCoroutine(ShieldPowerUpSpawn());
        
                
    }
    //method to stop spawning one player is dead
    public void PlayerDead()
    {
        _stopSpawning = true;
    }

    //co routine for spawning the enemy
    IEnumerator EnemySpawnRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while(_stopSpawning == false)
        {
            
            Vector3 randomSpawn = new Vector3(Random.Range(9.58f, -9.58f), 6.88f, 0);
            GameObject newEnemy = Instantiate(_enemyShip, randomSpawn, Quaternion.identity);
            yield return new WaitForSeconds(4.0f);
        }
    }

    //co routine for spawning the asteroid
    IEnumerator AsteroidSpawnRoutine()
    {
        yield return new WaitForSeconds(7.0f);
        while(_stopSpawning == false)
        {
            Vector3 randomSpawn = new Vector3(Random.Range(9.58f, -9.58f), 6.88f, 0);
            GameObject newAsteroid = Instantiate(_asteroid, randomSpawn, Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    //co routine for spawning the nemesis ship
    IEnumerator NemesisSpawnRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        while(_stopSpawning == false)
        {
            Vector3 randomSpawn = new Vector3(Random.Range(9.58f, -9.58f), 6.88f, 0);
            GameObject newNemesis = Instantiate(_nemesisShip, randomSpawn, Quaternion.identity);
            yield return new WaitForSeconds(10.0f);
        }
    }

    //co routine for spawning the speed boost powerup
   IEnumerator SpeedBoostPowerUpSpawn()
    {
        yield return new WaitForSeconds(3.0f);
        while(_stopSpawning == false)
        {
            Vector3 randomSpawn = new Vector3(Random.Range(9.58f, -9.58f), 6.88f, 0);
            GameObject newSpeedBootPowerup = Instantiate(_speedBoostPowerup, randomSpawn, Quaternion.identity);
            yield return new WaitForSeconds(6.0f);
        }
    }

    IEnumerator TripleShotPowerUpSpawn()
    {
        yield return new WaitForSeconds(6.0f);
        while(_stopSpawning == false)
        {
            Vector3 randomSpawn = new Vector3(Random.Range(9.58f, -9.58f), 6.88f, 0);
            GameObject newTripleShotPowerup = Instantiate(_tripleShotPowerup, randomSpawn, Quaternion.identity);
            yield return new WaitForSeconds(8.0f);
        }
    }

    IEnumerator ShieldPowerUpSpawn()
    {
        yield return new WaitForSeconds(7.0f);
        while(_stopSpawning == false)
        {
            Vector3 randomSpawn = new Vector3(Random.Range(9.58f, -9.58f), 6.88f, 0);
            GameObject newShieldPowerUp = Instantiate(_shieldPowerUp, randomSpawn, Quaternion.identity);
            yield return new WaitForSeconds(10.0f);
        }
    }
    

      
   
}
