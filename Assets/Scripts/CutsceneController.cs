using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
    public GameObject[] bars;
    public GameObject boss1;
    public GameObject boss2;

    public GameObject[] gameUI;

    public Camera cutsceneCam;
    public Camera playerCam;

    public KeyCode skipKey;

    public GameObject gameMaster;

    public GameObject WinScreen;

    enum sceneState{
        none,
        scene1,
        scene2,
        scene3
    }

    sceneState state;

    public void setGameUIActive(bool x){
        foreach (var ui in gameUI)
        {
            ui.SetActive(x);
        }
    }
    public void setBarsActive(bool x){
        foreach (var ui in bars)
        {
            ui.SetActive(x);
        }
    }
    public void playScene1(){

        state = sceneState.scene1;
        
        setGameUIActive(false);
        setBarsActive(true);
        
        playerCam.enabled = false;
        cutsceneCam.enabled = true;
    }

    public void playScene2(){

        state = sceneState.scene2;
        
        setGameUIActive(false);
        setBarsActive(true);
        
        playerCam.enabled = false;
        cutsceneCam.enabled = true;
    }

    public void playScene3(){
        state = sceneState.scene3;
        
        setGameUIActive(false);
        setBarsActive(true);
        
        playerCam.enabled = false;
        cutsceneCam.enabled = true;
    }

    public void endCutscene1(){
        
        setGameUIActive(true);
        setBarsActive(false);
        playerCam.enabled = true;
        cutsceneCam.enabled = false;
        gameMaster.GetComponent<GameMaster>().startPhase1();
        state = sceneState.none;

    }
    public void endCutscene2(){
        setGameUIActive(true);
        setBarsActive(false);
        playerCam.enabled = true;
        cutsceneCam.enabled = false;
        gameMaster.GetComponent<GameMaster>().startPhase2();
        state = sceneState.none;
    }
    public void endCutscene3(){
        WinScreen.SetActive(true);
        setBarsActive(false);
        state = sceneState.none;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    void Update(){
        switch(state){
            case sceneState.none:
            break;
            case sceneState.scene1:
            if(Input.GetKeyDown(skipKey)){
                endCutscene1();
            }
            break;
            case sceneState.scene2:
            if(Input.GetKeyDown(skipKey)){
                endCutscene2();
            }
            break;
            case sceneState.scene3:
            if(Input.GetKeyDown(skipKey)){
                endCutscene3();
            }
            break;
        }
        
    }
}