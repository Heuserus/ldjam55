using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AlienGun : Weapon
{

    public GameObject bulletPrefab;

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
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            bullet.transform.position = playerCam.transform.position;
            bullet.GetComponent<BulletBehaviour>().setDirection(playerCam.transform.TransformDirection(Vector3.forward));
        }
        
    }
}
