using Animancer;
using UnityEngine;

[RequireComponent(typeof(AnimancerComponent))]
public class ConcertLight : MonoBehaviour
{
    [SerializeField] AnimationClip clip;

    AnimancerComponent _animancer;

    void Awake()
    {
        _animancer = GetComponent<AnimancerComponent>();
    }

    void Update()
    {
        var state = _animancer.Play(clip);
    }
}