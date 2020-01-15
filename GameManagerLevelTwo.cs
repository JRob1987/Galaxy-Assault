using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerLevelTwo : MonoBehaviour
{
    [SerializeField] private bool _isGameOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ReloadSceneTwo(); //calling method
    }

    //method to reload level 2
    public void ReloadSceneTwo()
    {
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(2);
            
        }
    }

    //method to set isGameOver to true
    public void GameOver()
    {
        _isGameOver = true;
    }

}
