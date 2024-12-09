using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // 方法用于加载关卡
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    // 方法用于退出游戏
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting..."); // 仅在编辑器中有效
    }
}
