using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AlienGun : Weapon
{

    public GameObject bulletPrefab;
    public override void shoot()
    {
        if(currentAmmo > 0){
            changeAmmo(-1);
            model.GetComponent<Animator>().Play("Armature|Shoot");
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            bullet.transform.position = playerCam.transform.position;
            bullet.GetComponent<BulletBehaviour>().setDirection(playerCam.transform.TransformDirection(Vector3.forward));
        }
        
    }
}
