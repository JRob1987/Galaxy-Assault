//**This script defines the behavior of the laser in the game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //variables and initializations
    [SerializeField] private float _laserSpeed = 8.0f;

 
   
    // Update is called once per frame
    void Update()
    {
        LaserMovement(); //calling LaserMovement() method
        DestroyLaser(); //calling DestroyLaser() method
    }

    //Method for moving the laser in an updward direction after being fired by the player
    void LaserMovement()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime); //fires the laser up by 8.0f in real time
    }

    //method for destroying the laser when it leaves the screen
    void DestroyLaser()
    {
        //destroying the laser once it leaves the game scene. 6.9f is the top most y position on the screen
        if(transform.position.y >= 6.9f)
        {
            //if this object has a parent destroy it
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

}
