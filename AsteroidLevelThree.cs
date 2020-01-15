//this script defines the behavior of the asteroid in level 3

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLevelThree : MonoBehaviour
{
    [SerializeField] private float _asteroidSpeed;
    [SerializeField] private GameObject _explosionPrefab;
    PlayerLevelThree _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerLevelThree>();
        if(_player == null)
        {
            Debug.LogError("the player is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMovement(); //method call
    }
    
    //asteroid movement
    void AsteroidMovement()
    {
        //moves asteroid down the screen in real time
        transform.Translate(Vector3.down * _asteroidSpeed * Time.deltaTime);
    }

    //method when asteroid collides with the player or laser fired from player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
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
            Destroy(this.gameObject);
        }
    }

}
