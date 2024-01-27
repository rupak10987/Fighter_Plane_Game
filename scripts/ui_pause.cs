using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ui_pause : MonoBehaviour
{
    public GameObject brain;
    public void resume()
    {
        bool paused = brain.GetComponent<pause_game>().gameIsPaused;
        if(paused)
        {
            Time.timeScale = 1f;
            brain.GetComponent<pause_game>().gameIsPaused = !brain.GetComponent<pause_game>().gameIsPaused;
            
        }
    }
    public void main_menu()
    {
        Cursor.visible = true;
        brain.GetComponent<pause_game>().gameIsPaused = !brain.GetComponent<pause_game>().gameIsPaused;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void quit_game()
    {
        Application.Quit();
    }
}

