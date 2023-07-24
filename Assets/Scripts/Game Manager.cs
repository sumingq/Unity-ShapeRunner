using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ���� ���� ���¸� ǥ���ϰ�, ���� ������ UI�� �����ϴ� ���� �Ŵ���
// ������ �� �ϳ��� ���� �Ŵ����� ������ �� �ִ�.
public class GameManager : MonoBehaviour
{
    public static GameManager instance; // �̱����� �Ҵ��� ���� ����

    public bool isGameover = false; // ���� ���� ����
    public Text scoreText; // ������ ����� UI �ؽ�Ʈ
    public GameObject gameoverUI; // ���� ������ Ȱ��ȭ �� UI ���� ������Ʈ
    public int itemnum;

    //
    [HideInInspector]
    private int score = 0; // ���� ����

    // ���� ���۰� ���ÿ� �̱����� ����
    void Awake()
    {
        // �̱��� ���� instance�� ����ִ°�?
        if (instance == null)
        {
            // instance�� ����ִٸ�(null) �װ��� �ڱ� �ڽ��� �Ҵ�
            instance = this;
        }
        else
        {
            // instance�� �̹� �ٸ� GameManager ������Ʈ�� �Ҵ�Ǿ� �ִ� ���

            // ���� �ΰ� �̻��� GameManager ������Ʈ�� �����Ѵٴ� �ǹ�.
            // �̱��� ������Ʈ�� �ϳ��� �����ؾ� �ϹǷ� �ڽ��� ���� ������Ʈ�� �ı�
            Debug.LogWarning("���� �ΰ� �̻��� ���� �Ŵ����� �����մϴ�!");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // ���� ���� ���¿��� ������ ������� �� �ְ� �ϴ� ó��
        if (isGameover && Input.GetMouseButtonDown(0))
        {
            //���� ���� �̸��� ���� name���� ����
            //���� SceneManager.GetActiveScene().name�� ���� ���� �̸��� ������ ��
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Level 1")
        {
            if (score >= itemnum)
            {
                SceneManager.LoadScene("Level 2");
            }
        }
        else if (currentSceneName == "Level 2")
        {
            if (score >= itemnum)
            {
                SceneManager.LoadScene("Level 3");
            }
        }
        else if (currentSceneName == "Level 3")
        {
            if (score >= itemnum)
            {
                SceneManager.LoadScene("Outro");
            }
        }
    }

    // ������ ������Ű�� �޼���
    public void AddScore(int newScore)
    {
        if (!isGameover)
        {
            score += newScore;
            scoreText.text = "SCORE : " + score;
        }
    }

    // �÷��̾� ĳ���Ͱ� ����� ���� ������ �����ϴ� �޼���
    public void OnPlayerDead()
    {
        isGameover = true;
        gameoverUI.SetActive(true);
    }
}