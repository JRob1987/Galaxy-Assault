//**this script defines the behavior for the laser in level 3.***

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLevelThree : MonoBehaviour
{

    [SerializeField] private float _laserSpeed = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LaserMovement(); //method call
    }

    //laser movement
    void LaserMovement()
    {
        //moves the laser up in real time
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        //destroy laser once it leaves the screen.
        if(transform.position.y >= 7.19f)
        {
            Destroy(this.gameObject);
        }
    }

    
}
