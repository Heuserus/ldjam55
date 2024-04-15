using UnityEngine;

[CreateAssetMenu]
public class ThrowWeapon : Weapon
{

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
            // model.GetComponent<Animator>().Play("Armature|Shoot");

            model.transform.parent = null;
            if(model.GetComponent<ThrowableBehaviour>()){
                model.GetComponent<ThrowableBehaviour>().yeet(playerCam.transform.TransformDirection(Vector3.forward).normalized);
            }
            else{
                model.GetComponent<HolyGrenade>().yeet(playerCam.transform.TransformDirection(Vector3.forward).normalized);
            }
            
        }
    }

    public override void secondary()
    {
        base.secondary();
    }

}
