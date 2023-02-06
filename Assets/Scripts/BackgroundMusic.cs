using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//use this script if you want to use same music record throw all the scenes
//with stop on each scene and starting record from the beggining
public class BackgroundMusic : MonoBehaviour
{
    // Use this for initialization
    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
