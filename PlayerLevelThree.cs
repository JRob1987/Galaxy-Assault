//this script defines the behavior of the player in Level 3 of the game. ****

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevelThree : MonoBehaviour
{
    
    [SerializeField] private float playerSpeed = 5.0f; //player speed
    [SerializeField] private GameObject _laserPrefab; //laser gameobject
    [SerializeField] private float _fireRate; //fire rate
    private float _nextFire; //determines when we can fire next after a certain amount of time
    [SerializeField] private int _playerLives;
    SpawnManagerLevelThree _spawnManager;
    [SerializeField] private int playerScore = 0;
    UIManagerLevelThree _uiManager;
    GameManagerLevelThree _gameManager;
    [SerializeField] private GameObject _explosionPrefab;
    
    //variables for speed powerup
    [SerializeField] private GameObject _speedPowerUpPrefab;
    [SerializeField] private float _speedBoost;
    [SerializeField] private bool _canSpeedBoost = false;

    //variables for tripleshot powerup
    [SerializeField] private bool _canTripleShot = false;
    [SerializeField] private GameObject _tripleShotPoweerUpPrefab;
    [SerializeField] private GameObject[] _leftRightLasers;

    //variables for shield powerup
    [SerializeField] private GameObject _shieldPowerUpPrefab;
    [SerializeField] private bool _isShieldActivated = false;
    [SerializeField] private GameObject _playerShield;

    //Variables for engine damange
    [SerializeField] private GameObject[] _engineFailures;
    

    
    // Start is called before the first frame update
    void Start()
    {
        StartingPosition(); //method call

        _spawnManager = GameObject.Find("Spawn_Manager_3").GetComponent<SpawnManagerLevelThree>();
        if(_spawnManager == null)
        {
            Debug.LogError("Spawn manager object is null");
        }

        _playerShield.SetActive(false);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManagerLevelThree>();
        if(_uiManager == null)
        {
            Debug.LogError("The canvas object is null");
        }

        _engineFailures[0].SetActive(false);
        _engineFailures[1].SetActive(false);

       
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement(); //method call
        FireLaser();
    }

    //Player Starting Position Method
    void StartingPosition()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    //Player Movement Method
    void PlayerMovement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal"); //horizontal movement using the a,d or left, right arrow keys
        float verticalMovement = Input.GetAxis("Vertical"); //vertical movement using the w,s or up, down arrow keys
        
        if(_canSpeedBoost == true)
        {
            //moves player to the right and left
            transform.Translate(Vector3.right * _speedBoost *horizontalMovement * playerSpeed * Time.deltaTime);

            //moves player up and down
            transform.Translate(Vector3.up * _speedBoost * verticalMovement * playerSpeed * Time.deltaTime);
        }

        else if(_canSpeedBoost == false)
        {
            //moves player to the right and left
            transform.Translate(Vector3.right * horizontalMovement * playerSpeed * Time.deltaTime);

            //moves player up and down
            transform.Translate(Vector3.up * verticalMovement * playerSpeed * Time.deltaTime);
        }
       

        //player bounds on the x-axis
        if(transform.position.x > 9.27f)
        {
            transform.position = new Vector3(-9.27f, transform.position.y, 0);
        }
        else if(transform.position.x < -9.27f)
        {
            transform.position = new Vector3(9.27f, transform.position.y, 0);
        }

        //player bounds on the y-axis
        if(transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if(transform.position.y <= -3.51f)
        {
            transform.position = new Vector3(transform.position.x, -3.51f, 0);
        }
    }

    //Fire Laser Method
    void FireLaser()
    {
        if (Input.GetMouseButton(1) && _canTripleShot == true && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            Instantiate(_leftRightLasers[0], transform.position + new Vector3(-0.94f, -0.6f, 0), Quaternion.identity);
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.34f, 0), Quaternion.identity);
            Instantiate(_leftRightLasers[1], transform.position + new Vector3(0.94f, -0.6f, 0), Quaternion.identity);

        }
        else if (Input.GetMouseButton(0) && _canTripleShot == false && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.34f, 0), Quaternion.identity);
        }

    }

   //Player Damage
    public void PlayerDamage()
    {
       if(_isShieldActivated == true)
        {
            _playerLives += 0;
        }
       else if(_isShieldActivated == false)
        {
            _engineFailures[Random.Range(0, _engineFailures.Length)].SetActive(true);
            //decrease player's life by 1 once hit
            _playerLives -= 1;
            //communicate with UIManager script to call the UpdatePlayerLives method
            _uiManager.UpdatePlayerLivesUI(_playerLives);

        }


        //if the player's lives fall below 1
        if (_playerLives < 1)
        {
            Instantiate(_explosionPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Destroy(this.gameObject);
            _spawnManager.PlayerDead();
            
            
        }
    }

    //method when player gets hit by the enemy laser
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyLaser")
        {
            Instantiate(_explosionPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Destroy(other.gameObject);
            PlayerDamage();
        }


    }

    //method for activating the speed boost powerup
    public void ActivateSpeedBoost()
    {
        _canSpeedBoost = true;
        playerSpeed = playerSpeed + _speedBoost;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    //coroutine to allow speed boost active for a certain amount of time
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _canSpeedBoost = false;
    }

    public void ActivateTripleShot()
    {
        _canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());

    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _canTripleShot = false;
    }

    public void ActivateShieldPowerUp()
    {
        _isShieldActivated = true;
        _playerShield.SetActive(true);
        StartCoroutine(ShieldPowerDownRoutine());
    }

    IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isShieldActivated = false;
        _playerShield.SetActive(false);
    }

    //method to update the player's score by 10 points
    public void UpdatePlayerScore()
    {
        playerScore = playerScore + 10;
        _uiManager.UpdatePlayerScoreUI(playerScore);

        //if the player score reaches 90 load the next level
        if(playerScore == 90)
        {
            StartCoroutine(LoadNextLevel());
        }

    }

    //method to load the next scene after a certain amount of time
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(4);
    }

   

   
}

