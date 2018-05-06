using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Idle, Walking, Sprinting, CrouchedIdle, CrouchedWalking };

public class Player : MonoBehaviour
{
    public PlayerState playerState;

    // Use this for initialization
    void Start()
    {
        playerState = PlayerState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerState();
    }

    //Detects input from player and switches player state accordingly
    void UpdatePlayerState()
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
