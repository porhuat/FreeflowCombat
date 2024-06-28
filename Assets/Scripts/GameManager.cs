using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Instance
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }
    #endregion

    private Subject subject;

    public float playerHealth { get; private set; } = 100f; // Default health
    public float playerMaxHealth { get; private set; } = 100f; // Default maximum health

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

    void Update()
    {
        // Test decrease health by pressing X
        if (Input.GetKeyDown(KeyCode.X))
        {
            playerHealth -= 10f;
            subject.NotifyObservers(); // Notify observers about the change
            Debug.Log("Player health: " + playerHealth);
        }
    }
}
