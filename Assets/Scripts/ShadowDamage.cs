using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDamage : MonoBehaviour
{
    public int damage = 25; // 对敌人造成的伤害值
    public GameObject enemy; // 指定要受伤的敌人对象

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // 检测是否被子弹击中
        {
            Debug.Log("Damage object hit by bullet!");

            // 检查是否绑定了敌人对象
            if (enemy != null)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(damage); // 对敌人造成伤害
                }
            }

            // 销毁子弹
            Destroy(collision.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet")) // 检测是否被子弹击中
        {
            Debug.Log("Damage object hit by bullet!");

            // 检查是否绑定了敌人对象
            if (enemy != null)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(damage); // 对敌人造成伤害
                }
            }

            // 销毁子弹
            //Destroy(other.gameObject);
        }
    }
}