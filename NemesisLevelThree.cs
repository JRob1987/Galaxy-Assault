//this script defines the behavior of the Nemesis enemy ship in level 3.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NemesisLevelThree : MonoBehaviour
{
    [SerializeField] private float _nemesisSpeed; //variable for its speed
    [SerializeField] private GameObject _explosionPrefab;
    PlayerLevelThree _player; //creates a handle to our player script
    // Start is called before the first frame update
    void Start()
    {
        PlayerNullCheck(); //method call 
    }

    // Update is called once per frame
    void Update()
    {
        Movement(); //method call
    }

    //method for null checking the player. Display an error message if so
    void PlayerNullCheck()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerLevelThree>();
        if(_player == null)
        {
            Debug.LogError("The player is null.");
        }
    }

    //nemesis movement
    void Movement()
    {
        //moves nemesis down the screen in real time multiplied by its speed
        transform.Translate(Vector3.down * _nemesisSpeed * Time.deltaTime);
    }

    //method for when nemesis collides with the player or the laser fired from the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks for the laser tag
        if(collision.tag == "Laser")
        {
            Instantiate(_explosionPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Destroy(collision.gameObject);
            _player.UpdatePlayerScore();
            Destroy(this.gameObject);
        }

        //checks for the player tag, and then damages the player if collision is a success.
        if(collision.tag == "Player")
        {
            if(_player != null)
            {
                Instantiate(_explosionPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                Destroy(this.gameObject);
               _player.PlayerDamage();
                
            }
            
        }
    }
}
