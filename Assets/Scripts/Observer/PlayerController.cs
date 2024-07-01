using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Subject
{
    private bool _isJumpPressed = false;
    private bool _isAttacking = false;
    private bool _isHurt = false;

    private float _playerHealth = 100f;
 
    AudioSource _playerAudioPlayer;

    [SerializeField] AudioClip[] _gruntAudio;

    // Start is called before the first frame update
    void Start()
    {
        _playerAudioPlayer = GetComponent<AudioSource>();

        //Notify the player has spawned
        NotifyObservers(PlayerActions.Login);
    }

    // Update is called once per frame
    void Update()
    {
        _isJumpPressed = Input.GetKeyDown(KeyCode.Space);

        _isAttacking = Input.GetKeyDown(KeyCode.C);

        _isHurt = Input.GetKeyDown(KeyCode.X);

        OnJump();
        OnAttack();
        GetHurt();
    }

    private void OnJump()
    {
        if (_isJumpPressed)
        {
            //Debug.Log("Jump");
            //_playerAudioPlayer.PlayOneShot(_gruntAudio[UnityEngine.Random.Range(0, _gruntAudio.Length)]);
            NotifyObservers(PlayerActions.Jump);
        }
    }

    private void OnAttack()
    {
        if (_isAttacking)
        {
            //Debug.Log("Attack");
            _playerAudioPlayer.PlayOneShot(_gruntAudio[UnityEngine.Random.Range(0, _gruntAudio.Length)]);
            NotifyObservers(PlayerActions.AttackHit);
        }
    }

    private void GetHurt()
    {
        if (_isHurt)
        {
            PlayerHealth -= 10f;

            if (PlayerHealth <= 0)
            {
                //notify the player has died
            }
            else
            {
                NotifyObservers(PlayerActions.Hurt);
            }
        }
    }

    public float PlayerHealth
    {
        get { return _playerHealth; }
        set { _playerHealth = value; }
    }
}
