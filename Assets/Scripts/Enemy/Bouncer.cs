using Animancer;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(AnimancerComponent))]
[RequireComponent(typeof(BoxCollider2D))]
public class Bouncer : MonoBehaviour
{
    [SerializeField] Dialogue playerDialogue;
    [SerializeField] AnimationClip idle;
    [SerializeField] AnimationClip yes;
    [SerializeField] AnimationClip no;

    [Inject] DialogueManager _dialogueManager;

    AnimancerComponent _animancer;
    BoxCollider2D _collider;

    void Awake()
    {
        _animancer = GetComponent<AnimancerComponent>();
        _collider = GetComponent<BoxCollider2D>();
    }

    void OnEnable()
    {
        _animancer.Play(idle);
    }

    public void AllowPlayerThrough()
    {
        _animancer.Play(yes);
        _collider.enabled = false;
    }

    public void DenyPlayer()
    {
        _animancer.Play(no);
        _dialogueManager.StartDialogue(playerDialogue);
    }

    public void OnPlayerGone()
    {
        _animancer.Play(idle);
    }
}