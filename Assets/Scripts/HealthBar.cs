using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider bossHealthSlider = null;
    public Slider easeHealthSlider = null;
    public float bossMaxHealth = 100f;
    public float bossHealth;
    private float lerpSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        bossHealth = bossMaxHealth;
        Debug.Log("bossHealth:" + bossHealth.ToString("0"));
    }

    // Update is called once per frame
    void Update()
    {
        if (bossHealthSlider.value != bossHealth)
        {
            bossHealthSlider.value = bossHealth;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }

        if (bossHealthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, bossHealth , lerpSpeed);
        }
    }

    public void TakeDamage(float damage)
    {
        bossHealth -= damage;
        Debug.Log("bossHealth:" + bossHealth);
    }
}
