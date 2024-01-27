using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loader:MonoBehaviour
{
 public void load()
    {
        Debug.Log("clicker");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void quit_app()
    {
        Application.Quit();
    }
}
