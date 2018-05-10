using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public static event Action FadedOut;

    [Tooltip("The time it takes to fade out/back in")]
    [SerializeField] private float fadeTime;
    
    private CanvasGroup canvasGroup;
    private bool hasPlacedGun = false;
    private bool hasUsedToilet = false;
    private bool hasFaded = false;
    private float tParam = 0f;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        FadeOut();
        FadeIn();
    }

    private void OnEnable()
    {
        Gun.PlacedGun += SetGunBool;
        Door.Flushed += SetFlushedBool;
        Drawer.bigReveal += FadeOutLast;
    }

    private void OnDisable()
    {
        Gun.PlacedGun -= SetGunBool;
        Door.Flushed -= SetFlushedBool;
        Drawer.bigReveal -= FadeOutLast;
    }

    private void SetGunBool()
    {
        hasPlacedGun = true;
    }

    private void SetFlushedBool()
    {
        hasUsedToilet = true;
    }

    private void FadeOut()
    {
        if (hasPlacedGun)
        {
            if (tParam < 1 && !hasFaded)
            {
                tParam += Time.deltaTime * fadeTime;

                canvasGroup.alpha = Mathf.Lerp(0, 1, tParam);
            }

            if (tParam >= 1)
            {
                hasFaded = true;
                hasPlacedGun = false;
                if (FadedOut != null)
                    FadedOut.Invoke();
            }
        }

        if (hasUsedToilet)
        {
            if (tParam < 1 && !hasFaded)
            {
                tParam += Time.deltaTime * fadeTime;

                canvasGroup.alpha = Mathf.Lerp(0, 1, tParam);
            }

            if (tParam >= 1)
            {
                hasFaded = true;
                hasUsedToilet = false;
                if (FadedOut != null)
                    FadedOut.Invoke();
            }
        }
    }

    private void FadeIn()
    {
        if(hasFaded)
        {
            tParam -= Time.deltaTime * fadeTime;

            canvasGroup.alpha = Mathf.Lerp(0, 1, tParam);
        }

        if(tParam <= 0)
        {
            hasFaded = false;
        }
    }

    private void FadeOutLast()
    {
        if (tParam < 1 && !hasFaded)
        {
            tParam += Time.deltaTime * fadeTime;

            canvasGroup.alpha = Mathf.Lerp(0, 1, tParam);
        }
    }
}
