using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



[CreateAssetMenu(fileName = "New Weapon")]
public class Weapon : ScriptableObject
{
    public string name; 
    public int maxAmmo;

    public Transform FirePoint;

    public int currentAmmo;

    public float shootingTime; // 

    public float secondaryTime;

    public float coolDownTime;

    public float secondaryCooldown;

    public float summoningTime;

    public virtual void shoot(){}

    public virtual void secondary(){}

    public GameObject weaponPrefab;

    public GameObject weaponInstance;

    public GameObject model;

    public GameObject playerCam;

    public GameObject weaponStats;

    public TextMeshProUGUI weaponStatsText;

    public void changeAmmo(int ammo){
        currentAmmo += ammo;
        displayText();

    }

    public void displayText(){
        weaponStatsText.text = name + "   " + currentAmmo + "/"+maxAmmo;
    }
    public void die(){
        Destroy(weaponInstance);
    }

    

    
}
