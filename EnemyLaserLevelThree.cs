//defines the behavior of the enemy laser in level 3 of the game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserLevelThree : MonoBehaviour
{
    [SerializeField] private float _speed; //enemy laser speed

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LaserMovement();
    }

    //laser movement
    void LaserMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //destroy laser when off the screen
        if(transform.position.y <= -5.3f)
        {
            Destroy(this.gameObject);
        }
    }
}
