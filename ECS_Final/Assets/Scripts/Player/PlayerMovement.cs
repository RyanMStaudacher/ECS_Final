using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float mouseRotationSpeedX;
    [SerializeField] private float mouseRotationSpeedY;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float crouchedWalkSpeed;

    private Player playerScript;
    private CharacterController playerController;
    private float verticalRotation;
    private float currentMoveSpeed;
    private bool hasSetWalkSpeed = false;
    private bool hasSetSprintSpeed = false;
    private bool isCrouched = false;

    // Use this for initialization
    void Start ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        playerController = GetComponent<CharacterController>();
        playerScript = GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        ControlPlayer();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        PlayerLookAround();
    }

    void PlayerLookAround()
    {
        //Look right and left
        if(Input.GetAxis("MouseX") > 0 || Input.GetAxis("MouseX") < 0)
        {
            this.transform.Rotate(0, mouseRotationSpeedY * Input.GetAxis("MouseX") * Time.deltaTime, 0);
        }
        //Look up and down
        if(Input.GetAxis("MouseY") > 0 || Input.GetAxis("MouseY") < 0)
        {
            verticalRotation += Input.GetAxis("MouseY") * mouseRotationSpeedX * Time.deltaTime;

            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

            playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }
    }
    
    public void MovePlayer()
    {
        Vector3 playerMoveLeftRight = transform.right * Input.GetAxis("Horizontal") * (currentMoveSpeed / 1.4f) * Time.deltaTime;
        Vector3 playerMoveForwardBackward = transform.forward * Input.GetAxis("Vertical") * currentMoveSpeed * Time.deltaTime;

        playerController.SimpleMove(playerMoveLeftRight);
        playerController.SimpleMove(playerMoveForwardBackward);
    }

    void ControlPlayer()
    {
        //Walk
        if (playerScript.playerState == PlayerState.Walking)
        {
            if(hasSetWalkSpeed == false)
            {
                currentMoveSpeed = walkSpeed;
                hasSetWalkSpeed = true;
            }
        }
        else if(playerScript.playerState != PlayerState.Walking && hasSetWalkSpeed == true)
        {
            hasSetWalkSpeed = false;
        }

        //Sprint
        if (playerScript.playerState == PlayerState.Sprinting)
        {
            if (hasSetSprintSpeed == false)
            {
                currentMoveSpeed = sprintSpeed;
                hasSetSprintSpeed = true;
            }
        }
        else if (playerScript.playerState != PlayerState.Sprinting && hasSetSprintSpeed == true)
        {
            hasSetSprintSpeed = false;
        }

        //Crouch
        if (playerScript.playerState == PlayerState.CrouchedIdle || playerScript.playerState == PlayerState.CrouchedWalking)
        {
            if (isCrouched == false)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 0.5f, transform.localScale.z);
                currentMoveSpeed = crouchedWalkSpeed;
                isCrouched = true;
            }
        }

        //Stand
        if (playerScript.playerState != PlayerState.CrouchedIdle && playerScript.playerState != PlayerState.CrouchedWalking)
        {
            if (isCrouched == true)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2f, transform.localScale.z);
                isCrouched = false;
            }
        }
    }
}
