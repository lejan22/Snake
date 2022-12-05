using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD_Destroybomb : MonoBehaviour
{
    public float lifeTime;
    
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }

    }
}
