using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hide_Player : MonoBehaviour
{

    public static Hide_Player instance;
    public Rigidbody2D rb;
    public Collider2D coll;
    public Animator anim;
    public float speed, jumpForce;
    public int damage, health, maxHealth;
    public Transform groundCheck;
    public float groundCheckArea;
    public LayerMask ground;
    public GameObject Necklace;
    public GameObject weapon;

    public bool isGround, isJump;
    public bool doubleJump;
    int skyAttack, jumpCount;
    public bool isAttack;
    public bool canAttack = false;
    public bool isChange = false;
    public bool canMove = true;
    public bool isHurt;
    public bool isAlive;
    public bool isHenshin;

    public float changeTimer;
    public CircleCollider2D hitBox;
    public float invisibleTime;
    public AudioSource hitAudio;
    public AudioSource waveAudio;
    public AudioSource jumpAudio;
    public AudioSource gunAudio;

    private Scene scene;

    private void Awake()
    {
        scene = SceneManager.GetActiveScene();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        isAlive = true;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        Necklace = GameObject.Find("Necklace").gameObject;
        weapon = GameObject.Find("weapon_axe").gameObject;
        hitBox = GameObject.Find("CheckPoint").gameObject.GetComponent<CircleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
            health = 0;
        if (health > maxHealth)
            health = maxHealth;

        if ( GameObject.Find("Pausemenu") == null)
        {
            Jump();
        }

        if (!isAlive && GameObject.Find("Pausemenu") == null)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void FixedUpdate()
    {
        GroundMovement();
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckArea, ground); //判断是否在地面

        SwitchAnim();
    }

    void GroundMovement()   //移动
    {
        if (canMove)
        {
            float horizontalMove = Input.GetAxisRaw("Horizontal");
            if (!isAttack && !isHurt && isAlive &&  isAlive)
                rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
            if (horizontalMove != 0 && !isAttack && !isHurt && isAlive  && isAlive)
            {
                transform.localScale = new Vector3(horizontalMove, 1, 1);
            }
        }
    }

    void Jump()     // 跳跃
    {
        if (isGround && isAlive)
        {
            if (!doubleJump)
                jumpCount = 1;
            else if (doubleJump)
                jumpCount = 2;
            isJump = false;
        }

        if (!isGround && !isJump)        //修复在没有二段跳的情况下可以落下跳跃的bug
        {
            if (Input.GetButtonDown("Jump")  && doubleJump)
            {
                jumpAudio.Play();
                isJump = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCount -= 2;
            }
        }

        if (Input.GetButtonDown("Jump") && isGround && isAlive)
        {
            jumpAudio.Play();
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
        }
        else if (Input.GetButtonDown("Jump") && jumpCount > 0 && !isGround && isJump && isAlive)
        {
            jumpAudio.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
        }
    }

    void SwitchAnim()   //动画相关
    {
        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));

        if (isGround)
        {
            anim.SetBool("Fall", false);
            anim.SetBool("Jump", false);
        }
        else if (!isGround && rb.velocity.y > 0)
        {
            anim.SetBool("Jump", true);
        }
        else if (rb.velocity.y < 0)
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Fall", true);
        }
        if (isHurt)
        {
            anim.SetBool("isHurt", true);
            if (Mathf.Abs(rb.velocity.x) < 0.2f)
            {
                isHurt = false;
                anim.SetBool("isHurt", false);
                coll.sharedMaterial = new PhysicsMaterial2D()
                {
                    friction = 0.0f,
                    bounciness = 0.0f
                };
            }
        }
    }
}
