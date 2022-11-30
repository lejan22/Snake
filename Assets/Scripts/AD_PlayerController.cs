using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AD_PlayerController : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments;//A list that takes care of how long the snake will get once you eat fruit
    private Vector2 input; //To prevent the snake from going back
    private float nextUpdate;

    public Transform segmentPrefab;
    public float speed = 10f;
    public float speedMultiplier = 1f;

    public int life = 3;

    private Animator _animator;
    private bool isMoving;
    public GameObject waterParticle;
    private GameObject turningPoint;

    public TextMeshPro lifetext;

    // Start is called before the first frame update
    void Start()
    {
        life = 3;
        _segments = new List<Transform>();
        _segments.Add(this.transform);
        turningPoint = transform.Find("AD_turningPoint").gameObject;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        

        if (_direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (waterParticle != null && turningPoint != null)
                {
                    Instantiate(waterParticle, turningPoint.transform.position,
                     turningPoint.transform.rotation);
                }
                
                isMoving = true;
                input = Vector2.up;
                _animator.Play("AD_Boa_up");
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (waterParticle != null && turningPoint != null)
                {
                    Instantiate(waterParticle, turningPoint.transform.position,
                     turningPoint.transform.rotation);
                }
                
                isMoving = true;
                input = Vector2.down;
                _animator.Play("AD_Boa_down");
            }
        }
        // Only allow turning left or right while moving in the y-axis
        else if (_direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (waterParticle != null && turningPoint != null)
                {
                    Instantiate(waterParticle, turningPoint.transform.position,
                     turningPoint.transform.rotation);
                }
                
                isMoving = true;
                input = Vector2.right;
                _animator.Play("AD_Boa_right");
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (waterParticle != null && turningPoint != null)
                {
                    Instantiate(waterParticle, turningPoint.transform.position,
                     turningPoint.transform.rotation);
                }
                
                isMoving = true;
                input = Vector2.left;
                _animator.Play("AD_Boa_left");
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

        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
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
        }
        else if (other.tag == "Ouch")
        {
            life--;

           if (life == 0)
            {
                ResetGame();
                life = 3;
            }
           
        }

    }
    
}
