using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDamage : MonoBehaviour
{
    public int damage = 25; // �Ե�����ɵ��˺�ֵ
    public GameObject enemy; // ָ��Ҫ���˵ĵ��˶���

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // ����Ƿ��ӵ�����
        {
            Debug.Log("Damage object hit by bullet!");

            // ����Ƿ���˵��˶���
            if (enemy != null)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(damage); // �Ե�������˺�
                }
            }

            // �����ӵ�
            Destroy(collision.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet")) // ����Ƿ��ӵ�����
        {
            Debug.Log("Damage object hit by bullet!");

            // ����Ƿ���˵��˶���
            if (enemy != null)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(damage); // �Ե�������˺�
                }
            }

            // �����ӵ�
            //Destroy(other.gameObject);
        }
    }
}