using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    private float stage = 1;
    private bool facingRight = true;

    public float jumpForce;
    private bool isOnGround;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask allGround;

    public Text score;
    private int scoreValue = 0;

    public Text winText;
    public int life;
    public Text lifeText;

    public AudioClip musicStageOne;
    public AudioClip musicStageTwo;
    public AudioClip winMusic;
    public AudioClip collectMusic;
    public AudioSource musicSource;
    public AudioSource musicSource2;

    Animator anim;

    

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        life = 3;

        score.text = "Orbs: " + scoreValue.ToString();
        winText.text = "";
        lifeText.text = life.ToString();
        lifeText.text = "Life: " + life;
       

        musicSource.clip = musicStageOne;
        musicSource.Play();
        musicSource.loop = true;
    }

    // Update is called once per frame

    void Update()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        if (verMovement == 0 && isOnGround == true && hozMovement == 0)
        {
            anim.SetInteger("State", 0);
        }

        if(hozMovement > 0 && isOnGround == true)
        {
            anim.SetInteger("State", 1);
        }

        if(hozMovement == 0 && isOnGround == true)
        {
            anim.SetInteger("State", 0);
        }

        if(hozMovement < 0 && isOnGround == true)
        {
            anim.SetInteger("State", 1);
        }

    }


    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector3(hozMovement * speed, verMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, allGround);
        
        if(facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        if(facingRight == true && hozMovement < 0)
        {
            Flip();
        }

        if (isOnGround == false)
        {
           // Debug.Log("Jumping");
            anim.SetInteger("State", 2);
        }
        if (verMovement == 0 && isOnGround == true)
        {
          //  Debug.Log("Not Jumping");
            anim.SetInteger("State", 0);
        }

        


        if(stage == 2 && scoreValue >= 4)
        {

            musicSource.clip = winMusic;
            musicSource.Play();
            musicSource.loop = false;

            winText.text = "NICE! game by Angelo Reid";
            stage = 3;
    

        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(life <= 0)
        {
            winText.text = "Game Over";
            Destroy(this.gameObject);
        }

        if(stage == 1 && scoreValue >= 4)
        {
            transform.position = new Vector3(0.0f, 51.0f, 0.0f);
            stage = 2;
            scoreValue = 0;
            life = 3;
            lifeText.text = "Life: " + life.ToString();


            musicSource.clip = musicStageTwo;
            musicSource.Play();
            musicSource.loop = true;

        }


    }


    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Orbs: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            musicSource2.clip = collectMusic;
            musicSource2.Play();
            musicSource2.loop = false;

        }
        if(collision.collider.tag == "Enemy")
        {
            life -= 1;
            lifeText.text = "Life: " + life.ToString();
            Destroy(collision.collider.gameObject);

        }

  
    }

    void OnCollisionStay2D(Collision2D collision)
    {

        if(collision.collider.tag == "Ground" && isOnGround)
        {
        if(Input.GetKey(KeyCode.W))
        {
            rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        }
        
    }
}
