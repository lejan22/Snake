using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD_PlayerController : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments;

    public Transform segmentPrefab;
    // Start is called before the first frame update
    void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }
    }

    //MathF makes all the numbers whole, without decimals
    private void FixedUpdate()
    {
        for(int i = _segments.Count-1; i > 0; i--)
        {
            _segments [i].position = _segments[i - 1].position;
        }
        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + _direction.x, Mathf.Round(this.transform.position.y) + _direction.y, 0.0f);
    }

    private void extend()
    {
        Transform segment = Instantiate(this.segmentPrefab);

        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }
    private void ResetGame()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Tube")
        {
            extend();
        } else if(other.tag =="Ouch"){
            ResetGame();
        }

    }
}
