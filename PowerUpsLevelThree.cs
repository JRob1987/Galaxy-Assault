//this script defines the behavior for the powerups in level 3

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsLevelThree : MonoBehaviour
{
    [SerializeField] private float _powerUpsSpeed; //variable for the speed of the powerups
    //[SerializeField] private GameObject[] _powerUps;
    [SerializeField] private int powerUpID; //0 for speed, 1 for tripleshot powerup,  2 for shield powerup
    PlayerLevelThree _player; //getting handle to the player
    [SerializeField] private AudioClip _powerUpClips;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerLevelThree>();
        if(_player == null)
        {
            Debug.LogError("Player is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    //powerup movment method
    void Movement()
    {
        transform.Translate(Vector3.down * _powerUpsSpeed * Time.deltaTime);
    }

    //method when powerups collide with the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_powerUpClips, transform.position);
            switch (powerUpID)
            {
                case 0:
                    _player.ActivateSpeedBoost();
                    Debug.Log("Speed boost powerup connected");
                    Destroy(this.gameObject);
                    break;

                case 1:
                    _player.ActivateTripleShot();
                    Debug.Log("Triple shot powerup connected");
                    Destroy(this.gameObject);
                    break;

                case 2:
                    _player.ActivateShieldPowerUp();
                    Debug.Log("Shield powerup connected");
                    Destroy(this.gameObject);
                    break;

                
                default:
                    Debug.Log("Invalid powerup");
                    break;
            }
        }
    }
}
