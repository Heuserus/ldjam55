using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneCamera : MonoBehaviour
{
    public GameObject cutSceneController;
    public GameObject gamemaster;

    public GameObject bossCorpse;

    public GameObject Smoke;

    public GameObject Dust;

    public GameObject albonso;

    GameObject albonsoInstance;

    GameObject bossCorpseInstance;

    GameObject smokeInstance;

    GameObject dustInstance;
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
        Debug.Log("ending cutscene2");
        
        cutSceneController.GetComponent<CutsceneController>().endCutscene2();
    }

    public void spawnBossCorpse (){
        bossCorpseInstance =  Instantiate(bossCorpse);
        
    }

    public void startSmoke(){
        smokeInstance= Instantiate(Smoke);
    }

    public void spawnBossDummy(){
        albonsoInstance=Instantiate(albonso);
    }

    public void riseAlbonso(){
        albonsoInstance.GetComponent<Animator>().Play("Rise");
    }
    public void killAlbonso(){
        albonsoInstance.GetComponent<Animator>().Play("Die");
    }

    public void deathDust(){
        Vector3 pos = new Vector3(0,-1,3); 
        dustInstance= Instantiate(Dust,pos,Quaternion.Euler(-90, 0, 0));
    }

    public void endCutscene3(){
        cutSceneController.GetComponent<CutsceneController>().endCutscene3();
    }

    public void cleanUp(){
        Destroy(bossCorpseInstance);
        Destroy(smokeInstance);
        Destroy(albonsoInstance);
        Destroy(dustInstance);
    }
    
}
