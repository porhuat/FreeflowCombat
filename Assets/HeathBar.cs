using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{
    public Slider instantSlider;  // 即時健康條
    public Slider delayedSlider;  // 延遲健康條
    public float decreaseSpeed = 10f;
    public float playerHealth = 100f;

    private float targetValue;  // 目標值，表示即時健康值

    // Start is called before the first frame update
    void Start()
    {
        SetMaxHealth(playerHealth);
    }

    // Update is called once per frame
    void Update()
    {
        // 逐漸減少延遲健康條的值
        if (delayedSlider.value > targetValue)
        {
            delayedSlider.value -= decreaseSpeed * Time.deltaTime;
            if (delayedSlider.value < targetValue) // 避免滑塊值低於目標值
            {
                delayedSlider.value = targetValue;
            }
        }

        // Test decrease health by pressing X
        if (Input.GetKeyDown(KeyCode.X))
        {
            playerHealth -= 10f; // Decrease player health
            SetHealth(playerHealth); // Update target value
            Debug.Log("Player health: " + playerHealth + " Target: " + targetValue);
        }
    }

    public void SetMaxHealth(float maxHealth)
    {
        instantSlider.maxValue = maxHealth;
        delayedSlider.maxValue = maxHealth;
        instantSlider.value = maxHealth;
        delayedSlider.value = maxHealth;
        targetValue = maxHealth;
    }

    public void SetHealth(float health)
    {
        targetValue = health;
        instantSlider.value = health; // 即時更新即時健康條的值
    }
}