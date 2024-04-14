using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FistBehaviour : MonoBehaviour
{
    public float punchForce = 5f;

    private Vector3 direction;

    private CapsuleCollider capsuleCollider;

    void Start(){
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void Enable(Vector3 direction){
        this.direction = direction.normalized * punchForce;
        capsuleCollider.enabled = true;
    }

    public void Disable(){
        capsuleCollider.enabled = false;
    }

    void OnCollisionEnter(Collision collision){
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name != "Player"){
            return;
        }
        collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(direction.x, 1.5f, direction.z), ForceMode.Impulse);
        collision.gameObject.GetComponent<Player>().Damage(1);
    }
}