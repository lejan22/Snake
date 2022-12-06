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
    public float speed = 20f;
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
    private AD_Bomb _Bomb;
    private AD_DataPersistance _dataPersistance;

    private AudioSource playerAudioSource;
    public AudioClip colisionsfx;
    public AudioClip itemCollectsfx;


    // Start is called before the first frame update
    void Start()
    {
        //We start with 5 life points
        life = 5;

        //Reset the score
        score = 0;

        speedMultiplier = 1f;

        _segments = new List<Transform>();

        _segments.Add(this.transform);

        turningPoint = transform.Find("AD_turningPoint").gameObject;

        //We get the sprite renderer for the blinking
        _characterRenderer = GetComponent<SpriteRenderer>();
        //We get the audiosource
        playerAudioSource = GetComponent<AudioSource>();
        //We get the icon from the UI
        _IconDamage = FindObjectOfType<AD_IconDamage>();
        //We get the bomb component
        _Bomb = FindObjectOfType<AD_Bomb>();
        //We get datapersistance
        _dataPersistance = FindObjectOfType<AD_DataPersistance>();
    }

    //We get the animator component
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
            //if we press up or W, the direction our character will go is up
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                
                if (waterParticle != null && turningPoint != null)
                {
                    Destroy(Instantiate(waterParticle, turningPoint.transform.position,
                     turningPoint.transform.rotation), 1.5f);
                }

                isMoving = true;
                input = Vector2.up;
                //We play the animation for going up
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
                //We play the animation for going down
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
        //Makes the blinking 
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
        //MathF makes all the numbers whole, without decimals
        float x = Mathf.Round(transform.position.x) + _direction.x;
        float y = Mathf.Round(transform.position.y) + _direction.y;

        transform.position = new Vector2(x, y);

        nextUpdate = Time.time + (1f / (speed * speedMultiplier));
        //GAME OVER
        if (life <= 0)
        {
            //We save the score to display it at the game over screen 
            PlayerPrefs.SetInt("score",score);
            //We go to the game over screen
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }

    
   
    //This function will make our snake grow once we collect the item required
    public void extend()
    {
        Transform segment = Instantiate(this.segmentPrefab);

        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);

        //We add a point to the score
        score++;
        //We make it go faster
        speed += 0.5f;
        //Will make a sound effect everytime you collect one
        playerAudioSource.PlayOneShot(itemCollectsfx, 1);
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
            //When we collect the item we make the function extend
            extend();

            //The obstacle will appear randomly around the stage
            _Bomb.RandomPosition();


        }

        else if (other.tag == "Ouch")
        {
            //When we touch something we do the function get hurt
            Gethurt();

            //We reduce the speed of the player to give time to react
            speed -= 2 ;
           
           
        }

    }

    //Function used for make the character blink
    private void ToggleColor(bool isVisible)
    {
        Color color = _characterRenderer.color;
        color = new Color(color.r, color.g, color.b,
         isVisible ? 1 : 0);
        _characterRenderer.color = color;
    }


    public void Gethurt()
    {
       //We reduce one life point
        life--;
        //We play the sfx
        playerAudioSource.PlayOneShot(colisionsfx, 2);
        
        if (blinkingDuration > 0)
        {
            isBlinking = true;
            blinkingCounter = blinkingDuration;
        }
        //We make the icon animate
        _IconDamage.Animate();
    }
}
