using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AK : Weapon
{
    public int Damage = 1;

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
        if(currentAmmo > 0){
            setupAudioSource();
            audioSource.Play(); 
            changeAmmo(-1);
            model.GetComponent<Animator>().Play("Armature|Shoot");
            RaycastHit hit;
            if(Physics.Raycast(playerCam.transform.position,playerCam.transform.TransformDirection(Vector3.forward), out hit,100)){
                GameObject a = Instantiate(FireParticles, FirePoint.position, Quaternion.identity);
                GameObject b = Instantiate(HitPointParticles, hit.point, Quaternion.identity);

                Destroy(a,1);
                Destroy(b,2);

                // We hit nothing boys
                if (hit.transform.tag != "Boss"){
                    return;
                }

                BossBehaviour boss = FindObjectOfType<BossBehaviour>();

                if(boss != null){
                    boss.Damage(1f);
                }
            }
        }
    }
    public override void secondary()
    {
        base.secondary();
        
    }




}
