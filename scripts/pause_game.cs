using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pause_game : MonoBehaviour
{
    public bool gameIsPaused;
    public GameObject pause_ui;
    public GameObject plane;
    private void Awake()
    {
        pause_ui.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
        if(!gameIsPaused)
        {
            pause_ui.SetActive(false);
            Cursor.visible = false;
        }
        else
        {
            pause_ui.SetActive(true);
            PauseGame();
            Cursor.visible = true;
        }
       
        if(plane.GetComponent<Damageable>().ami_morte_jassi)
        {
            plane.GetComponent<Damageable>().ami_more_gesi = true;
            //
            gameIsPaused=true;
        }
    }
    void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            pause_ui.SetActive(true);
        }
        else
        {
           
            Time.timeScale = 1;
            pause_ui.SetActive(false);
        }
    }
}
