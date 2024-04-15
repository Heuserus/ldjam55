using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AcidCloudBehaviour : MonoBehaviour
{
    // If I have time I can make the cloud parts float a bit
    public GameObject[] CloudParts;

    public float Speed;

    private GameMaster gameMaster;

    private GameObject player;

    public float lifeTime = 10f;

    public float DamageTickTime = 3f;

    private float timeInCloud = 0f;


    // Start is called before the first frame update
    void Start()
    {
       gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>(); 
       player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!(gameMaster.state == GameMaster.GameState.phase1||gameMaster.state == GameMaster.GameState.phase2)){
            return;
        } 

        Vector3 vector2Player = (player.transform.position - transform.position);
        // Stay at the same height
        vector2Player.y = 0;

        transform.position += vector2Player.normalized * Speed * Time.deltaTime;

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other){
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name != "PlayerObj"){
            return;
        }
        player.GetComponent<Player>().Damage(1);
        timeInCloud = 0;
    }

    void OnTriggerStay(Collider other){
        if (other.gameObject.name != "PlayerObj"){
            return;
        }
        timeInCloud += Time.deltaTime;

        if (timeInCloud > DamageTickTime){
            player.GetComponent<Player>().Damage(1);
            timeInCloud = 0;
        }
    }

    void OnTriggerExit(Collider other){
        if (other.gameObject.name != "PlayerObj"){
            return;
        }
        timeInCloud = 0;
    }
}
