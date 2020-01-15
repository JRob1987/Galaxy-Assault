//This script enables the user to restart the game by pressing R after the player dies

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _isGameOver;
    Player player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Update()
    {
        
        RestartLevel(); //method call
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    //restart level
    void RestartLevel()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1); //current game scene which is Level 1
        }
    }
    //load next level
    void LoadNextLevel()
    {
        player.PlayerScore();
    }
}


