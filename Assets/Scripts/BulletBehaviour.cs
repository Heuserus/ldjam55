using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    public float MaxTravelDistance = 200f;
    
    private Vector3 direction; 

    private float travelledDistance;

    public float speed;

    public GameObject DecalParticles; 

    public void setDirection(Vector3 direction){
        this.direction = direction.normalized * speed;
        transform.LookAt(transform.position + direction.normalized * 10f);
    }

    void Update(){
        transform.position += direction * Time.deltaTime;

        travelledDistance +=  speed * Time.deltaTime;

        if (travelledDistance > MaxTravelDistance){
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter(Collision collision){
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Player"){
            return;
        }

        GameObject c = Instantiate(DecalParticles, transform.position, Quaternion.identity);
        Destroy(c, 1);

        if (collision.gameObject.tag == "Boss"){
                BossBehaviour boss = FindObjectOfType<BossBehaviour>();

                if(boss != null){
                    boss.Damage(1);
                }
        }

        Destroy(gameObject);
    }
}
