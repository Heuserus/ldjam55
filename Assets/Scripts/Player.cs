using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public GameObject gameMasterObj;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;
    public float airMultiplier;
    bool readyToJump;

    public float jumpCooldown;

    public float dashCooldown = 1f;
    
    public float dashDuration = 0.2f;

    public float dashSpeed = 40f;

    public Transform orientation;

    private bool isDashing;
    
    private bool readyToDash;

    private Vector3 dashDirection;

    private float currentMaxSpeed;

    public float maxAllowedSpeed = 70f;

    public float bunnyhopTimewindow = 0.15f;

    public float crouchNoPenaltyDuration = 1f;

    public float crouchSpeedGain = 1.5f;

    public float crouchSpeedPenalty = 0.15f;

    private double crouchStartTime;

    private bool isCrouched = false;

    public float crouchCameraOffset = 0.7f;

    private GameObject cameraPos;
    
    [Header("Grounded")]
    public LayerMask whatIsGround;
    bool grounded;
    public float groundDrag;
    public float playerHeight;

    public GameObject[] bloodCorners;

    private double groundTouchTime;
    
    [Header("Keybinds")]
    public KeyCode jumpKey;

    public KeyCode dashKey = KeyCode.LeftShift;

    public KeyCode crouchKey = KeyCode.C;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    [Header("Stats")]

    public float maxHealth = 10f;

    public float health;

    public Image healthBar;

    private float cameraBaseY;

    Rigidbody rb;

    GameMaster gameMaster;
    // Start is called before the first frame update
    void Start()
    {
        gameMaster = gameMasterObj.GetComponent<GameMaster>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        readyToDash = true;
        health = maxHealth;
        groundTouchTime = Time.timeAsDouble;
        crouchStartTime = Time.timeAsDouble;
        currentMaxSpeed = moveSpeed;
        cameraPos = transform.Find("CameraPos").gameObject;
        cameraBaseY = cameraPos.transform.localPosition.y;
    }

    // Update is called once per frame
    public void Update()
    {
        if(gameMaster.state == GameMaster.GameState.phase1||gameMaster.state == GameMaster.GameState.phase2){
        bool nowGrounded = Physics.Raycast(transform.position, Vector3.down,playerHeight * 0.5f +0.2f, whatIsGround);
        if (!grounded && nowGrounded){
            groundTouchTime = Time.timeAsDouble;
        }
        grounded = nowGrounded;

        MyInput();
        SpeedControl();
        if (grounded && !isDashing && !isCrouched){
            rb.drag = groundDrag;
        }
        else{
            rb.drag = 0;
        }
        }
        
    }

    private void FixedUpdate(){
        if(gameMaster.state == GameMaster.GameState.phase1||gameMaster.state == GameMaster.GameState.phase2){
        MovePlayer();
        }
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

        if (Input.GetKey(dashKey) && readyToDash){
            readyToDash = false;
            Dash();
            Invoke(nameof(EndDash), dashDuration);
            Invoke(nameof(ResetDash), dashCooldown);
        }

        if (Input.GetKey(crouchKey) && grounded && !isCrouched){
            isCrouched = true;
            Crouch();
        }

        if (Input.GetKeyUp(crouchKey)){
            isCrouched = false;
            cameraPos.transform.localPosition = new Vector3(cameraPos.transform.localPosition.x, cameraBaseY, cameraPos.transform.localPosition.z);
            // If crouch has exited allow player to benefit from grace period as if they just started touching the ground
            if (grounded){
                groundTouchTime = Time.timeAsDouble;
            }
        }
    }

    private void MovePlayer(){
        if (isDashing){
            rb.AddForce(dashDirection.normalized * dashSpeed * 10f, ForceMode.Force);
            return;
        }

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded){
            rb.AddForce(moveDirection.normalized * currentMaxSpeed * 10f, ForceMode.Force);
        }
        else{
            rb.AddForce(moveDirection.normalized * currentMaxSpeed * 10f * airMultiplier, ForceMode.Force);
        }
        
    }

    private void Jump(){
        rb.velocity = new Vector3(rb.velocity.x,0f,rb.velocity.z);
        rb.AddForce(transform.up * jumpForce,ForceMode.Impulse);
    }

    private void Crouch(){
        crouchStartTime = Time.timeAsDouble;
        currentMaxSpeed += crouchSpeedGain;
        cameraPos.transform.localPosition = new Vector3(cameraPos.transform.localPosition.x, cameraBaseY - crouchCameraOffset, cameraPos.transform.localPosition.z);
    }

    private void Dash(){
        isDashing = true;
        dashDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (dashDirection.magnitude == 0){
            dashDirection = orientation.forward;
        }

        rb.AddForce(dashDirection.normalized * dashSpeed * 10f, ForceMode.Force);

        // Speed carryover
        currentMaxSpeed += dashSpeed / 4;
    }

    private void ResetDash(){
        readyToDash = true;
    }

    private void EndDash(){
        isDashing = false;
    }

    private void ResetJump(){
        readyToJump = true;
    }

    private void SpeedControl(){

        Vector3 flatVel = new Vector3(rb.velocity.x,0f,rb.velocity.z);

        //Debug.Log("Current Player Speed: " + flatVel.magnitude);

        if (isDashing){
            return;
        }

        // Movement speed penalty
        if (isCrouched){
            if (Time.timeAsDouble - Math.Max(crouchStartTime, groundTouchTime) > crouchNoPenaltyDuration && grounded){
                print("Penalty");
                currentMaxSpeed = Math.Max(currentMaxSpeed - crouchSpeedPenalty, 0);
            }
        }
        else if (grounded && Time.timeAsDouble - groundTouchTime > bunnyhopTimewindow){
            currentMaxSpeed = moveSpeed;
        }

        if(flatVel.magnitude > Math.Min(currentMaxSpeed, maxAllowedSpeed)){
            Vector3 limitedVel = flatVel.normalized * currentMaxSpeed;
            rb.velocity = new Vector3(limitedVel.x,rb.velocity.y,limitedVel.z);
        }
    }

    public void Damage(int damage){
        health -= damage;
        healthBar.fillAmount = health / maxHealth;
        if (health/maxHealth <= 0.5f){
            GetComponent<WeaponArm>().setFlaskNext();
        }
        foreach (var corner in bloodCorners)
        {
            var tempColor = corner.GetComponent<Image>().color;
            tempColor.a = 1-(health/maxHealth);
            corner.GetComponent<Image>().color = tempColor;
        }
        if(health<=0){
            Die();
        }
    }

    public void Die(){
        gameMaster.playerDeath();
        Destroy(this.gameObject);
        
    }

     private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MageAttack"){
            Damage(2);
            Destroy(other.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MageAttack"){
            Damage(2);
            Destroy(collision.gameObject);
        }
    }
}
