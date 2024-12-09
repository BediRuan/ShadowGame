using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public AudioSource hurtAudioSource;         // 用于播放音效的 AudioSource
    public AudioClip[] hurtClips;              // 存储多个受伤音效的数组
    private int lastPlayedIndex = -1;
    public float audioCooldown = 1.0f;         // 音效冷却时间
    private bool canPlayAudio = true;          // 控制音效播放的标志

    private Renderer enemyRenderer;            // 敌人材质的渲染器
    public Material originalMaterial;          // 原始材质
    public Material hurtMaterial;              // 受伤时的材质
    public float materialChangeDuration = 0.5f; // 材质切换持续时间
    private bool isChangingMaterial = false;   // 防止材质多次切换

    private EnemyMovement enemyMovement;       // 敌人移动脚本引用
    private bool isCollidingWithPlayer = false; // 检测是否正在与玩家碰撞
    private Coroutine restoreMovementCoroutine; // 记录恢复移动的协程

    private void Start()
    {
        // 获取渲染器
        enemyRenderer = GetComponent<Renderer>();
        if (enemyRenderer != null)
        {
            originalMaterial = enemyRenderer.material; // 初始化为当前材质
        }

        // 获取敌人移动脚本
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy took damage. Current health: " + health);

        if (!isChangingMaterial) // 防止重复材质切换
        {
            StartCoroutine(ChangeMaterialAndStopMovement());
        }

        if (hurtClips.Length > 0 && canPlayAudio) // 播放随机受伤音效
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, hurtClips.Length);
            } while (randomIndex == lastPlayedIndex); // 确保不同于上一次

            lastPlayedIndex = randomIndex;
            hurtAudioSource.PlayOneShot(hurtClips[randomIndex]);
            StartCoroutine(AudioCooldownRoutine()); // 启动冷却协程
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy has died.");
        Destroy(gameObject);
    }

    private IEnumerator AudioCooldownRoutine()
    {
        canPlayAudio = false;                  // 禁止音效播放
        yield return new WaitForSeconds(audioCooldown); // 等待冷却时间
        canPlayAudio = true;                   // 冷却结束，允许音效播放
    }

    private IEnumerator ChangeMaterialAndStopMovement()
    {
        isChangingMaterial = true;

        if (enemyRenderer != null && hurtMaterial != null)
        {
            // 切换到受伤材质
            enemyRenderer.material = hurtMaterial;
        }

        // 暂停敌人移动
        if (enemyMovement != null)
        {
            enemyMovement.StopMovement(true); // 确保暂停有效
        }

        // 等待指定时间
        yield return new WaitForSeconds(materialChangeDuration);

        // 恢复材质
        if (enemyRenderer != null && originalMaterial != null)
        {
            enemyRenderer.material = originalMaterial;
        }

        // 恢复敌人移动
        if (enemyMovement != null && !isCollidingWithPlayer)
        {
            enemyMovement.StopMovement(false); // 恢复移动
        }

        isChangingMaterial = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollidingWithPlayer = true;

            // 如果有恢复协程正在运行，停止它
            if (restoreMovementCoroutine != null)
            {
                StopCoroutine(restoreMovementCoroutine);
            }

            if (enemyMovement != null)
            {
                enemyMovement.StopMovement(true); // 暂停敌人移动
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollidingWithPlayer = false;

            // 启动延迟恢复移动的协程
            restoreMovementCoroutine = StartCoroutine(RestoreMovementAfterDelay());
        }
    }

    private IEnumerator RestoreMovementAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // 等待 0.5 秒

        if (!isCollidingWithPlayer && enemyMovement != null)
        {
            enemyMovement.StopMovement(false); // 恢复敌人移动
        }

        restoreMovementCoroutine = null; // 协程执行完成，重置引用
    }
}
