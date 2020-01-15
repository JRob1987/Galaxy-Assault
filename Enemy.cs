//**this script is for the behavior of the enemy in the game***
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //variables and initializations
    [SerializeField] private float enemySpeed = 4.0f;

    private Player _player; //creating a variable to access the Player globally

    //create a handle to the animator component
    private Animator _enemyExplosionAnimator;

    private AudioSource _audioSource;

        
    // Start is called before the first frame update
    void Start()
    {
        //enemy starting position when the game begins
        //transform.position = new Vector3(0, 9.0f, 0);
        _player = GameObject.Find("Player").GetComponent<Player>(); //accessing the player globally
        //null check the player
        if(_player == null)
        {
            Debug.LogError("Player is null");
        }

        //assign the component to Anim
        _enemyExplosionAnimator = GetComponent<Animator>();

        //null check the enemy animator
        if(_enemyExplosionAnimator == null)
        {
            Debug.LogError("Enemy Animator is null");
        }

        _audioSource = GetComponent<AudioSource>();



    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement(); //calling EnemyMovement() method
        EnemyRespawn(); //Calling EnemyRespawn() method
    }

    //method for enemy movement
    void EnemyMovement()
    {
        //moves the enemy down 4 meters per second in real time
        transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);
    }

    //method for Enemy Respawn going off the screen
    void EnemyRespawn()
    {
        //checking if the enemy reached this value off the screen on the y axis
        if (transform.position.y <= -5.64f)
        {
            //respawns the enemy at a random x value between -16.27 and 16.27 at position 6.63 on the y
            transform.position = new Vector3(Random.Range(-9.45f, 9.45f), 6.63f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
                      
        //if the tag is a Laser
        if(other.gameObject.tag == "Laser")
        {
            Destroy(other.gameObject); //destroy the laser tag

            //Access player data and then add 10 to score
             _player.PlayerScore();

            //trigger anim
            _enemyExplosionAnimator.SetTrigger("OnEnemyDeath");
            enemySpeed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 1.0f); //destroy the enemy 
            

           

        }
        
        //if the tag is the Player
       else if(other.gameObject.tag == "Player")
        {
                       
           
            //checking to see if the player component exists before damaging the player
            if(_player != null)
            {
                _player.PlayerDamage();
            }

            //trigger anim
            _enemyExplosionAnimator.SetTrigger("OnEnemyDeath");
            //enemySpeed = 1;
            //destroy the enemy after collding with the player
            _audioSource.Play();
            Destroy(this.gameObject, 1.0f);
            

            


        }

        

        
        
    }

}
