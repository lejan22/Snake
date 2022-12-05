using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD_Bomb : MonoBehaviour
{
    public BoxCollider2D spawnArea;

    public GameObject bomb;

    private AD_IconDamage AD_Icon;
    private AD_PlayerController playercontroller;
    // Start is called before the first frame update
    void Start()
    {
        RandomPosition();
        AD_Icon = FindObjectOfType<AD_IconDamage>();
        playercontroller = FindObjectOfType<AD_PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    public void RandomPosition()
    {
        

        Bounds bounds = spawnArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        Vector3 bombposition = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
       
        Instantiate(bomb, bombposition, transform.rotation);
    }
    
}
