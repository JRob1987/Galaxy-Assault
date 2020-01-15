//This is the UI manager script for level 3.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerLevelThree : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text reloadLevelText;
    [SerializeField] private Text _endGameText;
    [SerializeField] private Image _livesImage;
    [SerializeField] private Sprite[] playerLivesSprite;
    GameManagerLevelThree _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "score: " + 0;
        gameOverText.text = "Game Over";
        reloadLevelText.text = "Press R to reload the level.";
        gameOverText.gameObject.SetActive(false);
        reloadLevelText.gameObject.SetActive(false);
        _endGameText.text = "Congratulations Pilot. You defended the galaxy!!";
        _endGameText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManagerLevelThree>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //method to update the UI for player score
    public void UpdatePlayerScoreUI(int playerScore)
    {
        scoreText.text = "score: " + playerScore;

        //when the score reaches 90
        if(playerScore == 90)
        {
            StartCoroutine(EndGameMessage());
        }
    }

    //method to update the UI for player lives
    public void UpdatePlayerLivesUI(int currentLives)
    {
        _livesImage.sprite = playerLivesSprite[currentLives];

        //if the player has no lives
        if (currentLives == 0)
        {
            StartCoroutine(FlashGameOverMessage());
            reloadLevelText.gameObject.SetActive(true);
            _gameManager.GameOver();
        }
    }

    //co routine to flash the game over text when player dies
    IEnumerator FlashGameOverMessage()
    {
        //flashes the game over message every 1.0f seconds
        while(true)
        {
            gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.0f);
        }
        
    }

    //co routine to flash the end game message every 1.0 seconds
    IEnumerator EndGameMessage()
    {
        while(true)
        {
            _endGameText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            _endGameText.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.0f);
        }
       
    }

    
}
