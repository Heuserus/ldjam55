using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GameMaster.GameState resumeState;
    public GameObject gameMaster;

    public GameObject settingsobj;
    public void Resume(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameMaster.GetComponent<GameMaster>().state = resumeState;
        this.gameObject.SetActive(false);
    }
    public void setResumeState(GameMaster.GameState s){
        resumeState = s;
    }
    public void backToMenu(){
        SceneManager.LoadScene(0);
    }
    public void settings(){
        settingsobj.SetActive(true);
        this.gameObject.SetActive(false);
    }


}
