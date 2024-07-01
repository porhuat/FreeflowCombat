using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNarrationSystem : MonoBehaviour, IObserver
{
    [SerializeField] 
    private Subject _playerSubject;

    private Dictionary<PlayerActions, System.Action> _playerActionHandlers;

    private AudioSource _audioPlayer;

    [SerializeField] private AudioClip _loginBGMAudioClip;
    [SerializeField] private AudioClip _jumpingAudioClip;
    [SerializeField] private AudioClip _attackSwordSliceAudioClip;
    [SerializeField] private AudioClip _hurtAudioClip;

    void Awake()
    {
        _audioPlayer = GetComponent<AudioSource>();

        //将处理程序方法分配给其关联的动作
        //assign the handler method to its associated action
        _playerActionHandlers = new Dictionary<PlayerActions, System.Action>()
        {
            { PlayerActions.Login, HandleLogin },
            { PlayerActions.Logout, HandleLogout },
            { PlayerActions.Hurt, HandleHurt },
            { PlayerActions.Jump, HandleJump },
            { PlayerActions.AttackHit, HandleAttackHit }
        };
    }

    public void OnNotify(PlayerActions action)
    {
        if (_playerActionHandlers.ContainsKey(action))
        {
            // 为每个操作调用相应的处理方法
            //invoke the appropriate handler method for each action
            _playerActionHandlers[action]();
            //Debug.Log("PlayerNarrationSystem OnNotify:" + action);
        }
    }

    private void HandleLogin()
    {
        if (_loginBGMAudioClip != null)
        {
            Debug.Log("Login");
            _audioPlayer.clip = _loginBGMAudioClip;
            _audioPlayer.Play();
        }
        else
        {
            Debug.LogWarning("No audio clip assigned for login action");
        }
    }

    private void HandleLogout()
    {

    }

    private void HandleJump()
    {
        _audioPlayer.clip = _jumpingAudioClip;
        _audioPlayer.Play();
    }

    private void HandleHurt()
    {
        _audioPlayer.clip = _hurtAudioClip;
        _audioPlayer.Play();
    }

    private void HandleAttackHit()
    {
        Debug.Log("Attack");

        _audioPlayer.clip = _attackSwordSliceAudioClip;
        _audioPlayer.Play();
    }

    //在游戏对象启用时调用
    // called when the gameobject is enabled
    private void OnEnable()
    {
        // add itself to the subject's list of observers
        _playerSubject.AddObserver(this);
    }

    //在禁用游戏对象时调用
    // called when the gameobject is disabled
    private void OnDisable()
    {
        // remove itself to the subject's list of observers
        _playerSubject.RemoveObserver(this);
    }
}
