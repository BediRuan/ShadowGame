using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    // ��ײ��ⷽ��
    private void OnCollisionEnter(Collision collision)
    {
        // �����ײ�Ķ����Ƿ����ض��ı�ǩ������ǽ�ڻ���棩
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject); // �����ӵ�����
        }
    }
}
