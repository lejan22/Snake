using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD_TubePlacement : MonoBehaviour
{
    public BoxCollider2D spawnArea;
    // Start is called before the first frame update
    void Start()
    {
        RandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void RandomPosition()
    {
        Bounds bounds = this.spawnArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x),Mathf.Round (y), 0.0f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag== "Player")
        {
            RandomPosition();
        }
        
    }
}
