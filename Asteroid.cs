//script defines the behavior of the Asteroid in Level 1 of the game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    //Variables
     [SerializeField] private float asteroidSpeed = 2.0f; //movement speed
    [SerializeField] private GameObject _explosionPrefab;
    private Player _player; //creating a handle to the player
    
   
    
    


    // Start is called before the first frame update
    void Start()
    {

       _player = GameObject.Find("Player").GetComponent<Player>(); //accessing the player component
        

        

    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMovement(); 

    }

    //method for Asteroid movement
    void AsteroidMovement()
    {
       // moving asteroid down
      transform.Translate(Vector3.down * asteroidSpeed * Time.deltaTime);
    }

    

    //method for when the asteroid gets hit by the laser. As a result it will explode
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
           
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.15f);
        }

        if(other.tag == "Player")
        {

            
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _player.PlayerDamage();
            Destroy(this.gameObject, 0.15f);

        }
    }
}
