//this script defines the behavior of the player in level 2 of the game.***

//namespaces
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevelTwo : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 5.0f; //laser speed
    [SerializeField] private GameObject _laserPrefab; //laser gameobject
    [SerializeField] private float fireRate = 0.3f;
    [SerializeField] private float nextFire = 0.0f;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _playerLives = 3;
    [SerializeField] private int  _score;
    [SerializeField] private GameObject playerExplosion;
    SpawnManagerLevelTwo _spawnManager;


    //variables for tripleshot powerup
    [SerializeField] private bool _canTripleShot = false;
    [SerializeField] private GameObject[] _leftRightLasers;

    //variables for speed powerup
    [SerializeField] private GameObject _speedPowerUpPrefab;
    [SerializeField] private bool _speedBoostPowerUpOn = false;
    [SerializeField] private float _speedBoost = 2.0f;

    //variables for shield powerup
    [SerializeField] private GameObject _shieldPowerupPrefab;
    [SerializeField] private bool _playerShieldOn = false;
    [SerializeField] private GameObject _playerShield;

    //variables for Nemesis gameobject
    [SerializeField] private GameObject _nemesisPrefab;

    //variables for the UIManager
    private UIManagerLevelTwo _uiManager; //handle to the UIManagerLevelTwoScript

    //variables for lasershot clip
    AudioSource _laserShotClip;

    //variable for engine failures
    [SerializeField] private GameObject[] _engineFailures;
    

    
        

    // Start is called before the first frame update
    void Start()
    {
        //function or method calls
        PlayerStartingPosition();

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManagerLevelTwo>();
        if(_spawnManager == null)
        {
            Debug.LogError("Spawn manager is null.");
        }
        _playerShield.SetActive(false);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManagerLevelTwo>();
        if (_uiManager == null)
        {
            Debug.LogError("UI manager is null.");
        }

        _laserShotClip = GetComponent<AudioSource>();

        _engineFailures[0].SetActive(false);
        _engineFailures[1].SetActive(false);


    }

    // Update is called once per frame. A game loop
    void Update()
    {
        PlayerMovement();
        FireLaser();
    }

    //Player's starting position at the beginning of the level
    void PlayerStartingPosition()
    {
        //Player starting a position 0,0,0
        transform.position = new Vector3(0, 0, 0);
    }

    //method for player movement
    void PlayerMovement()
    {
        //moving the player left or right along the x-axis using a,d or left/right arrow keys
        float _horizontalMovement = Input.GetAxis("Horizontal");
        float _verticalMovement = Input.GetAxis("Vertical");

        if(_speedBoostPowerUpOn == true)
        {
            transform.Translate(Vector3.right * _horizontalMovement * _playerSpeed * _speedBoost * Time.deltaTime);
            transform.Translate(Vector3.up * _verticalMovement * _playerSpeed * _speedBoost * Time.deltaTime);
        }

        else if(_speedBoostPowerUpOn == false)
        {
            transform.Translate(Vector3.right * _horizontalMovement * _playerSpeed * Time.deltaTime);
            transform.Translate(Vector3.up * _verticalMovement * _playerSpeed * Time.deltaTime);
        }
        

        //player bounds for horizontal movement
        if(transform.position.x < -9.2f)
        {
            transform.position = new Vector3(9.2f, transform.position.y, 0);
        }
        else if(transform.position.x > 9.2f)
        {
            transform.position = new Vector3(-9.2f, transform.position.y, 0);
        }

        //player bounds for vertical movement
        if(transform.position.y >= 1f)
        {
            transform.position = new Vector3(transform.position.x, 1f, 0);
        }
        else if(transform.position.y <= -1.59f)
        {
            transform.position = new Vector3(transform.position.x, -1.59f, 0);
        }

    }
    
    //method for player firing laser
    void FireLaser()
    {
        if(Input.GetMouseButton(1) && Time.time > nextFire && _canTripleShot == true)
        {
            _laserShotClip.Play();
            nextFire = Time.time + fireRate;
            Instantiate(_leftRightLasers[0], transform.position + new Vector3(0.99f, -0.49f, 0), Quaternion.identity);
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.24f, 0), Quaternion.identity);
            Instantiate(_leftRightLasers[1], transform.position + new Vector3(-0.99f, -0.49f, 0), Quaternion.identity);
        }
        else if(Input.GetMouseButton(0) && Time.time > nextFire && _canTripleShot == false)
        {
            _laserShotClip.Play();
            nextFire = Time.time + fireRate;
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.24f, 0), Quaternion.identity);
        }
        
    }

    //method for when player gets damaged by the enemy
    public void PlayerDamage()
    {
        if(_playerShieldOn == true)
        {
            _playerLives += 0; 
        }
        else if(_playerShieldOn == false)
        {
            _engineFailures[Random.Range(0, _engineFailures.Length)].SetActive(true);
            _playerLives -= 1;
            _uiManager.UpdateUILivesImages(_playerLives);
        }
        

        //if player has no lives left, player dies
        if(_playerLives < 1)
        {
            Instantiate(playerExplosion, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Destroy(this.gameObject);
            //_uiManager.DisplayGameOverMessage();
           // _uiManager.FlashGameOverMessage();
            _spawnManager.OnPlayerDead();
           
        }
    }

    //method for Activating tripleshot powerup
    public void ActivateTripleShot()
    {
        _canTripleShot = true;
        StartCoroutine(TripleShotTimeLimit());
    }

    //method for only activating the tripleshot for 5 seconds
    IEnumerator TripleShotTimeLimit()
    {
        yield return new WaitForSeconds(5.0f);
        _canTripleShot = false;
    }

    //method for activating the speed boost powerup
    public void ActivateSpeedBoost()
    {
        _speedBoostPowerUpOn = true;
        _playerSpeed = _playerSpeed + _speedBoost;
        StartCoroutine(SpeedBoostTimeLimit());

    }

    //Coroutine for only activating the speed boost for 5 seconds
    IEnumerator SpeedBoostTimeLimit()
    {
        yield return new WaitForSeconds(5.0f);
        _speedBoostPowerUpOn = false;
        _playerSpeed = _playerSpeed - _speedBoost;
    }

    //method for Activating shield powerup
    public void ActivateShieldPowerup()
    {
        _playerShieldOn = true;
        _playerShield.SetActive(true);
        StartCoroutine(ShieldTimeLimit());
    }

    //method for only activating the tripleshot for 5 seconds
    IEnumerator ShieldTimeLimit()
    {
        yield return new WaitForSeconds(5.0f);
        _playerShieldOn = false;
        _playerShield.SetActive(false);
    }

    //method to add 10 to the score
    //communicate with the UI to update the score
    public void AddToPlayerScore()
    {
        _score += 10;

        //communicate with the UIManagerLevelTwoScript's to update the score
        _uiManager.UpdatePlayerScore(_score);

        //load level 3 when player reaches a score of 80
        if(_score == 80)
        {
            StartCoroutine(LoadLevelThree());
        }
    }

    //load level 3 after 3 seconds
    IEnumerator LoadLevelThree()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(3);
    }

    
    
}
