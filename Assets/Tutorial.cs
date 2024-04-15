using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{   
    public GameObject mainMenu;

    public void back(){
        mainMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
