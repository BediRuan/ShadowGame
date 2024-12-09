using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    // 碰撞检测方法
    private void OnCollisionEnter(Collision collision)
    {
        // 检查碰撞的对象是否有特定的标签（例如墙壁或地面）
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject); // 销毁子弹对象
        }
    }
}
