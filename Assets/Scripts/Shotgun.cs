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

    
    int charges = 0;

    public float spread = 0.5f;

    public float maxRange = 30;

    public float damageFallofStartDistance = 5f;

    public int BaseDamage = 5;

    void setupAudioSource(){
        if (audioSource != null){
            return;
        }
        audioSource = FirePoint.GetComponent<AudioSource>();
        if (audioSource == null){
            audioSource = FirePoint.gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = WeaponSound;
    }

    public override void shoot()
    {
        if(currentAmmo > 0 || charges > 0){
            setupAudioSource();
            audioSource.Play();
            Debug.Log("Shotgun Shoot");
            changeAmmo(-1);

            GameObject a = Instantiate(FireParticles, FirePoint.position, Quaternion.identity);
            a.transform.LookAt(FirePoint.position + playerCam.transform.TransformDirection(Vector3.forward).normalized);
            Destroy(a,1);

            model.GetComponent<Animator>().Play("Armature|Shoot");
            RaycastHit hit;
            if(Physics.SphereCast(playerCam.transform.position, spread, playerCam.transform.TransformDirection(Vector3.forward), out hit, maxRange)){

                float shotDistance = hit.distance;
                Debug.DrawRay(FirePoint.position, playerCam.transform.TransformDirection(Vector3.forward) * hit.distance,Color.yellow);


                // We hit nothing boys
                if (hit.transform.tag != "Boss"){
                    GameObject c = Instantiate(DecalParticles, hit.point, Quaternion.identity);
                    c.transform.LookAt(FirePoint);
                    Destroy(c, 1);
                    return;
                }

                GameObject b = Instantiate(HitPointParticles, hit.point, Quaternion.identity);
                Destroy(b,2);

                BossBehaviour boss = FindObjectOfType<BossBehaviour>();

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
       base.secondary();
    }

}