using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    [Tooltip("Name of the next scene to load.")]
    public string nextLevelName;
    private bool hasTriggered = false;
    private void Update()
    {
        CheckEnemiesDefeated();
    }

    void CheckEnemiesDefeated()
    {
        // �������д��� "Enemy" ��ǩ����Ϸ����
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // �����������Ϊ 0������ָ������
        if (enemies.Length == 0)
        {
            LoadNextLevel();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        Debug.Log("All enemies defeated. Loading next level: " + nextLevelName);

        // ȷ�� nextLevelName ��Ϊ�ջ�ո�
        if (!string.IsNullOrWhiteSpace(nextLevelName))
        {
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.LogError("Next level name is not set. Please specify a valid level name.");
        }
    }
}
