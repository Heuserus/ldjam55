using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
    public GameObject bars;
    public GameObject boss1;
    public GameObject boss2;

    public GameObject gameUI;

    public Camera cutsceneCam;
    public Camera playerCam;

    public KeyCode skipKey;

    public GameObject gameMaster;

    enum sceneState{
        none,
        scene1,
        scene2
    }

    sceneState state;

    public void playScene1(){

        state = sceneState.scene1;
        
        gameUI.SetActive(false);
        bars.SetActive(true);
        
        playerCam.enabled = false;
        cutsceneCam.enabled = true;
    }

    public void playScene2(){

        state = sceneState.scene2;
        
        gameUI.SetActive(false);
        bars.SetActive(true);
        
        playerCam.enabled = false;
        cutsceneCam.enabled = true;
    }

    public void endCutscene1(){
        
        gameUI.SetActive(true);
        bars.SetActive(false);
        playerCam.enabled = true;
        cutsceneCam.enabled = false;
        gameMaster.GetComponent<GameMaster>().startPhase1();
        state = sceneState.none;

    }
    public void endCutscene2(){
        gameUI.SetActive(true);
        bars.SetActive(false);
        playerCam.enabled = true;
        cutsceneCam.enabled = false;
        gameMaster.GetComponent<GameMaster>().startPhase2();
        state = sceneState.none;
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
        }
        
    }
}
