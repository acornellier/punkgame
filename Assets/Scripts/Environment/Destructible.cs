using System.Collections;
using Animancer;
using UnityEngine;

[RequireComponent(typeof(AnimancerComponent))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Destructible : MonoBehaviour
{
    [SerializeField] AnimationClip idleClip;
    [SerializeField] AnimationClip destroyClip;
    [SerializeField] float fadeSpeed = 2;

    AnimancerComponent _animancer;
    SpriteRenderer _renderer;

    void Awake()
    {
        _animancer = GetComponent<AnimancerComponent>();
        _renderer = GetComponent<SpriteRenderer>();
        _animancer.Play(idleClip);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>())
            StartCoroutine(CO_Destroy());
    }

    IEnumerator CO_Destroy()
    {
        var state = _animancer.Play(destroyClip);
        yield return state;

        var color = _renderer.color;
        var t = 0f;
        while (_renderer.color.a > 0)
        {
            t += Time.deltaTime * fadeSpeed;
            color.a = Mathf.Lerp(1, 0, t);
            _renderer.color = color;
            yield return null;
        }

        Destroy(gameObject);
    }
}