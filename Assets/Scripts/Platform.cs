using UnityEngine;

// �������μ� �ʿ��� ������ ���� ��ũ��Ʈ
public class Platform : MonoBehaviour
{
    public GameObject[] obstacles; // ��ֹ� ������Ʈ��

    // ������Ʈ�� Ȱ��ȭ�ɶ����� �Ź� ����Ǵ� �޼���
    private void OnEnable()
    {
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
}