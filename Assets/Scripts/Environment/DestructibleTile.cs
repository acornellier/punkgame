using System.Collections;
using Animancer;
using UnityEngine;

// [RequireComponent(typeof(AnimancerComponent))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class DestructibleTile : MonoBehaviour
{
    [SerializeField] AnimationClip idleClip;
    [SerializeField] AnimationClip destroyClip;
    [SerializeField] float fadeSpeed = 2;
    [SerializeField] float respawnTime = 5;

    // AnimancerComponent _animancer;
    Collider2D _collider;
    SpriteRenderer _renderer;

    void Awake()
    {
        // _animancer = GetComponent<AnimancerComponent>();
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        // _animancer.Play(idleClip);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.collider.GetComponent<Player>())
            return;

        if (col.collider.bounds.min.y - _collider.bounds.max.y < -0.1f)
            return;

        StartCoroutine(CO_Destroy());
    }

    IEnumerator CO_Destroy()
    {
        // var state = _animancer.Play(destroyClip);
        // yield return state;

        var color = _renderer.color;
        var t = 0f;
        while (_renderer.color.a > 0)
        {
            t += Time.deltaTime * fadeSpeed;
            color.a = Mathf.Lerp(1, 0, t);
            _renderer.color = color;
            yield return null;
        }

        _collider.enabled = false;

        yield return new WaitForSeconds(5);

        color.a = 1;
        _renderer.color = color;
        _collider.enabled = true;
    }
}