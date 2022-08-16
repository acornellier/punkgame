using System.Collections;
using Animancer;
using UnityEngine;

[RequireComponent(typeof(AnimancerComponent))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Destructible : MonoBehaviour
{
    [SerializeField] AnimationClip idleClip;
    [SerializeField] AnimationClip destroyClip;
    [SerializeField] float fadeSpeed = 2;
    [SerializeField] Vector2 launchForce = Vector2.one;

    AnimancerComponent _animancer;
    Rigidbody2D _body;
    Collider2D _collider;
    SpriteRenderer _renderer;

    void Awake()
    {
        _animancer = GetComponent<AnimancerComponent>();
        _body = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animancer.Play(idleClip);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.collider.GetComponent<Player>())
            return;

        _collider.enabled = false;
        _body.AddForce(launchForce, ForceMode2D.Impulse);
        StartCoroutine(CO_Destroy());
    }

    IEnumerator CO_Destroy()
    {
        var state = _animancer.Play(destroyClip);
        yield return state;

        // var color = _renderer.color;
        // var t = 0f;
        // while (_renderer.color.a > 0)
        // {
        //     t += Time.deltaTime * fadeSpeed;
        //     color.a = Mathf.Lerp(1, 0, t);
        //     _renderer.color = color;
        //     yield return null;
        // }
        //
        // Destroy(gameObject);
    }
}