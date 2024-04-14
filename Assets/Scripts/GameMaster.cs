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

    public float Boss1Health;
    public float Boss2Health;

    public KeyCode pauseKey;
    public GameObject pauseMenu;

    public GameObject playerCam;

    

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
            case GameState.phase1:
            case GameState.phase2:
            if(Input.GetKeyDown(pauseKey)){
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                pauseMenu.SetActive(true);
                pauseMenu.GetComponent<PauseMenu>().setResumeState(state);
                state = GameState.paused;
            }
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
        state = GameState.midScene;
        cutsceneController.GetComponent<CutsceneController>().boss2 = Boss2;
        cutsceneController.GetComponent<CutsceneController>().playScene2();
    }

    public void endScene(){
        state = GameState.ending;
        cutsceneController.GetComponent<CutsceneController>().playScene3();
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
        Boss1.GetComponent<BossBehaviour>().maxHealth = Boss1Health;
        Boss1.GetComponent<BossBehaviour>().phase = 1;
        
    }
    public void spawnBoss2(){
        Boss2 = Instantiate(Boss2Prefab);
        Boss2.GetComponent<BossBehaviour>().gameMasterObj = this.gameObject;
        Boss2.GetComponent<BossBehaviour>().healthBar = Boss2Healthbar;
        Boss2.GetComponent<BossBehaviour>().ui = boss2Ui;
        Boss2.GetComponent<BossBehaviour>().maxHealth = Boss2Health;
        Boss1.GetComponent<BossBehaviour>().phase = 2;
        
    }

    public void cleanUp(){
        GameObject.Find("Zoom").GetComponent<Image>().enabled = false;
        playerCam.GetComponent<Camera>().fieldOfView =60;
    }
}
