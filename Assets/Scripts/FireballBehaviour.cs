using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class FireballBehaviour : MonoBehaviour
{

    public float speed;

    private float travelledDistance;

    public float maxRange;

    private Vector3 movementVector;

    public float rotationSpeed;

    private GameMaster gameMaster;

    public void Start(){
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    public void SetMovementVector(Vector3 movementVector){
        this.movementVector = movementVector.normalized * speed;
    }

    void FixedUpdate(){
        if(!(gameMaster.state == GameMaster.GameState.phase1||gameMaster.state == GameMaster.GameState.phase2)){
            return;
        }
        transform.position += this.movementVector;
        travelledDistance += speed;
        transform.rotation = Quaternion.Euler( transform.rotation.eulerAngles.x + rotationSpeed, transform.rotation.eulerAngles.y + rotationSpeed, transform.rotation.eulerAngles.z + rotationSpeed);
    }

    // Update is called once per frame
    void Update()
    {
       if (travelledDistance > maxRange) {
            Destroy(gameObject);
       }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Cover"){
            return;
        }
        Destroy(gameObject);
    }
}
