using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;
    public float airMultiplier;
    bool readyToJump;

    public float jumpCooldown;

    public Transform orientation;
    
    [Header("Grounded")]
    public LayerMask whatIsGround;
    bool grounded;
    public float groundDrag;
    public float playerHeight;
    
    [Header("Keybinds")]
    public KeyCode jumpKey;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    [Header("Stats")]

    public float maxHealth = 10f;

    public float health;

    public Image healthBar;


    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        health = maxHealth;
    }

    // Update is called once per frame
    public void Update()
    {

        grounded = Physics.Raycast(transform.position, Vector3.down,playerHeight * 0.5f +0.2f, whatIsGround);
        MyInput();
        SpeedControl();
        if (grounded){
            rb.drag = groundDrag;
        }
        else{
            rb.drag = 0;
        }
        
    }

    private void FixedUpdate(){
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump && grounded){
           readyToJump = false;
           Jump();
           Invoke(nameof(ResetJump), jumpCooldown);

        }

    }

    private void MovePlayer(){
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded){
            rb.AddForce(moveDirection.normalized * moveSpeed *10f, ForceMode.Force);
        }
        else{
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f* airMultiplier, ForceMode.Force);
        }

        
    }

    private void Jump(){
        rb.velocity = new Vector3(rb.velocity.x,0f,rb.velocity.z);
        rb.AddForce(transform.up * jumpForce,ForceMode.Impulse);
    }

    private void ResetJump(){
        readyToJump = true;
    }

    private void SpeedControl(){
        Vector3 flatVel = new Vector3(rb.velocity.x,0f,rb.velocity.z);

        if(flatVel.magnitude > moveSpeed){
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x,rb.velocity.y,limitedVel.z);
        }
    }

    public void Damage(int damage){
        health -= damage;
        healthBar.fillAmount = health / maxHealth;
        if(health<=0){
            Die();
        }
    }
    public void Die(){
        Destroy(this.gameObject);
    }
}
