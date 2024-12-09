using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public AudioSource hurtAudioSource;         // ���ڲ�����Ч�� AudioSource
    public AudioClip[] hurtClips;              // �洢���������Ч������
    private int lastPlayedIndex = -1;
    public float audioCooldown = 1.0f;         // ��Ч��ȴʱ��
    private bool canPlayAudio = true;          // ������Ч���ŵı�־

    private Renderer enemyRenderer;            // ���˲��ʵ���Ⱦ��
    public Material originalMaterial;          // ԭʼ����
    public Material hurtMaterial;              // ����ʱ�Ĳ���
    public float materialChangeDuration = 0.5f; // �����л�����ʱ��
    private bool isChangingMaterial = false;   // ��ֹ���ʶ���л�

    private EnemyMovement enemyMovement;       // �����ƶ��ű�����
    private bool isCollidingWithPlayer = false; // ����Ƿ������������ײ
    private Coroutine restoreMovementCoroutine; // ��¼�ָ��ƶ���Э��

    private void Start()
    {
        // ��ȡ��Ⱦ��
        enemyRenderer = GetComponent<Renderer>();
        if (enemyRenderer != null)
        {
            originalMaterial = enemyRenderer.material; // ��ʼ��Ϊ��ǰ����
        }

        // ��ȡ�����ƶ��ű�
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy took damage. Current health: " + health);

        if (!isChangingMaterial) // ��ֹ�ظ������л�
        {
            StartCoroutine(ChangeMaterialAndStopMovement());
        }

        if (hurtClips.Length > 0 && canPlayAudio) // �������������Ч
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, hurtClips.Length);
            } while (randomIndex == lastPlayedIndex); // ȷ����ͬ����һ��

            lastPlayedIndex = randomIndex;
            hurtAudioSource.PlayOneShot(hurtClips[randomIndex]);
            StartCoroutine(AudioCooldownRoutine()); // ������ȴЭ��
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
        canPlayAudio = false;                  // ��ֹ��Ч����
        yield return new WaitForSeconds(audioCooldown); // �ȴ���ȴʱ��
        canPlayAudio = true;                   // ��ȴ������������Ч����
    }

    private IEnumerator ChangeMaterialAndStopMovement()
    {
        isChangingMaterial = true;

        if (enemyRenderer != null && hurtMaterial != null)
        {
            // �л������˲���
            enemyRenderer.material = hurtMaterial;
        }

        // ��ͣ�����ƶ�
        if (enemyMovement != null)
        {
            enemyMovement.StopMovement(true); // ȷ����ͣ��Ч
        }

        // �ȴ�ָ��ʱ��
        yield return new WaitForSeconds(materialChangeDuration);

        // �ָ�����
        if (enemyRenderer != null && originalMaterial != null)
        {
            enemyRenderer.material = originalMaterial;
        }

        // �ָ������ƶ�
        if (enemyMovement != null && !isCollidingWithPlayer)
        {
            enemyMovement.StopMovement(false); // �ָ��ƶ�
        }

        isChangingMaterial = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollidingWithPlayer = true;

            // ����лָ�Э���������У�ֹͣ��
            if (restoreMovementCoroutine != null)
            {
                StopCoroutine(restoreMovementCoroutine);
            }

            if (enemyMovement != null)
            {
                enemyMovement.StopMovement(true); // ��ͣ�����ƶ�
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollidingWithPlayer = false;

            // �����ӳٻָ��ƶ���Э��
            restoreMovementCoroutine = StartCoroutine(RestoreMovementAfterDelay());
        }
    }

    private IEnumerator RestoreMovementAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // �ȴ� 0.5 ��

        if (!isCollidingWithPlayer && enemyMovement != null)
        {
            enemyMovement.StopMovement(false); // �ָ������ƶ�
        }

        restoreMovementCoroutine = null; // Э��ִ����ɣ���������
    }
}
