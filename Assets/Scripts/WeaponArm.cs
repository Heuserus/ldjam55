using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponArm : MonoBehaviour
{
    public Weapon weapon;
    float coolDown;
    float shootingTime;

    float secondaryTime;

    float secondaryCooldown;

    public GameObject playerCam;

    enum WeaponState {
        ready,
        coolDown,
        startUp,
        shooting,
        secondary,
        secondaryCooldown

    }

    WeaponState state = WeaponState.startUp;

    public KeyCode fire;
    public KeyCode secondary;

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case WeaponState.ready:
            if(Input.GetKeyDown(fire)){
                
                weapon.shoot();
                state = WeaponState.shooting;
                shootingTime = weapon.shootingTime;
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
            break;
            case WeaponState.startUp:
                weapon.weaponInstance = Instantiate(weapon.weaponPrefab,playerCam.transform);
                weapon.weaponInstance.SetActive(true);
                weapon.model = weapon.weaponInstance.transform.Find("Holder/Model").gameObject;
                weapon.playerCam = playerCam;
                weapon.currentAmmo = weapon.maxAmmo;
                weapon.FirePoint = weapon.weaponInstance.transform.Find("FirePoint");
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
        }
    }
}
