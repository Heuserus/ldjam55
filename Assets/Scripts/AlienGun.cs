using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AlienGun : Weapon
{
    public override void shoot()
    {
        if(currentAmmo > 0){
            changeAmmo(-1);
            model.GetComponent<Animator>().Play("Armature|Shoot");
        }
        
    }
}
