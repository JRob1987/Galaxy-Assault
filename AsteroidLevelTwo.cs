//this script defines the behavior of the asteroid in level 2 of the game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLevelTwo : MonoBehaviour
{
    [SerializeField] private float _asteroidSpeed; //asteroid's speed
    [SerializeField] private GameObject _explosionPrefab;
    PlayerLevelTwo _player;
    

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerLevelTwo>();
        if(_player == null)
        {
            Debug.LogError("Player is null");
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        Movement(); //method call 
    }

    //Asteroid's movement
    void Movement()
    {
        //moves down the screen in real time
        transform.Translate(Vector3.down * _asteroidSpeed * Time.deltaTime);
    }

    //collision with player or laser
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
           
            Destroy(collision.gameObject);
            Instantiate(_explosionPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }

        else if (collision.tag == "Player")
        {
            if (_player != null)
            {
                Instantiate(_explosionPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                _player.PlayerDamage();
                Destroy(this.gameObject);
            }

        }

    }
}
