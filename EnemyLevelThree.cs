//This script defines the behavior for level 3 of the game.**

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevelThree : MonoBehaviour
{
    //variables
    [SerializeField] private float _enemySpeed; //variable for enemy speed
     PlayerLevelThree _player; //creates a handle to our player
     [SerializeField] private GameObject _enemyLaserPrefab; //variable for our enemy laser prefab. 
    [SerializeField] private float _fireRate; //fire fate
    [SerializeField] private float nextFire; //determines when the enemy ship can fire a laser after a certain amount of time.
    [SerializeField] private GameObject _explosionPrefab;
    
  
   


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerLevelThree>(); //locating our player
        //if player is null. print a log error message
        if(_player == null)
        {
            Debug.LogError("The player is null");
        }

       

       

       
    }

    // Update is called once per frame
    void Update()
    {
        Movement(); //calling method
        EnemyFire();
       
    }

    //Enemy Movement
    void Movement()
    {
        //moves the enemy in real time. 
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);
    }

    //when enemy collides with the player or the laser
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //if player is not null then destroy this gameobject and damage the player
            if(_player != null)
            {
                Instantiate(_explosionPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                Destroy(this.gameObject);
               _player.PlayerDamage();
            }
           
        }
        else if(other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
             Destroy(other.gameObject);
             _player.UpdatePlayerScore();
             Destroy(this.gameObject);
            
            
        }

        

    }

    //Enemy firing laser method
    void EnemyFire()
    {
        
            if (Time.time > nextFire)
            {
                nextFire = Time.time + _fireRate;
                Instantiate(_enemyLaserPrefab, transform.position + new Vector3(0, -1.22f, 0), Quaternion.identity);

            }
        
        
        
    }

    
   
    
    

}
