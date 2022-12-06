using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD_TubePlacement : MonoBehaviour
{
    public BoxCollider2D spawnArea;
    // Start is called before the first frame update
    void Start()
    {
        //At the beginning of the match we activate the random position
        RandomPosition();
    }

    //We make the icon spawn randomly inside the spawn area. This spawn area is an empty gameobject with a collider.
    private void RandomPosition()
    {
        Bounds bounds = this.spawnArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x),Mathf.Round (y), 0.0f);
    }

    //Once we touch the player, we appear randomly
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag== "Player")
        {
            RandomPosition();
        }
        
    }
}
