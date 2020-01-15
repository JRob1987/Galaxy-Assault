using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerLevelTwo : MonoBehaviour
{
    [SerializeField] private Text _scoreText; //UI text for the score
    [SerializeField] private Text _gameOverText; //UI text for the game over message 
    [SerializeField] private Text _reloadLevelText;
    [SerializeField] private Text _nextLevelText;
    [SerializeField] Sprite[] _playerLivesSprite; //array to hold the live sprites
    [SerializeField] Image _livesImage; //UI image for the lives
    GameManagerLevelTwo _gameManager;
 
    // Start is called before the first frame update
    void Start()
    {
        //assign text component to the handle
        _scoreText.text = "Score: " + 0;

        //assign text component to the handle
        _gameOverText.text = "Game Over ";
        _gameOverText.gameObject.SetActive(false);
        _reloadLevelText.text = "Press R to reload the level.";
        _reloadLevelText.gameObject.SetActive(false);
        _nextLevelText.text = "Score reached. Moving on to Level 3!";
        _nextLevelText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManagerLevelTwo>();

        if(_gameManager == null)
        {
            Debug.LogError("Game manager is null");
        }
    }

    
    //method to update the playerscore on the UI
    public void UpdatePlayerScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;

        //load the flicker message when score is reached
        if(playerScore == 80)
        {
            StartCoroutine(NextLevelMessage());
            _nextLevelText.gameObject.SetActive(true);
        }
    }

    //method for updating the UI images for player lives
    public void UpdateUILivesImages(int currentLives)
    {
        _livesImage.sprite = _playerLivesSprite[currentLives];

        if(currentLives == 0)
        {
            _gameManager.GameOver();
            StartCoroutine(FlashGameOverMessage());
            _reloadLevelText.gameObject.SetActive(true);
        }
    }

   
    //co routine to display the gameover message with a flash/blinking effect
    IEnumerator FlashGameOverMessage()
    {
        while(true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.0f);
        }
    }

    //co routine to display next level text with a flickering effect.
    IEnumerator NextLevelMessage()
    {
        while(true)
        {
            _nextLevelText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            _nextLevelText.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.0f);
        }
    }

    

    
}
