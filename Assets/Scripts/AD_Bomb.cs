using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD_Bomb : MonoBehaviour
{
    public BoxCollider2D spawnArea;

    public GameObject bomb;

    private AD_PlayerController playercontroller;
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
        Instantiate(this);
        Bounds bounds = this.spawnArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }
}
