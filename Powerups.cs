//**this script defines the behavior of the powerups in level 1 of the game.***

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    //[SerializeField] private GameObject _tripleShotPowerupPrefab;
    [SerializeField] private float _PowerUpSpeed = 3.0f;
    [SerializeField] private int powerUpID; // 0 = triple shot powerup, 1 = speed powerup, 2 = shield powerup
    [SerializeField] private AudioClip _powerUpClip;
   
   
   
              
    // Update is called once per frame
    void Update()
    {
        PowerUpMovement();
    }

    //method for triple shot powerup movement
    void PowerUpMovement()
    {
        //moving the tripleshot powerup down at the speed of 3
        transform.Translate(Vector3.down * _PowerUpSpeed * Time.deltaTime);

        //if the powerup leaves the screen, destroy the object
        if (transform.position.y <= -5.45f)
        {
            Destroy(this.gameObject);
        }


    }

    

    //method for when player collects the tripleshot powerup
    private void OnTriggerEnter2D(Collider2D other)
    {
        //checking if the tag is the player
        if (other.tag == "Player")
        {
           
            Player player = GameObject.Find("Player").GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_powerUpClip, transform.position);

            switch(powerUpID)
            {
                case 0:
                    
                    player.TripleShotActive();
                    Debug.Log("Triple shot powerup collected");
                    break;
                case 1:
                   
                    player.SpeedBoostActive();
                    Debug.Log("Speed powerup collected");
                    break;
                case 2:
                   
                    player.ShieldPowerUpActive();
                    Debug.Log("Shield powerup collected");
                    break;
                default:
                    Debug.Log("Default Value");
                    break;
            }
            //destroy the powerup object once collected by the player
            Destroy(this.gameObject); 


           
        }
    }

}
   
    
    
