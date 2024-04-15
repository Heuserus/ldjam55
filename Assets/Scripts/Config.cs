using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Config : ScriptableObject
{
    public bool assist = false;

    public float volume = 1;

    public float sens = 1;
    public  void setAssistMode(){
        assist = !assist;
    }

    public void setVolume(float v){
        volume = v;
        AudioListener.volume = volume;
    }

    public void setSensitivity(float v){
        sens = v;
    }
}
