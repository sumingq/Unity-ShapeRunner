using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip;
    public AudioClip jumpClip;
    public AudioClip itemClip;
    public AudioClip backgroundClip;

    public float jumpForce = 700f;
    public int jumpMax;

    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isDead = false;

    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private AudioSource playerAudio;
    private AudioSource backgroundAudio;


    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        backgroundAudio = gameObject.AddComponent<AudioSource>();
        backgroundAudio.loop = true; 
        backgroundAudio.clip = backgroundClip;
        backgroundAudio.Play();
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && jumpCount < jumpMax)
        {
            jumpCount++;
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            PlayJumpSound();
        }
        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        animator.SetBool("Grounded", isGrounded);
    }

    private void Die()
    {
        animator.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerRigidbody.velocity = Vector2.zero;
        isDead = true;

        backgroundAudio.Stop();

        GameManager.instance.OnPlayerDead();
    }

    // 특정 태그를 가진 오브젝트와 충돌했을 때 호출
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dead") && !isDead)
        {
            Die();
        }
        else if (other.CompareTag("Item"))
        {
            GameManager.instance.AddScore(1);
            other.gameObject.SetActive(false);
            PlayItemSound();
        }
    }

    // 충돌을 감지했을 때
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    // 충돌이 안될 때
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void PlayJumpSound()
    {
        playerAudio.clip = jumpClip;
        playerAudio.Play();
    }

    private void PlayItemSound()
    {
        playerAudio.clip = itemClip;
        playerAudio.Play();
    }
   
  }
