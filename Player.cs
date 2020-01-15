//**This script is for the player's behavior in the game.***
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    //***Variable declarations and initializations
    [SerializeField] private float _playerSpeed = 5.0f; //variable for player's speed with a value of 3.5f.
    private float _horizontalMovement; //variable for moving the player horizontally along the x-axis with "a" "d" keys or left and right arrow keys
    private float _verticalMovement; //variable for moving the player horizontally along the y-axis with "w" "s" keys or up and down arrow keys
    [SerializeField] private GameObject _laserPrefab; //variable for laser prefab object
    [SerializeField] private float _fireRate = 0.5f; //variable for the fire rate of the laser when fired by player
    private float _canFire = 0.0f; //determines if we can fire
    [SerializeField] private int _playerLives = 3; //variable for the player's lives
    

    //**Variables for Triple Shot powerup
    [SerializeField] private bool canTripleShot = false; //bool variable to simulate a tripleshot powerup pickup
    [SerializeField] private GameObject _rightLaserPrefab; //variable for right laser prefab object
    [SerializeField] private GameObject _leftLaserPrefab; //variable for left laser prefab object

    //**Variables for the speed powerup
    [SerializeField] private GameObject _speedPowerUpPrefab;
    [SerializeField] private bool canSpeedBoost = false;
    [SerializeField] private float speedBoost = 2.0f;

    //*variables for the shield powerup
    [SerializeField] private GameObject _shieldPowerUpPrefab;
    [SerializeField] private bool shieldActivated = false; //shield powerup activated?
    [SerializeField] private GameObject _playerShield;

    //variables for the Spawn Manager
    private SpawnManager _spawnManager; //variable for the Spawn Manager component

    //Variables for User Interface(UIManager)
    public int _score; //player's score
    private UIManager _uiManager; //variabble for the UIManager component.

    //Variables for Engine Failures
    [SerializeField] private GameObject[] _engineFailure;

    //Variables for player explosion after lives are 0
    [SerializeField] private GameObject _playerExplosion;

    //variable for the lasershot audio clip
    [SerializeField] private AudioClip _laserShotClip;
    // [SerializeField] private AudioClip _playerExplosionClip;
    private AudioSource _audioSource;

     
    
        

    // Start is called before the first frame update
    void Start()
    {
        //player's starting position when the game starts
        transform.position = new Vector3(0, 0, 0);

        _playerShield.SetActive(false);

        //telling the player to communicate with the SpanManager Script
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>(); 

        //checking if the SpawnManager is null
        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is null.");
        }

        //telling the player to communicate with the UIManager script
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>(); 

        //checking if the UIManager is null
        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }

        _engineFailure[0].SetActive(false);
        _engineFailure[1].SetActive(false);

        _audioSource = GetComponent<AudioSource>();

        if(_audioSource == null)
        {
            Debug.LogError("AudioSource on the player is null");
        }
        else
        {
            _audioSource.clip = _laserShotClip;
        }   

        



    }

    // Update is called once per frame. Considered the game loop
    void Update()
    {
        PlayerMovement(); //calling PlayerMovement() method
        PlayerBounds();   //calling PlayerBounds() method
        FireLaser(); //calling FireLaser() method
    }

    //method for the player's movement
    void PlayerMovement()
    {
        _horizontalMovement = Input.GetAxis("Horizontal"); //connecting variable to input manager
        _verticalMovement = Input.GetAxis("Vertical"); //connecting variable to input manager

        if (canSpeedBoost == true)
        {
            transform.Translate(Vector3.right * _horizontalMovement * _playerSpeed * speedBoost * Time.deltaTime); //moves the player along the x-axis in real time
            transform.Translate(Vector3.up * _verticalMovement * _playerSpeed * speedBoost * Time.deltaTime); //moves the player along the y-axis in real time. 
        }
        else if(canSpeedBoost == false)
        {
            transform.Translate(Vector3.right * _horizontalMovement * _playerSpeed *  Time.deltaTime); //moves the player along the x-axis in real time
            transform.Translate(Vector3.up * _verticalMovement * _playerSpeed * Time.deltaTime); //moves the player along the y-axis in real time.
        }

             

    }

        

    //method for player bounds(limitations) for movement in the game
    void PlayerBounds()
    {
        //horizontal bound(limitations) for player
        if(transform.position.x >= 13.74f)
        {
            transform.position = new Vector3(-13.74f, transform.position.y, 0); //sets the new position and wraps to other side of the screen
        }
        else if(transform.position.x <= -13.74f)
        {
            transform.position = new Vector3(13.74f, transform.position.y, 0); //sets the position and wraps to other side of the screen
        }

        //Vertical bound(limitations) for player
        if(transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if(transform.position.y <= -3.90f)
        {
            transform.position = new Vector3(transform.position.x, -3.90f, 0);
        }
    }

    //method for firing the laser
    void FireLaser()
    {
        //fire the laser after hitting the space key or the right mouse button
        if(Input.GetMouseButtonDown(0) && Time.time > _canFire)
        {
            //play the laser audio clip
            _audioSource.Play();
            _canFire = Time.time + _fireRate; //cool down. can only fire every 0.2 seconds
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.78f, 0), Quaternion.identity); //spawns the laser at the player's current position with no rotation(Quaternion.identity) with an offset of 0.78f above the player
        }

        //fires the triple shot powerup once set to true using the left mouse button
        if(canTripleShot == true && Input.GetMouseButtonDown(1))
        {
            //play the laser audio clip
            _audioSource.Play();
            Instantiate(_leftLaserPrefab, transform.position + new Vector3(-0.76f, -0.46f, 0), Quaternion.identity); //left laser is fired
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.78f, 0), Quaternion.identity); //middle laser is fired
            Instantiate(_rightLaserPrefab, transform.position + new Vector3(0.76f, -0.46f, 0), Quaternion.identity); //right laser is fired

        }

        
     
           
    }

    //method for taking damage
    public void PlayerDamage()
    {
        if(shieldActivated == true)
        {
            return;
        }
        else if(shieldActivated == false)
        {
            _engineFailure[Random.Range(0, _engineFailure.Length)].SetActive(true);
            _playerLives = _playerLives - 1;
           
            
           
        }

        _uiManager.UpdateLives(_playerLives);
        
        //checking if the player has no lives left. If no lives left, then player is dead
        if(_playerLives < 1)
        {
            Instantiate(_playerExplosion, transform.position, Quaternion.identity);
            _spawnManager.OnPlayerDead(); //calling the OnPlayerDead() method from the spawn manager script
             Destroy(this.gameObject);
            
                      
           

        }
        
             
       
    }

   

    //method for enabling the tripleshot powerup once player collects it
    public void TripleShotActive()
    {
       
        //tripleshot active becomes true
        canTripleShot = true;
       
        //start the power down routine for tripleshot
        StartCoroutine(TripleShotPowerDownRoutine());

    }

    //co routine to only allow the tripleshot powerup to be active for only 5 seconds
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    //method for enabling the speed boost powerup once player collects it
    public void SpeedBoostActive()
    {
       
        canSpeedBoost = true;
       
        _playerSpeed = _playerSpeed + speedBoost;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    //coroutine to only allow the speed boost powerup to be active for only 5 seconds
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedBoost = false;
        _playerSpeed = _playerSpeed - speedBoost;
    }

    

    //method for enalbling the shield boost powerup once collected by player
    public void ShieldPowerUpActive()
    {
       
        shieldActivated = true;
       
        _playerShield.SetActive(true);
        StartCoroutine(ShieldPowerDownRoutine());

    }
   
    //coroutine to allow the shield powerup to be active for only 5 seconds
    IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        shieldActivated = false;
        _playerShield.SetActive(false);
        
    }

    //method to add 10 to the player's score
    public void PlayerScore()
    {
        _score += 10; //adds 10 the player's current score

        //calling UpdateScore method from UIManager script
        _uiManager.UpdateScore(_score);

        //load next level
        if(_score == 60)
        {
            StartCoroutine(LoadNewLevel());
        }
    }

    //loading a level after a certain amount of time
    IEnumerator LoadNewLevel()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(2);
    }
   
    

}
