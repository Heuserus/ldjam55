using System;
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

    public float spread = 0.5f;

    public float maxRange = 20;

    public float damageFallofStartDistance = 2f;

    public int BaseDamage = 3;

    public override void shoot()
    {
        if(currentAmmo > 0 || charges > 0){
            Debug.Log("Shotgun Shoot");
            changeAmmo(-1);
            model.GetComponent<Animator>().Play("Armature|Shoot");
            RaycastHit hit;
            if(Physics.SphereCast(FirePoint.position, spread, playerCam.transform.TransformDirection(Vector3.forward), out hit, maxRange)){

                float shotDistance = hit.distance;
                Debug.DrawRay(FirePoint.position, playerCam.transform.TransformDirection(Vector3.forward) * hit.distance,Color.yellow);
                GameObject a = Instantiate(FireParticles, FirePoint.position, Quaternion.identity);
                GameObject b = Instantiate(HitPointParticles, hit.point, Quaternion.identity);

                Destroy(a,1);
                Destroy(b,2);

                BossBehaviour boss = hit.transform.GetComponent<BossBehaviour>();

                if(boss != null){
                    boss.Damage(calculateDamageFalloff(shotDistance));
                }
            }
        }
        
    }

    private float calculateDamageFalloff(float distance){
        Debug.Log(distance);
        if (distance <= damageFallofStartDistance){
            return BaseDamage;
        }
        return Math.Max(BaseDamage * Mathf.Pow((maxRange - distance)/maxRange, 2), 0);
    }

    public override void secondary()
    {
       model.GetComponent<Animator>().Play("Armature|Pump");
    }

}