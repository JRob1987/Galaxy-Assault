//This script defines the behavior of the powerups in level 2

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsLevelTwo : MonoBehaviour
{
    [SerializeField] private float _powerUpSpeed = 3.0f; //powerup's speed
    [SerializeField] private int powerupID; //0 = tripleshot powerup, 1 = speed powerup, 2 = shield powerup
    PlayerLevelTwo _player; //creating a handle to the player
    [SerializeField] private AudioClip _powerUpClips;

    // Start is called before the first frame update
    void Start()
    {
        //starting position
        //transform.position = new Vector3(0, 7.59f, 0);

        _player = GameObject.Find("Player").GetComponent<PlayerLevelTwo>();


    }

    // Update is called once per frame
    void Update()
    {
        PowerupMovement(); //calling method
    }

    //method for powerups movement
    void PowerupMovement()
    {
        //moves powerups down in real time
        transform.Translate(Vector3.down * _powerUpSpeed * Time.deltaTime);
    }

    //method for when powerups collide with the player
    private void OnTriggerEnter2D(Collider2D collision)
    {

      if (collision.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_powerUpClips, transform.position);
            switch (powerupID)
            {
                case 0:
                    _player.ActivateTripleShot();
                    Destroy(this.gameObject);
                    break;

                case 1:
                    _player.ActivateSpeedBoost();
                    Destroy(this.gameObject);
                    break;

                case 2:
                    _player.ActivateShieldPowerup();
                    Destroy(this.gameObject);
                    break;

                default:
                    Debug.Log("Default message");
                    break;

            }
        }
    }
}
