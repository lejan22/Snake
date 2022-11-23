using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD_PlayerController : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments;//A list that takes care of how long the snake will get once you eat fruit
    private Vector2 input; //To prevent the snake from going back
    private float nextUpdate;

    public Transform segmentPrefab;
    public float speed = 10f;
    public float speedMultiplier = 1f;
     
    // Start is called before the first frame update
    void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (_direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                input = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                input = Vector2.down;
            }
        }
        // Only allow turning left or right while moving in the y-axis
        else if (_direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                input = Vector2.right;
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                input = Vector2.left;
            }
        }
    }

    //MathF makes all the numbers whole, without decimals
    private void FixedUpdate()
    {
        if (input != Vector2.zero)
        {
            _direction = input;
        }

        for (int i = _segments.Count-1; i > 0; i--)
        {
            _segments [i].position = _segments[i - 1].position;
        }
        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x)
            + _direction.x, Mathf.Round(this.transform.position.y) + _direction.y, 0.0f);


    }

    private void extend()
    {
        Transform segment = Instantiate(this.segmentPrefab);

        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }
    private void ResetGame()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);

        this.transform.position = Vector3.zero;
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
