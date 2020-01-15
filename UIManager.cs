using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    //create a handle to text
    [SerializeField] private Text _scoreText;

    [SerializeField] private Sprite[] _liveSprites;
    [SerializeField] private Image _livesImage;

    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _reloadText;
    [SerializeField] private Text _nextLevelText;
    private GameManager _gameManager;
   

    // Start is called before the first frame update
    void Start()
    {
        //assign text component to the handle
        _scoreText.text = "Score: " + 0;
        _gameOverText.text = "Game Over";
        _gameOverText.gameObject.SetActive(false); //turning off the object when the game starts
        _reloadText.text = "Press R to reload the level";
        _reloadText.gameObject.SetActive(false);
        _nextLevelText.text = "Score reached. Moving on the Level 2!";
        _nextLevelText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>(); //telling UIManager to find the game manager object and component

        //null checking to see if the Game Manager is null
        if (_gameManager == null)
        {
            Debug.Log("Game manager is null");
        }
       


    }

       
    //method to UpdateScore with playerScore as a parameter variable
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;

        if(playerScore == 60)
        {
            StartCoroutine(NextLevelMessage());
            _nextLevelText.gameObject.SetActive(true);
        }

    }
    //method for updating player lives
    public void UpdateLives(int currentLives)
    {
        //access the display image sprite, and give it a new one based on the currentLives index
        _livesImage.sprite = _liveSprites[currentLives];

        //if the player's lives becomes zero, display the game over message.
       if (currentLives == 0)
         {
            //Game Over Sequence
            _gameManager.GameOver();
           // _gameOverText.gameObject.SetActive(true);
            StartCoroutine(GameOverMessageFlicker()); //calling corotuine
            _reloadText.gameObject.SetActive(true);
           
                                           
                       
         }

             

    }

    //co routine to flicker the game over text off and display try again every 0.5 seconds after the player dies.
    IEnumerator GameOverMessageFlicker()
    {
        while(true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.0f);
            
        }
    }

    //co routine to flicker the next level text
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


    

    

