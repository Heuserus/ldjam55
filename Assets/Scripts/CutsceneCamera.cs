using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneCamera : MonoBehaviour
{
    public GameObject cutSceneController;
    public GameObject gamemaster;

    public GameObject bossCorpse;
    public void spawnBoss1(){
        gamemaster.GetComponent<GameMaster>().spawnBoss1();
        
    }
    public void endCutscene1(){
        cutSceneController.GetComponent<CutsceneController>().endCutscene1();

    }

    public void spawnBoss2(){
        gamemaster.GetComponent<GameMaster>().spawnBoss2();
    }

    public void endCutscene2(){
        cutSceneController.GetComponent<CutsceneController>().endCutscene2();
    }

    public void spawnBossCorpse (){
        Instantiate(bossCorpse);
    }
}
