using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFades : MonoBehaviour
{
    public GameObject fadeIn, fadeOut;

    void Awake ()
    {
        fadeIn.SetActive(true);
        fadeOut.SetActive(true);
        Destroy(this);
    }
}
