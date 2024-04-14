using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AK : Weapon
{
    public int Damage = 1;
    
    
    public override void shoot()
    {
        if(currentAmmo > 0){
            
            changeAmmo(-1);
            model.GetComponent<Animator>().Play("Armature|Shoot");
            RaycastHit hit;
            if(Physics.Raycast(playerCam.transform.position,playerCam.transform.TransformDirection(Vector3.forward), out hit,100)){
                GameObject a = Instantiate(FireParticles, FirePoint.position, Quaternion.identity);
                GameObject b = Instantiate(HitPointParticles, hit.point, Quaternion.identity);

                Destroy(a,1);
                Destroy(b,2);

                BossBehaviour boss = hit.transform.GetComponent<BossBehaviour>();

                if(boss != null){
                    boss.Damage(Damage);
                }
            }

            

        }
    }
    public override void secondary()
    {
        base.secondary();
        
    }




}
