/*
using UnityEngine;

// PlayerController�� �÷��̾� ĳ���ͷμ� Player ���� ������Ʈ�� �����Ѵ�.
public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip; // ����� ����� ����� Ŭ��
    public float jumpForce = 700f; // ���� ��

    //
    public AudioClip itemClip;

    private int jumpCount = 0; // ���� ���� Ƚ��
    private bool isGrounded = false; // �ٴڿ� ��Ҵ��� ��Ÿ��
    private bool isDead = false; // ��� ����

    private Rigidbody2D playerRigidbody; // ����� ������ٵ� ������Ʈ
    private Animator animator; // ����� �ִϸ����� ������Ʈ
    private AudioSource playerAudio; // ����� ����� �ҽ� ������Ʈ
    

    private void Start()
    {
        // �ʱ�ȭ
        // ���� ������Ʈ�κ��� ����� ������Ʈ���� ������ ������ �Ҵ�
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // ����� �Է��� �����ϰ� �����ϴ� ó��
        if (isDead)
        {
            //��� �� ó���� �� �̻� �������� �ʰ� ����
            return;
        }

        // ���콺 ���� ��ư�� �������� && �ִ� ���� Ƚ��(2)�� �������� �ʾҴٸ�
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            //���� Ƚ�� ����
            jumpCount++;
            //���� ������ �ӵ��� ���������� ����(0, 0)�� ����
            playerRigidbody.velocity = Vector2.zero;
            //������ٵ� �������� �� �ֱ�
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            //����� �ҽ� ���
            playerAudio.Play();
        }

        // ���콺 ���� ��ư���� ���� ���� ���� && �ӵ��� y���� ������(���� ��� ��)
        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            // ���� �ӵ��� �������� ����
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        // �ִϸ������� Grounded �Ķ���͸� isGrounded ������ ����
        // �Ķ���� �̸�, �ش� �Ķ���Ϳ� �Ҵ��� ���ο� �� �Է�
        animator.SetBool("Grounded", isGrounded);
    }

    private void Die()
    {
        // ��� ó��

        //�ִϸ������� Die Ʈ���� �Ķ���͸� ��
        animator.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerRigidbody.velocity = Vector2.zero;
        isDead = true;

        //���� �Ŵ����� ���ӿ��� ó�� ����
        GameManager.instance.OnPlayerDead();
    }

    //OnTriggerEnter : Ʈ���� �浹�� ����
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ʈ���� �ݶ��̴��� ���� ��ֹ����� �浹�� ����
        if (other.tag == "Dead" && !isDead)
        {
            Die();
        }
        if(other.tag == "Item")
        {
            GameManager.instance.AddScore(1);
            //�����ۿ� ������ �������
            other.gameObject.SetActive(false);

            //
            playerAudio.clip = itemClip;
            playerAudio.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ٴڿ� ������� �����ϴ� ó��

        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // �ٴڿ��� ������� �����ϴ� ó��
        isGrounded = false;
    }
}

*/

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip;
    public AudioClip jumpClip;
    public AudioClip itemClip;
    //
    public AudioClip backgroundClip;

    public float jumpForce = 700f;
    public int jumpMax;

    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isDead = false;

    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private AudioSource playerAudio;
    //
    private AudioSource backgroundAudio;


    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

       
        backgroundAudio = gameObject.AddComponent<AudioSource>();
        backgroundAudio.loop = true; // �ݺ� ��� ����
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

        // ��׶��� ���� ���߱�
        backgroundAudio.Stop();

        GameManager.instance.OnPlayerDead();
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

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
