using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Subject
{
    private bool _isJumpPressed = false;
    private bool _isAttacking = false;

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

        OnJump();
        OnAttack();
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
}
