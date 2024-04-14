using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class Flask : Weapon
{
    
    public int heal;

    
    public override void shoot()
    {
        Debug.Log("Flask SHot");
        if(currentAmmo > 0){
            changeAmmo(-1);
            model.GetComponent<Animator>().Play("Armature|Shoot");
            GameObject.Find("Player").GetComponent<Player>().Damage(-heal);
           
        }

            

        
    }
    public override void secondary()
    {
        base.secondary();
    }

}
