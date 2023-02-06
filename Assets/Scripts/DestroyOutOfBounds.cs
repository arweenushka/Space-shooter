using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float yBound = 8;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Destroy objects that pass borders above and below the max and min y border
        if (transform.position.y > yBound)
        {
            Destroy(gameObject);
        }
        else if (transform.position.y < -yBound)
        {
            Destroy(gameObject);
        }
    }
}
