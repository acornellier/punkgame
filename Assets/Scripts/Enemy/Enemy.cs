using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] bool startFlipped = true;
    [SerializeField] Transform groundCheckPoint;

    Collider2D _collider;
    Rigidbody2D _body;
    LayerMask _groundMask;
    ContactFilter2D _collisionFilter;

    int _direction;
    bool _dead;

    readonly RaycastHit2D[] _hitBuffer = new RaycastHit2D[8];
    readonly Collider2D[] _colliderBuffer = new Collider2D[8];

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _body = GetComponent<Rigidbody2D>();
        _groundMask = LayerMask.GetMask("Collision");
        _collisionFilter.useLayerMask = true;
        _collisionFilter.layerMask = LayerMask.GetMask("Collision", "Enemy");
    }

    void Start()
    {
        _direction = 1;
        if (startFlipped)
            Flip();
    }

    void FixedUpdate()
    {
        if (_dead)
        {
            _body.bodyType = RigidbodyType2D.Kinematic;
            _body.velocity = 2 * speed * Vector2.down;
            transform.Rotate(0, 0, 10);
            return;
        }

        CheckFlip();

        var velocity = _body.velocity;
        velocity.x = _direction * speed;
        _body.velocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (_dead) return;

        var player = col.gameObject.GetComponent<Player>();
        if (player)
            player.Die();
    }

    void CheckFlip()
    {
        var hits = _collider.Cast(_direction * Vector2.right, _collisionFilter, _hitBuffer, 0.1f);
        if (hits != 0)
            Flip();
        else
            CheckLedge();
    }

    void CheckLedge()
    {
        var hits = Physics2D.OverlapCircleNonAlloc(
            groundCheckPoint.position,
            0.1f,
            _colliderBuffer,
            _groundMask
        );

        if (hits == 0)
            Flip();
    }

    void Flip()
    {
        _direction = -_direction;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    public void Die()
    {
        _dead = true;
        _collider.enabled = false;
    }
}