using UnityEngine;

// �������μ� �ʿ��� ������ ���� ��ũ��Ʈ
public class Platform : MonoBehaviour
{
    public GameObject[] obstacles; // ��ֹ� ������Ʈ��
    //private bool stepped = false; // �÷��̾� ĳ���Ͱ� ��Ҿ��°�

    // ������Ʈ�� Ȱ��ȭ�ɶ� ���� �Ź� ����Ǵ� �޼���
  
    private void OnEnable()
    {
        // ������ �����ϴ� ó��
       // stepped = false;

        //��ֹ� 1��~3�� ��������
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (Random.Range(0, 3) == 0)
            {
                obstacles[i].SetActive(true);
            }
            else
            {
                obstacles[i].SetActive(false);
            }
        }
    }
  
    
   /* void OnCollisionEnter2D(Collision2D collision)
    {
        // �÷��̾� ĳ���Ͱ� �ڽ��� ������� ������ �߰��ϴ� ó��
        if (collision.collider.tag == "Player" && !stepped)
        {
            stepped = true;
            GameManager.instance.AddScore(1);
        }
    }
   */
}