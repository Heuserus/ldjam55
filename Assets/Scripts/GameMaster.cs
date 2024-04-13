using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    public GameObject player;
    public GameObject Boss1Prefab;
    public GameObject Boss1;
    public GameObject Boss2Prefab;
    public GameObject Boss2;

    enum GameState{
        opening,
        phase1,
        midScene,
        phase2,
        ending,
        death,
        paused,
    }

    GameState state = GameState.opening;
    // Start is called before the first frame update
    void Start()
    {
        spawnBoss1();
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case GameState.opening:

            break;

        }
        
    }

    private void spawnBoss1(){
        Boss1 = Instantiate(Boss1Prefab);
    }
}
