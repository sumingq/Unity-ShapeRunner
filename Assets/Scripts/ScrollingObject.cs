using UnityEngine;

// ���� ������Ʈ�� ��� �������� �����̴� ��ũ��Ʈ
public class ScrollingObject : MonoBehaviour
{
    public float speed = 10f; // �̵� �ӵ�

    private void Update()
    {
        if (!GameManager.instance.isGameover)
        {
            // ���� ������Ʈ�� �������� ���� �ӵ��� ���� �̵��ϴ� ó��
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}