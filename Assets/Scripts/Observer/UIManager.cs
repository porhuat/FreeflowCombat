using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour, IObserver
{
    [SerializeField] PlayerController playerController;

    [SerializeField] private Slider instantSlider;  // 即時健康條
    [SerializeField] private Slider delayedSlider;  // 延遲健康條

    private float _playerHealth = 100f;
    private float _decreaseSpeed = 40f;
    private float _targetValue;

    // Start is called before the first frame update
    void Start()
    {
        SetMaxHealth(PlayerHealth);
    }

    // Update is called once per frame
    void Update()
    {
        // Gradually decrease the delayed health bar value to match the target value
        if (DelayedSlider.value > TargetValue)
        {
            DelayedSlider.value -= GameManager.Instance.DecreaseSpeed * Time.deltaTime;
            if (DelayedSlider.value < TargetValue) // Prevent the slider value from going below the target value
            {
                DelayedSlider.value = TargetValue;
            }
        }
    }

    public void OnNotify(PlayerActions action)
    {
        //Display the action
        if (action == PlayerActions.Hurt)
        {
            if (PlayerHealth >= 0)
            {
                PlayerHealth -= 10f; // Decrease player health
                SetHealth(PlayerHealth); // Update target value
                Debug.Log("Player health UI Update: " + PlayerHealth + " Target: " + TargetValue);
            }
        }
    }

    public void SetMaxHealth(float maxHealth)
    {
        InstantSlider.maxValue = maxHealth;
        DelayedSlider.maxValue = maxHealth;
        InstantSlider.value = maxHealth;
        DelayedSlider.value = maxHealth;
        TargetValue = maxHealth;
    }

    public void DecreaseHeartCountToDisplay()
    {
        
    }

    public void SetHealth(float health)
    {
        TargetValue = health;

        //即時更新即時健康條的值
        InstantSlider.value = health;
    }

    //在游戏对象启用时调用
    // called when the gameobject is enabled
    private void OnEnable()
    {
        // add itself to the subject's list of observers
        playerController.AddObserver(this);
    }

    //在禁用游戏对象时调用
    // called when the gameobject is disabled
    private void OnDisable()
    {
        // remove itself to the subject's list of observers
        playerController.RemoveObserver(this);
    }

    public float PlayerHealth
    {
        get { return _playerHealth; }
        set { _playerHealth = value; }
    }

    public float TargetValue
    {
        get { return _targetValue; }
        set { _targetValue = value; }
    }

    public float DecreaseSpeed
    {
        get { return _decreaseSpeed; }
        set { _decreaseSpeed = value; }
    }

    public Slider InstantSlider
    {
        get { return instantSlider; }
        set { instantSlider = value; }
    }

    public Slider DelayedSlider
    {
        get { return delayedSlider; }
        set { delayedSlider = value; }
    }
}