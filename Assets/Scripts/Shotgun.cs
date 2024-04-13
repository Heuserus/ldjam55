using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class Shotgun : Weapon
{

    public GameObject FireParticles;
    public GameObject HitPointParticles;
    int charges = 0;
    public override void shoot()
    {
        if(currentAmmo > 0 || charges > 0){
            Debug.Log("Shotgun Shoot");
            changeAmmo(-1);
            model.GetComponent<Animator>().Play("Armature|Shoot");
            RaycastHit hit;
            if(Physics.Raycast(FirePoint.position, playerCam.transform.TransformDirection(Vector3.forward), out hit, 100)){
                Debug.DrawRay(FirePoint.position, playerCam.transform.TransformDirection(Vector3.forward) * hit.distance,Color.yellow);
                GameObject a = Instantiate(FireParticles, FirePoint.position, Quaternion.identity);
                GameObject b = Instantiate(HitPointParticles, hit.point, Quaternion.identity);

                Destroy(a,1);
                Destroy(b,2);

                BossBehaviour boss = hit.transform.GetComponent<BossBehaviour>();

                if(boss != null){
                    boss.Damage(1);
                }
            }
        }
        
    }

    public override void secondary()
    {
       model.GetComponent<Animator>().Play("Armature|Pump");
    }




}