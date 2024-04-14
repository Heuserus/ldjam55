using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthquakeBehaviour : MonoBehaviour
{
    
    private CapsuleCollider capsuleCollider;

    private Vector3 direction;

    public float punchForce = 5f;

    public float maxTravelDistance = 50f;

    public float travelSpeed = 40f;

    private float travelledDistance = 0f;

    void Start(){
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Update(){
        transform.position += direction * Time.deltaTime;

        travelledDistance += (direction * Time.deltaTime).magnitude;

        if (travelledDistance > maxTravelDistance){
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector3 direction, float speedfactor){
        this.direction = direction.normalized * travelSpeed * speedfactor;
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
