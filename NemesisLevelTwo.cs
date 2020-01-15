//this script defines the behavior of the Nemesis gameobject in level 2.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NemesisLevelTwo : MonoBehaviour
{
    [SerializeField] private float nemesisSpeed = 5.0f; //nemesis speed
    [SerializeField] private GameObject _explosion;
    PlayerLevelTwo _player; //handle to the playerleveltwo script

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerLevelTwo>();
    }

    // Update is called once per frame
    void Update()
    {
        NemesisMovement(); //method call
    }

    //method for nemesis movement
    void NemesisMovement()
    {
        //moves nemesis down the screen in real time
        transform.Translate(Vector3.down * nemesisSpeed * Time.deltaTime);
    }

    //method when Nemesis collides with player or laser
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(_player != null)
            {
                Instantiate(_explosion, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                _player.PlayerDamage();
            }
           
            Destroy(this.gameObject);
        }
        else if(collision.tag == "Laser")
        {
            Destroy(collision.gameObject);
            if(_player != null)
            {
               Instantiate(_explosion, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);

                //add 10 to the player's score
                _player.AddToPlayerScore();
            }
           
            Destroy(this.gameObject);
           
            
        }
    }
}
