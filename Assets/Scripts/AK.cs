using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AK : Weapon
{
    public float Damage = 0.3f;

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

            GameObject a = Instantiate(FireParticles, FirePoint.position, Quaternion.identity);
            a.transform.LookAt(FirePoint.position + playerCam.transform.TransformDirection(Vector3.forward).normalized);
            Destroy(a,1);

            model.GetComponent<Animator>().Play("Armature|Shoot");
            RaycastHit hit;
            if(Physics.Raycast(playerCam.transform.position,playerCam.transform.TransformDirection(Vector3.forward), out hit,100)){

                // We hit nothing boys
                if (hit.transform.tag != "Boss"){
                    GameObject c = Instantiate(DecalParticles, hit.point, Quaternion.identity);
                    Destroy(c, 1);
                    return;
                }

                BossBehaviour boss = FindObjectOfType<BossBehaviour>();

                GameObject b = Instantiate(HitPointParticles, hit.point, Quaternion.identity);
                Destroy(b,2);

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
