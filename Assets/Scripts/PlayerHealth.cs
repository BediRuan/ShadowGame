using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth = 100;
    public Image healthImpact;
    public string gameOverSceneName = "GameOver";

    public float invincibilityDuration = 1f; // �޵г���ʱ��
    private float lastDamageTime = 0f;      // ��¼�ϴ�����ʱ��

    void Start()
    {
        playerHealth = 100;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            PlayerTookDamage(25f);
        }
        else
        {
            PlayerNotTakingDamage(0.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            PlayerNotTakingDamage(0.5f);
        }
    }
    void HealthDamageImpact()
    {
        float transparency = 1f - (playerHealth / 100f);
        Color imageColor = Color.white;
        imageColor.a = transparency;
        healthImpact.color = imageColor;
    }

    void PlayerTookDamage(float damage)
    {
        if (Time.time - lastDamageTime > invincibilityDuration)
        {
            lastDamageTime = Time.time;

            if (playerHealth > 0)
            {
                playerHealth -= damage;
                Debug.Log("Player is taking damage");

                if (playerHealth <= 0)
                {
                    playerHealth = 0; // ȷ��Ѫ����Ϊ����
                    Debug.Log("Player has died");
                    LoadGameOverScene();
                }
            }
        }
    }

    void PlayerNotTakingDamage(float health)
    {
        if (playerHealth < 300 && Time.time - lastDamageTime > invincibilityDuration)
        {
            playerHealth += health;
            playerHealth = Mathf.Clamp(playerHealth, 0, 300); // ����Ѫ����Χ
            Debug.Log("Player is not taking damage");
        }
    }

    void LoadGameOverScene()
    {
        Debug.Log("Loading Game Over scene...");
        SceneManager.LoadScene(gameOverSceneName); // ʹ����ȷ�ĳ�����
    }

    void Update()
    {
        HealthDamageImpact();
    }
}
