using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Idle, Walking, Sprinting, CrouchedIdle, CrouchedWalking };

public class Player : MonoBehaviour
{
    public PlayerState playerState;
    public static event Action<string> InteractedWithInteractable;

    [Tooltip("The length of the player's raycast")]
    [SerializeField] private float rayCastDistance = 2f;

    private GameObject playerCamera;
    private RaycastHit hit;
    private bool playerRaycastHit;

    // Use this for initialization
    private void Start()
    {
        playerState = PlayerState.Idle;
        playerCamera = this.gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdatePlayerState();
        InteractWithInteractables();
    }

    private void FixedUpdate()
    {
        PlayerRaycast();
    }

    private void PlayerRaycast()
    {
        Vector3 forward = playerCamera.transform.TransformDirection(Vector3.forward);
        playerRaycastHit = Physics.Raycast(playerCamera.transform.position, forward, out hit, rayCastDistance);

#if UNITY_EDITOR
        Debug.DrawRay(playerCamera.transform.position, forward * rayCastDistance, Color.green);
#endif
    }

    private void InteractWithInteractables()
    {
        if (playerRaycastHit && hit.transform.gameObject.layer == 11)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (InteractedWithInteractable != null)
                {
                    InteractedWithInteractable.Invoke(hit.transform.gameObject.name);
                }
            }
        }
    }

    //Detects input from player and switches player state accordingly
    private void UpdatePlayerState()
    {
        if (playerState == PlayerState.Idle)
        {
            if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 ||
                Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
            {
                playerState = PlayerState.Walking;
            }
            if (Input.GetAxis("Vertical") > 0 && Input.GetButtonDown("Sprint"))
            {
                playerState = PlayerState.Sprinting;
            }
            if(Input.GetButtonDown("Crouch"))
            {
                playerState = PlayerState.CrouchedIdle;
            }
        }
        else if (playerState == PlayerState.Walking)
        {
            if (Input.GetAxis("Vertical") > 0 && Input.GetButtonDown("Sprint"))
            {
                playerState = PlayerState.Sprinting;
            }
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                playerState = PlayerState.Idle;
            }
            if(Input.GetButtonDown("Crouch"))
            {
                playerState = PlayerState.CrouchedWalking;
            }
        }
        else if (playerState == PlayerState.Sprinting)
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                playerState = PlayerState.Idle;
            }
            if(!Input.GetButton("Sprint") || Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
            {
                playerState = PlayerState.Walking;
            }
            if(Input.GetButtonDown("Crouch"))
            {
                playerState = PlayerState.CrouchedWalking;
            }
        }
        else if(playerState == PlayerState.CrouchedIdle)
        {
            if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 ||
                Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
            {
                playerState = PlayerState.CrouchedWalking;
            }
            if(Input.GetButtonDown("Crouch"))
            {
                playerState = PlayerState.Idle;
            }
            if(Input.GetAxis("Vertical") > 0 && Input.GetButtonDown("Sprint"))
            {
                playerState = PlayerState.Sprinting;
            }
        }
        else if(playerState == PlayerState.CrouchedWalking)
        {
            if(Input.GetButtonDown("Crouch"))
            {
                playerState = PlayerState.Walking;
            }
            if (Input.GetAxis("Vertical") > 0 && Input.GetButtonDown("Sprint"))
            {
                playerState = PlayerState.Sprinting;
            }
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                playerState = PlayerState.CrouchedIdle;
            }
        }
    }
}
