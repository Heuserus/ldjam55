using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponArm : MonoBehaviour
{

    public Weapon[] weapons;
    public Weapon weapon;
    float coolDown;
    float shootingTime;

    float secondaryTime;

    float secondaryCooldown;

    float summoningTime;

    public GameObject playerCam;
    public GameObject gameMasterObj;
    public GameMaster gameMaster;

    public GameObject summoningCircle;

    public GameObject summoningCirclePrefab;

    public Config config;

    

    public enum WeaponState {
        ready,
        coolDown,
        startUp,
        shooting,
        secondary,
        secondaryCooldown,
        summoning,
        none

    }

    public WeaponState state = WeaponState.none;

    public KeyCode fire;
    public KeyCode secondary;

    public int flaskCount = 0;
    public GameObject FlaskUI;

    private int[] weaponScores;

    void Start(){
        gameMaster = gameMasterObj.GetComponent<GameMaster>();
        weaponScores = Enumerable.Repeat(0, weapons.Length).ToArray();

        if(config.assist){
            flaskCount = 15;
        }
        else{
            flaskCount = 5;
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        if(gameMaster.state == GameMaster.GameState.phase1||gameMaster.state == GameMaster.GameState.phase2){
        switch(state){
            case WeaponState.ready:
            
            if(Input.GetKey(fire)){
                
                if(weapon.currentAmmo== 0){
                    summonNewWeapon();
                    state = WeaponState.summoning;
                }
                else{
                weapon.shoot();
                state = WeaponState.shooting;
                shootingTime = weapon.shootingTime;
                }
                
            }
            if(Input.GetKeyDown(secondary)){
                
                weapon.secondary();
                state = WeaponState.secondary;
                secondaryTime = weapon.secondaryTime;
            }
            break;
            case WeaponState.shooting:
                if(shootingTime > 0){
                    shootingTime -= Time.deltaTime;
                }
                else{
                    state = WeaponState.coolDown;
                    coolDown = weapon.coolDownTime;
                }
            break;
            case WeaponState.coolDown:
                if(coolDown > 0){
                    coolDown -= Time.deltaTime;
                }
                else{
                    state = WeaponState.ready;
                }
                if(Input.GetKeyDown(secondary)){
                
                weapon.secondary();
                state = WeaponState.secondary;
                secondaryTime = weapon.secondaryTime;
                }
            break;
            case WeaponState.startUp:
                weapon.weaponInstance = Instantiate(weapon.weaponPrefab,playerCam.transform);
                weapon.weaponInstance.SetActive(true);
                weapon.model = weapon.weaponInstance.transform.Find("Holder/Model").gameObject;
                weapon.playerCam = playerCam;
                weapon.currentAmmo = weapon.maxAmmo;
                weapon.FirePoint = weapon.weaponInstance.transform.Find("FirePoint");
                weapon.weaponStats = GameObject.Find("WeaponStats");
                weapon.weaponStatsText = weapon.weaponStats.GetComponent<TextMeshProUGUI>();
                weapon.displayText();
                
                state = WeaponState.ready;
            break;
            case WeaponState.secondary:
                if(secondaryTime > 0){
                    secondaryTime -= Time.deltaTime;
                }
                else{
                    state = WeaponState.secondaryCooldown;
                    secondaryCooldown = weapon.secondaryCooldown;
                }
            break;
            case WeaponState.secondaryCooldown:
                if(secondaryCooldown > 0){
                    secondaryCooldown -= Time.deltaTime;
                }
                else{
                    state = WeaponState.ready;
                }
            break;
            case WeaponState.summoning:
                if(summoningTime > 0){
                    summoningTime -= Time.deltaTime;
                }
                else{
                    Destroy(summoningCircle);
                    state = WeaponState.startUp;
                } 
            break;
            case WeaponState.none:
            break;
        }
        }
    }
    public void summonNewWeapon(){

        summoningCircle = Instantiate(summoningCirclePrefab,playerCam.transform);

        weapon.die();

        // Increment all weaponscores by 1
        weaponScores = weaponScores.Select(x => x+1).ToArray();

        int index = getNextWeaponIndex();

        // pseudo random weapon generation
        weapon = weapons[index];
        weaponScores[index] = 0;

        summoningTime = weapon.summoningTime;
    }

    private int getNextWeaponIndex(){
        int total =  weaponScores.Sum();

        int randomValue = UnityEngine.Random.Range(0, total);

        int cursor = 0;

        for (int i = 0; i < weaponScores.Length; i++){
            cursor += weaponScores[i];
            if (cursor >= randomValue){
                return i;
            }
        }
        // This should never happen!
        Debug.Log("Illegal case for weapon selection! Fallback to element 0");
        return 0;
    }
}
