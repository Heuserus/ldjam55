using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsobj;
    public void play(){
        SceneManager.LoadScene(1);
    }
    public void quit(){
        Application.Quit();
    }
    public void settings(){
        settingsobj.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
