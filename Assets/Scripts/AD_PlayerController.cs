using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class AD_PlayerController : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments;//A list that takes care of how long the snake will get once you eat fruit
    private Vector2 input; //To prevent the snake from going back

    private float nextUpdate;


    public Transform segmentPrefab;
    public float speed = 15f;
    public float speedMultiplier = 1f;

    public int life = 3;
    private int score;

    private Animator _animator;
    private bool isMoving;
    public GameObject waterParticle;
    private GameObject turningPoint;

    public TextMeshProUGUI lifetext;
    public TextMeshProUGUI scoreText;
   
    //Blinking when we get hit
    private bool isBlinking;
    [SerializeField] private float blinkingDuration;
    private float blinkingCounter;
    private SpriteRenderer _characterRenderer;

    private AD_IconDamage _IconDamage;


    // Start is called before the first frame update
    void Start()
    {
        life = 3;

        score = 0;

        speedMultiplier = 2f;

        _segments = new List<Transform>();

        _segments.Add(this.transform);

        turningPoint = transform.Find("AD_turningPoint").gameObject;

        _characterRenderer = GetComponent<SpriteRenderer>();

        _IconDamage = GetComponent<AD_IconDamage>();
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        lifetext.text = life.ToString();
        scoreText.text = score.ToString();

        if (_direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (waterParticle != null && turningPoint != null)
                {
                    Destroy(Instantiate(waterParticle, turningPoint.transform.position,
                     turningPoint.transform.rotation), 1.5f);
                }

                isMoving = true;
                input = Vector2.up;
                _animator.Play("AD_Boa_up");
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (waterParticle != null && turningPoint != null)
                {
                    Destroy(Instantiate(waterParticle, turningPoint.transform.position,
                      turningPoint.transform.rotation), 1.5f);
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
                    Destroy(Instantiate(waterParticle, turningPoint.transform.position,
                     turningPoint.transform.rotation), 1.5f);
                }

                isMoving = true;
                input = Vector2.right;
                _animator.Play("AD_Boa_right");
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (waterParticle != null && turningPoint != null)
                {
                    Destroy(Instantiate(waterParticle, turningPoint.transform.position,
                      turningPoint.transform.rotation), 1.5f);
                }

                isMoving = true;
                input = Vector2.left;
                _animator.Play("AD_Boa_left");
            }
        }

        if (isBlinking)
        {
            blinkingCounter -= Time.deltaTime;
            if (blinkingCounter > blinkingDuration * 0.8)
            {
                ToggleColor(false);
            }
            else if (blinkingCounter > blinkingDuration * 0.6)
            {
                ToggleColor(true);
            }
            else if (blinkingCounter > blinkingDuration * 0.4)
            {
                ToggleColor(false);
            }
            else if (blinkingCounter > blinkingDuration * 0.2)
            {
                ToggleColor(true);
            }
            else if (blinkingCounter > 0)
            {
                ToggleColor(false);
            }
            else
            {
                ToggleColor(true);
                isBlinking = false;
            }

        }
        if (life == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
           // ResetGame();
            //life = 3;
            //score = 0;
        }
    }

    //MathF makes all the numbers whole, without decimals
    private void FixedUpdate()
    {
        // Set the new direction based on the input
        if (input != Vector2.zero)
        {
            _direction = input;
        }

        if (Time.time < nextUpdate)
        {
           return;
        }

        // Set each segment's position to be the same as the one it follows. We
        // must do this in reverse order so the position is set to the previous
        // position, otherwise they will all be stacked on top of each other.
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        // Move the snake in the direction it is facing
        // Round the values to ensure it aligns to the grid
        float x = Mathf.Round(transform.position.x) + _direction.x;
        float y = Mathf.Round(transform.position.y) + _direction.y;

        transform.position = new Vector2(x, y);

        nextUpdate = Time.time + (1f / (speed * speedMultiplier));

    }

    public void extend()
    {
        Transform segment = Instantiate(this.segmentPrefab);

        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);

        score++;

        speedMultiplier++;
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
            Gethurt();
            
           
        }

    }
    private void ToggleColor(bool isVisible)
    {
        Color color = _characterRenderer.color;
        color = new Color(color.r, color.g, color.b,
         isVisible ? 1 : 0);
        _characterRenderer.color = color;
    }


    public void Gethurt()
    {
       
        life--;
       
        if (blinkingDuration > 0)
        {
            isBlinking = true;
            blinkingCounter = blinkingDuration;
        }
        _IconDamage.Animate();
    }
}
