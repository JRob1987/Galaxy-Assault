//this script defines the behavior of the enemy in level 2.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevelTwo : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 3.0f; //enemy speed
    [SerializeField] private GameObject _explosion;
    PlayerLevelTwo _player; //handle to the playerleveltwo script
  

    // Start is called before the first frame update
    void Start()
    {
        //EnemyStartingPosition(); //calling method
        _player = GameObject.Find("Player").GetComponent<PlayerLevelTwo>();

        
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement(); //calling method
    }

    
    //method for enemy movement
    void EnemyMovement()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);
    }

    //method for when enemy makes contact with the player or laser
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(_player != null)
            {
                
                Instantiate(_explosion, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                _player.PlayerDamage(); //calls the playerdamage() from the playerleveltwo script to subtract 1 life from the player
            }
           
            Destroy(this.gameObject);
            
        }
        else if(collision.tag == "Laser")
        {
           
            Destroy(collision.gameObject);
            Instantiate(_explosion, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            //add 10 to score
            _player.AddToPlayerScore();
            
            Destroy(this.gameObject);
            
           
            
            
        }
    }
}
