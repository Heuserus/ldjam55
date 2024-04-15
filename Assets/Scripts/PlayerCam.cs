using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float MaxSensX = 10000;
    public float MaxSensY = 10000;

    public Transform orientation;

    float xRotation;
    float yRotation;

    public GameObject gameMasterObj;
    GameMaster gameMaster;

    private void Start(){
          gameMaster = gameMasterObj.GetComponent<GameMaster>();
          Cursor.lockState = CursorLockMode.Locked;
          Cursor.visible = false;
    }

    public void Update(){
        if(gameMaster.state == GameMaster.GameState.phase1||gameMaster.state == GameMaster.GameState.phase2){
            Debug.Log(gameMaster.config.sens);
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * (MaxSensX * gameMaster.config.sens);
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * (MaxSensY * gameMaster.config.sens);

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f,90f);
        transform.rotation = Quaternion.Euler(xRotation,yRotation,0);
        orientation.rotation = Quaternion.Euler(0, yRotation,0);
    }
    }

}
