using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] int startingDirection = -1;

    Collider2D _collider;
    Rigidbody2D _body;
    ContactFilter2D _groundMask;

    int _direction;
    bool _dead;
    
    readonly RaycastHit2D[] _hitBuffer = new RaycastHit2D[8];

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _body = GetComponent<Rigidbody2D>();
        _groundMask.useLayerMask = true;
        _groundMask.layerMask = LayerMask.GetMask("Collision");
    }

    void Start()
    {
        _direction = startingDirection;
        if (_direction == -1)
            FlipDirection();
    }

    void FixedUpdate()
    {
        if (_dead)
        {
            _body.bodyType = RigidbodyType2D.Kinematic;
            _body.velocity = speed * Vector2.down;
            transform.Rotate(0, 0, 10);
            return;
        }

        CheckWall();
        
        var velocity = _body.velocity;
        velocity.x = _direction * speed;
        _body.velocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        var player = col.gameObject.GetComponent<Player>();
        if (player)
            player.Die();
    }

    void CheckWall()
    {
        var hits = _collider.Cast(_direction * Vector2.right, _groundMask, _hitBuffer, 0.1f);
        if (hits == 0)
            return;
        
        _direction = -_direction;
        FlipDirection();
    }

    void FlipDirection()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    public void Die()
    {
        _dead = true;
        _collider.enabled = false;
    }
}
