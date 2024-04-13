using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class Sniper : Weapon
{
    
    public override void shoot()
    {
        Debug.Log("Shoot");
        //base.shoot();
    }

    

    
}
