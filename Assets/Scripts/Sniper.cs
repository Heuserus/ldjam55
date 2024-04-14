using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Sniper : Weapon
{
    public int Damage = 5;
    private bool zoom = false;

    
    
    public override void shoot()
    {
        if(currentAmmo > 0){
            if(playerCam.GetComponent<Camera>().fieldOfView ==60){
            zoom = false; 
            }
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
                    if(zoom){
                        boss.Damage(Damage*2);
                    }
                    else{
                        boss.Damage(Damage);
                    }
                    
                }
            }

            

        }
    }
    public override void secondary()
    {
        if(playerCam.GetComponent<Camera>().fieldOfView ==60){
            zoom = false; 
        }
        if(!zoom){
            GameObject.Find("Zoom").GetComponent<Image>().enabled = true;
            playerCam.GetComponent<Camera>().fieldOfView =30;
            zoom = true;
        }
        else{
            GameObject.Find("Zoom").GetComponent<Image>().enabled = false;
            playerCam.GetComponent<Camera>().fieldOfView =60; 
            zoom = false;  
        }
        
    }




}
