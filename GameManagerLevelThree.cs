using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerLevelThree : MonoBehaviour
{
    [SerializeField] private bool _isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ReloadLevelThree();
    }

    //method to reload level 3 scene
    void ReloadLevelThree()
    {
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(3);
        }

    }

    //method to _isGameOver to true
    public void GameOver()
    {
        _isGameOver = true;
    }
}
