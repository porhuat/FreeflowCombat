using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Instance
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }
    #endregion

    [SerializeField] private Slider instantSlider;  // 即時健康條
    [SerializeField] private Slider delayedSlider;  // 延遲健康條
    private float _decreaseSpeed = 40f;
    //private float _playerHealth = 100f;
    //private float _targetValue;  // 目標值，表示即時健康值

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public float DecreaseSpeed
    {
        get { return _decreaseSpeed; }
        set { _decreaseSpeed = value; }
    }

    //public float PlayerHealth
    //{
    //    get { return _playerHealth; }
    //    set { _playerHealth = value; }
    //}

    //public float TargetValue
    //{
    //    get { return _targetValue; }
    //    set { _targetValue = value; }
    //}

    //public Slider InstantSlider
    //{
    //    get { return instantSlider; }
    //    set { instantSlider = value; }
    //}

    //public Slider DelayedSlider
    //{
    //    get { return delayedSlider; }
    //    set { delayedSlider = value; } 
    //}
}
