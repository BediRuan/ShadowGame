using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // �������ڼ��عؿ�
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    // ���������˳���Ϸ
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting..."); // ���ڱ༭������Ч
    }
}
