using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Analytics;

public class CrowdBehaviour : MonoBehaviour
{
    
    public float BobbingRange = 1.5f;

    public float speed = 0.06f;

    private float lowY;
    private float highY;

    private bool isDescending;

    private float initialDelay;

    void Start(){
        lowY = transform.position.y - BobbingRange;
        highY = transform.position.y + BobbingRange;
        isDescending = UnityEngine.Random.Range(0,2) == 1;
        initialDelay =  UnityEngine.Random.Range(0,20) * 0.1f;
    }

    void Update(){
        // Await initial delay to desync rows of crowd
        if (initialDelay > 0){
            initialDelay -= Time.deltaTime;
            return;
        }
        float yMovement;
        if (isDescending){
            yMovement = - Mathf.Max(Mathf.Pow((transform.position.y - lowY), 2), speed);
        } else{
            yMovement = Mathf.Max(Mathf.Pow((transform.position.y - highY), 2), speed);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y + yMovement * Time.deltaTime, transform.position.z);
        if (isDescending) {
            if (Math.Abs(lowY - transform.position.y) > 0.2){
                return;
            }
        } else if(Math.Abs(highY - transform.position.y) > 0.2){
            return;
        }
        isDescending = !isDescending;
    } 
}
