using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneCamera : MonoBehaviour
{
    public GameObject cutSceneController;
    public GameObject gamemaster;
    public void spawnBoss1(){
        gamemaster.GetComponent<GameMaster>().spawnBoss1();
        
    }
    public void endCutscene1(){
        cutSceneController.GetComponent<CutsceneController>().endCutscene1();
        
    }
}
