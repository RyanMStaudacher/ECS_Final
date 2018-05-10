using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public static event Action Flushed;

    private bool canFlush = false;
    private bool hasFlushed = false;

    private void OnEnable()
    {
        Player.InteractedWithInteractable += UseRestroom;
        Gun.PlacedGun += SetBool;
    }

    private void OnDisable()
    {
        Player.InteractedWithInteractable -= UseRestroom;
        Gun.PlacedGun -= SetBool;
    }

    private void SetBool()
    {
        canFlush = true;
    }

    private void UseRestroom(string objectName)
    {
        if (objectName == this.gameObject.name && !hasFlushed && canFlush)
        {
            GetComponent<AudioSource>().Play();
            if (Flushed != null)
                Flushed.Invoke();
            hasFlushed = true;
        }
    }
}
