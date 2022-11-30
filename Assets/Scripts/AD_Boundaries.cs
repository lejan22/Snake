using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD_Boundaries : MonoBehaviour
{
    private float Xlim = 17.08f;
    private float Ylim = 12.46f;
    private float Zlim = 1;

    private void FixedUpdate()
    {
        if (transform.position.x > Xlim)
        {
            transform.position = new Vector3(-Xlim, transform.position.y, Zlim);
        }

        if (transform.position.x < -Xlim)
        {
            transform.position = new Vector3(Xlim, transform.position.y, Zlim);
        }

        if (transform.position.y > Ylim)
        {
            transform.position = new Vector3(transform.position.x, -Ylim, Zlim);
        }

        if (transform.position.y < -Ylim)
        {
            transform.position = new Vector3(transform.position.x, Ylim, Zlim);
        }
    }
}
