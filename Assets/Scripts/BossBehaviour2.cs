using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour2 : MonoBehaviour
{
   public float maxHealth = 10f;
    public float health;
    public Image healthBar;

    public GameObject ui;

    public GameObject gameMasterObj;

    public Animator animator;

    GameMaster gameMaster;

    public void Damage(int Damage){
        health -= Damage;
        healthBar.fillAmount = health / maxHealth;
        
        if(health <= 0){
            Die();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ui.SetActive(true);
        gameMaster = gameMasterObj.GetComponent<GameMaster>();
        health = maxHealth;
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameMaster.state == GameMaster.GameState.phase1||gameMaster.state == GameMaster.GameState.phase2){

        }
        
    }

    public void Die(){
        ui.SetActive(false);
        Destroy(this.gameObject);
    }
}
