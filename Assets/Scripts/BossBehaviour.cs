using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour
{
    
    public float maxHealth = 10f;
    public float health;
    public Image healthBar;

    public GameObject ui;

    public GameObject gameMasterObj;

    public Animator animator;

    GameMaster gameMaster;
    public int phase;

    private bool isInCast;

    private GameObject playerObject;

    public float idleTimeBoss1 = 1.5f;

    private double lastActionTime;

    public int teleportRange = 30;

    private Vector3 teleportTarget;

    private string[] ActionChoicesBoss1 = new string[]{"Teleport", "MeteorShower"};

    private int[] ActionWeightsBoss1 = new int[]{2,2};


    public void Damage(float Damage){
        health -= Damage;
        healthBar.fillAmount = health / maxHealth;
        
        if(health <= 0){
            Die();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ui.SetActive(true);
        gameMaster = gameMasterObj.GetComponent<GameMaster>();
        health = maxHealth;
        isInCast = false;
        playerObject =  GameObject.Find("Player");
        lastActionTime = Time.timeAsDouble;
    }

    // Update is called once per frame
    void Update()
    {

        if(gameMaster.state == GameMaster.GameState.phase1||gameMaster.state == GameMaster.GameState.phase2){

            // turn to player
            if (!isInCast){
                transform.LookAt( new Vector3(playerObject.transform.position.x, transform.position.y, playerObject.transform.position.z));
            }

            if(phase == 1){
                PhaseOneUpdate();
            }
        }
        
    }

    void PhaseOneUpdate(){
        if (Time.timeAsDouble - lastActionTime < idleTimeBoss1 || isInCast) {
            return;
        }

        string nextAction = getWeightedActionName(ActionChoicesBoss1, ActionWeightsBoss1);

        Debug.Log(nextAction);

        switch(nextAction){
            case "Teleport":
                // Teleport
                teleportTarget  = new Vector3(UnityEngine.Random.Range(-teleportRange, teleportRange), transform.position.y, UnityEngine.Random.Range(-teleportRange,teleportRange));
                isInCast = true;
                animator.Play("Armature|Cast1");
                break;
            case "MeteorShower":
                // Meteor Shower spell
                isInCast = true;
                animator.Play("Armature|Cast2");
                break;
        }
    }

    public void FinishTeleport(){
        Debug.Log("Teleported");
        transform.position  = teleportTarget;
    }

    public void CastMeteorShower(){
        Debug.Log("Meteor Shower casted!");
    }

    public void FinishAction(){
        lastActionTime = Time.timeAsDouble;
        isInCast = false;
    }

    void PhaseTwoUpdate(){

    }

    public void Die(){
        
        ui.SetActive(false);
        Destroy(this.gameObject);
        if(phase == 1){
            gameMaster.midScene();
        }
        else{
            gameMaster.endScene();
        }
    }

    private float GetAnimationDuration(string animationName){
        AnimationClip clip = (animator.runtimeAnimatorController.animationClips).First(clip => clip.name == animationName);
        return  clip.length; 
    }

    private string getWeightedActionName(string[] actionNames, int[] actionWeights){
        int total = actionWeights.Sum();

        int randomValue = UnityEngine.Random.Range(0, total);

        Debug.Log(randomValue);

        int cursor = 0;

        for (int i = 0; i < actionWeights.Length; i++){
            cursor += actionWeights[i];
            if (cursor >= randomValue){
                return actionNames[i];
            }
        }
        // This should never happen!
        Debug.Log("Illegal case for weapon selection! Fallback to element 0");
        return actionNames.First();
    }
}
