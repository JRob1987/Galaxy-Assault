//this script defines the behavior of the laser in level 2.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLevelTwo : MonoBehaviour
{
    [SerializeField] private float _laserSpeed = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LaserMovement();
    }

    //method for laser movement
    void LaserMovement()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        //Destory laser once it leaves the screen
        if(transform.position.y >= 9.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
