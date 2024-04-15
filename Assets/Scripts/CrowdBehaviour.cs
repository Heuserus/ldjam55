using System;
using System.Numerics;
using UnityEngine;

public class CrowdBehaviour : MonoBehaviour
{
    
    public float BobbingRange = 1.5f;

    public float speed = 0.06f;

    private float lowY;
    private float highY;

    private bool isDescending;

    private float initialDelay;
    
    public AudioClip[] VoiceLines;

    public GameObject CrowdAudioSourcePrefab;

    public float VoiceLineMinimumDelay = 8;

    public float VoiceLineRadius = 35f;

    private float voiceLineDelay;

    private GameObject audioSource;

    void Start(){
        lowY = transform.position.y - BobbingRange;
        highY = transform.position.y + BobbingRange;
        isDescending = UnityEngine.Random.Range(0,2) == 1;
        initialDelay =  UnityEngine.Random.Range(0,20) * 0.1f;
        voiceLineDelay = VoiceLineMinimumDelay + UnityEngine.Random.Range(0,500) * 0.1f;
        audioSource = GameObject.Instantiate(CrowdAudioSourcePrefab);
        audioSource.GetComponent<AudioSource>().Stop();
    }

    void Update(){
        // Await initial delay to desync rows of crowd
        if (initialDelay > 0){
            initialDelay -= Time.deltaTime;
            return;
        }

        voiceLineDelay -= Time.deltaTime;
        if (voiceLineDelay <= 0){
            PlayVoiceLine();
            voiceLineDelay = VoiceLineMinimumDelay + UnityEngine.Random.Range(0,500) * 0.1f;
        }

        float yMovement;
        if (isDescending){
            yMovement = - Mathf.Max(Mathf.Pow((transform.position.y - lowY), 2), speed);
        } else{
            yMovement = Mathf.Max(Mathf.Pow((transform.position.y - highY), 2), speed);
        }
        transform.position = new UnityEngine.Vector3(transform.position.x, transform.position.y + yMovement * Time.deltaTime, transform.position.z);
        if (isDescending) {
            if (Math.Abs(lowY - transform.position.y) > 0.2){
                return;
            }
        } else if(Math.Abs(highY - transform.position.y) > 0.2){
            return;
        }
        isDescending = !isDescending;
    } 

    void PlayVoiceLine(){
        UnityEngine.Vector3 audioPosition = new UnityEngine.Vector3(0, 15f, 0) + new UnityEngine.Vector3(UnityEngine.Random.Range(-1, 1), 0, UnityEngine.Random.Range(-1, 1)).normalized * VoiceLineRadius;
        audioSource.transform.position  = audioPosition;
        audioSource.GetComponent<AudioSource>().clip = VoiceLines[UnityEngine.Random.Range(0, VoiceLines.Length)];
        audioSource.GetComponent<AudioSource>().Play();
    }
}
