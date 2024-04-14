using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{

    public GameObject player;
    public GameObject Boss1Prefab;
    public GameObject Boss1;
    public GameObject Boss2Prefab;
    public GameObject Boss2;

    public Image BossHealthbar;

    public Image Boss2Healthbar;
    
    public GameObject boss1Ui;

    public GameObject boss2Ui;
    public GameObject cutsceneController;

    public enum GameState{
        opening,
        phase1,
        midScene,
        phase2,
        ending,
        death,
        paused,
    }

    public GameState state = GameState.opening;
    // Start is called before the first frame update
    void Start()
    {
        
        doOpening();
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case GameState.opening:
            break;

        }
        
    }

    private void doOpening(){
        spawnBoss1();
        cutsceneController.GetComponent<CutsceneController>().boss1 = Boss1;
        cutsceneController.GetComponent<CutsceneController>().playScene1();
    }

    public void midScene(){
        spawnBoss2();
        cutsceneController.GetComponent<CutsceneController>().boss2 = Boss2;
        cutsceneController.GetComponent<CutsceneController>().playScene2();
    }

    public void startPhase1(){
        player.GetComponent<WeaponArm>().state = WeaponArm.WeaponState.startUp;
        state = GameState.phase1;
    }
    public void startPhase2(){
        //player.GetComponent<WeaponArm>().state = WeaponArm.WeaponState.startUp;
        state = GameState.phase2;
    }

    public void spawnBoss1(){
        Boss1 = Instantiate(Boss1Prefab);
        Boss1.GetComponent<BossBehaviour>().gameMasterObj = this.gameObject;
        Boss1.GetComponent<BossBehaviour>().healthBar = BossHealthbar;
        Boss1.GetComponent<BossBehaviour>().ui = boss1Ui;
        
    }
    public void spawnBoss2(){
        Boss2 = Instantiate(Boss2Prefab);
        Boss2.GetComponent<BossBehaviour2>().gameMasterObj = this.gameObject;
        Boss2.GetComponent<BossBehaviour2>().healthBar = Boss2Healthbar;
        Boss2.GetComponent<BossBehaviour2>().ui = boss2Ui;
        
    }
}
