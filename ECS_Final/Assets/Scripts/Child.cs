using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    [Tooltip("Running speed")]
    [SerializeField] private float speed = 5f;

    private bool canRun = false;
    private bool isGoingTo2 = false;

    private void FixedUpdate()
    {
        if (canRun)
        {
            Run();
        }
    }

    private void OnEnable()
    {
        FadeToBlack.FadedOut += ChildrenArrive;
        ClassEnd.SchoolsOut += SummerTime;
    }

    private void OnDisable()
    {
        FadeToBlack.FadedOut -= ChildrenArrive;
        ClassEnd.SchoolsOut -= SummerTime;
    }

    private void ChildrenArrive()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void SummerTime()
    {
        GetComponent<Animator>().SetBool("bellHasRung", true);
        StartCoroutine(LetTheKidStandUp());
    }

    private void Run()
    {
        if(!isGoingTo2)
        {
            this.gameObject.transform.LookAt(GameObject.Find("Running Point 1").gameObject.transform, Vector3.up);
            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Running Point 1").gameObject.transform.position, speed * Time.deltaTime);
        }

        if(isGoingTo2)
        {
            this.gameObject.transform.LookAt(GameObject.Find("Running Point 2").gameObject.transform, Vector3.up);
            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Running Point 2").gameObject.transform.position, speed * Time.deltaTime);
        }

        if (this.gameObject.transform.position == GameObject.Find("Running Point 1").gameObject.transform.position)
        {
            isGoingTo2 = true;
        }

        if (this.gameObject.transform.position == GameObject.Find("Running Point 2").gameObject.transform.position)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator LetTheKidStandUp()
    {
        yield return new WaitForSeconds(6.2f);
        canRun = true;
    }
}
