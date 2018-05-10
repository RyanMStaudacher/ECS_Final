using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public static event Action closed;
    public static event Action opened;
    public static event Action bigReveal;

    private Animator deskAnimator;
    private bool classHasEnded = false;
    private bool isLocked = false;
    private bool gunAudioHasPlayed = false;

    private void Start()
    {
        deskAnimator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        BigReveal();
    }

    private void OnEnable()
    {
        Player.InteractedWithInteractable += Interact;
        FadeToBlack.FadedOut += CloseDrawer;
        ClassEnd.SchoolsOut += SetBool;
    }

    private void OnDisable()
    {
        Player.InteractedWithInteractable -= Interact;
        FadeToBlack.FadedOut -= CloseDrawer;
        ClassEnd.SchoolsOut -= SetBool;
    }

    private void SetBool()
    {
        classHasEnded = true;
    }

    private void Interact(string objectName)
    {
        if (objectName == this.gameObject.name && !isLocked)
        {
            if (deskAnimator.GetBool("shouldOpen") == false)
            {
                deskAnimator.SetBool("shouldOpen", true);
                if(opened != null)
                    opened.Invoke();
            }
            else if (deskAnimator.GetBool("shouldOpen") == true)
            {
                deskAnimator.SetBool("shouldOpen", false);
                if (closed != null)
                    closed.Invoke();
            }
        }
    }

    private void CloseDrawer()
    {
        if (deskAnimator.GetBool("shouldOpen") == true)
        {
            deskAnimator.SetBool("shouldOpen", false);
            if (closed != null)
                closed.Invoke();
        }
    }

    private void BigReveal()
    {
        if(classHasEnded && deskAnimator.GetBool("shouldOpen") == true)
        {
            if(!gunAudioHasPlayed)
            {
                GetComponent<AudioSource>().Play();
                gunAudioHasPlayed = true;
            }

            if(gunAudioHasPlayed && !GetComponent<AudioSource>().isPlaying)
            {
                if (bigReveal != null)
                    bigReveal.Invoke();
            }
        }
    }
}
