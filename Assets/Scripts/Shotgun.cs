using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class Shotgun : Weapon
{

    
    public override void shoot()
    {
        Debug.Log("Shotgun Shoot");
        changeAmmo(-1);
        
        
        model.GetComponent<Animator>().Play("Armature|Shoot");
        //base.shoot();
    }

    public override void secondary()
    {
       model.GetComponent<Animator>().Play("Armature|Pump");
    }




}