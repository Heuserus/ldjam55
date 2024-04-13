using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon")]
public class Weapon : ScriptableObject
{
    public string name; // Ben
    public int maxAmmo;

    public int currentAmmo;

    public float shootingTime; // 

    public float secondaryTime;

    public float coolDownTime;

    public float secondaryCooldown;

    public virtual void shoot(){}

    public virtual void secondary(){}

    public GameObject weaponPrefab;

    public GameObject weaponInstance;

    public GameObject model;

    public void changeAmmo(int ammo){
        currentAmmo += ammo;
    }

    

    
}
