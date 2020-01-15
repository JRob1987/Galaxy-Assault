using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Ending : MonoBehaviour
{
    public void ReloadGame()
    {
        //reload game back at the main menu
        SceneManager.LoadScene(0);
    }
}
