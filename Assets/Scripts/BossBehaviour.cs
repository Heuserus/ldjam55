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

    public float idleTimeBoss2 = 2f;

    private double lastActionTime;

    public int teleportRange = 30;

    private Vector3 teleportTarget;

    private string[] ActionChoicesBoss1 = new string[]{"Teleport", "MeteorShower", "FireBall"};

    private string[] ActionChoicesBoss2 = new string[]{"Earthquake", "AcidCloud"};

    private int[] ActionWeightsBoss2 = new int[]{500, 5};

    private int[] ActionWeightsBoss1 = new int[]{2,5,5};

    public GameObject MeteorPrefab;

    public GameObject FireBallPrefab;

    public GameObject EarthQuakePrefab;

    private ParticleSystem particleSystem;

    private FistBehaviour fistBehaviour;

    public float Boss2MovementSpeed = 15f;

    public double Boss2SpellPauseDuration = 5f;

    public double Boss2LastSpellTime;


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
        particleSystem = GetComponent<ParticleSystem>();
        fistBehaviour = GameObject.Find("Hands.002").GetComponent<FistBehaviour>();
        Boss2LastSpellTime = Time.timeAsDouble;
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
            else {
                PhaseTwoUpdate();
            }
        }
        
    }

    void PhaseOneUpdate(){
        if (Time.timeAsDouble - lastActionTime < idleTimeBoss1 || isInCast) {
            return;
        }

        string nextAction = getWeightedActionName(ActionChoicesBoss1, ActionWeightsBoss1);

        switch(nextAction){
            case "Teleport":
                // Teleport
                teleportTarget  = new Vector3(UnityEngine.Random.Range(-teleportRange, teleportRange), transform.position.y, UnityEngine.Random.Range(-teleportRange,teleportRange));
                isInCast = true;
                animator.Play("Armature|Cast1");
                particleSystem.Play();
                break;
            case "MeteorShower":
                // Meteor Shower spell
                isInCast = true;
                animator.Play("Armature|Cast2");
                break;
            case "FireBall":
                // Fireball spell
                isInCast = true;
                animator.Play("Armature|Cast3");
                break;
        }
    }

    public void FinishTeleport(){
        Debug.Log("Teleported");
        transform.position  = teleportTarget;
        particleSystem.Stop();
    }

    public void CastMeteorShower(){
        Debug.Log("Meteor Shower casted!");
        int meteorAmount = UnityEngine.Random.Range(5,8);
        for (int i = 0; i <= meteorAmount; i++){
            GameObject.Instantiate(MeteorPrefab, new Vector3(UnityEngine.Random.Range(-teleportRange, teleportRange), 30, UnityEngine.Random.Range(-teleportRange, teleportRange)), Quaternion.identity);
        }
    }

    public void CastFireBall(){
        Debug.Log("Fire ball casted!");
        Vector3 vectorToPlayer = playerObject.transform.position - transform.position;
        // Keep it at ground level
        vectorToPlayer.y = 0;
        GameObject fireball = GameObject.Instantiate(FireBallPrefab);
        fireball.transform.position = transform.position + vectorToPlayer.normalized * 0.5f + new Vector3(0, 1, 0);
        fireball.GetComponent<FireballBehaviour>().SetMovementVector(vectorToPlayer);
    }

    public void FinishAction(){
        lastActionTime = Time.timeAsDouble;
        Boss2LastSpellTime = Time.timeAsDouble;
        isInCast = false;
    }

    void PhaseTwoUpdate(){
        if (Time.timeAsDouble - lastActionTime < idleTimeBoss2 || isInCast) {
            return;
        }

        if ( Vector3.Distance(playerObject.transform.position, transform.position) < 2){
            Debug.Log("Attacking");
            isInCast = true;
            animator.Play("Armature|Attack1");
        }
        else{
            // TODO: Add spell casts
            if (Time.timeAsDouble - Boss2LastSpellTime > Boss2SpellPauseDuration){
                string nextSpell = getWeightedActionName(ActionChoicesBoss2, ActionWeightsBoss2);

                Debug.Log("Casting: " + nextSpell);

                switch(nextSpell){
                    case "Earthquake":
                        // Earthquake
                        animator.Play("Armature|Cast3");
                        isInCast = true;
                        break;
                    case "AcidCloud":
                        // Acid Cloud
                        animator.Play("Armature|Cast4");
                        isInCast = true;
                        break;
                }

            } else{
                // Walk
                Vector3 movementVector = (playerObject.transform.position - transform.position);
                movementVector.y = 0;
                transform.position +=  movementVector.normalized * Boss2MovementSpeed * Time.deltaTime;
            }
        }
    }

    public void SpawnPunch(){
        fistBehaviour.Enable(transform.forward);
    }

    public void EndPunch(){
        fistBehaviour.Disable();
    }

    public void CastEarthquake(){
        Debug.Log("Earthquake");
        for (int i = 0; i < 3; i++){
            GameObject earthquake = GameObject.Instantiate(EarthQuakePrefab);
            earthquake.transform.position = transform.position;
            earthquake.transform.rotation = Quaternion.Euler(new Vector3(earthquake.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, earthquake.transform.rotation.eulerAngles.z));
            earthquake.GetComponent<EarthquakeBehaviour>().SetDirection(transform.forward, 1f - (i * 0.3f));
        }
    }

    public void CastAcidCloud(){
        Debug.Log("Acid Cloud");
    }

    public void Die(){
        gameMaster.cleanUp();
        ui.SetActive(false);
        Destroy(this.gameObject);
        if(phase == 1){
            gameMaster.midScene();
        }
        else{
            gameMaster.endScene();
        }
    }

    private string getWeightedActionName(string[] actionNames, int[] actionWeights){
        int total = actionWeights.Sum();

        int randomValue = UnityEngine.Random.Range(0, total);

        int cursor = 1;

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
