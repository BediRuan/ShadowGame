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
        // 查找所有带有 "Enemy" 标签的游戏对象
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // 如果敌人数量为 0，加载指定场景
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

        // 确保 nextLevelName 不为空或空格
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
