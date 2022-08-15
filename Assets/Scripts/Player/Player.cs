using System;
using System.Collections;
using System.IO;
using Animancer;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AnimancerComponent))]
public class Player : MonoBehaviour
{
    [SerializeField] PlayerActions playerActions;
    [SerializeField] PlayerAudio playerAudio;
    [SerializeField] Collider2D cleets;
    [SerializeField] Stats stats;
    [SerializeField] Animations animations;

    Collider2D _collider;
    Rigidbody2D _body;
    AnimancerComponent _animancer;
    ContactFilter2D _groundMask;

    float _jumpInputTimestamp = float.NegativeInfinity;
    bool _isGrounded;
    bool _isJumping;
    float _jumpingTimestamp;
    bool _isFalling;

    readonly RaycastHit2D[] _hitBuffer = new RaycastHit2D[8];

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _body = GetComponent<Rigidbody2D>();
        _animancer = GetComponent<AnimancerComponent>();
        _groundMask.layerMask = LayerMask.GetMask("Collision");
    }

    void Start()
    {
        playerActions.actions.Player.Jump.performed += (_) => _jumpInputTimestamp = Time.time;
        playerActions.actions.Player.Restart.performed += 
            (_) => SceneManager.LoadScene(SceneManager.GetActiveScene().name);;
    }

    void FixedUpdate()
    {
        UpdateGrounded();
        UpdateRunning();
        UpdateJumping();
        UpdateDirection();
        // UpdateAnimations();
    }

    public void DisableControlsAndStopAnimancer()
    {
        playerActions.DisableControls();
        _animancer.Stop();
    }

    void OnFootstep()
    {
        playerAudio.Footstep();
    }

    void UpdateGrounded()
    {
        _isGrounded = IsGrounded();
    }

    bool IsGrounded()
    {
        var numHits = _collider.Cast(Vector2.down, _groundMask, _hitBuffer, 0.05f);
        for (var hitIndex = 0; hitIndex < numHits; hitIndex++)
        {
            var hit = _hitBuffer[hitIndex];
            if (Vector2.Angle(hit.normal, Vector2.up) < 60)
                return true;
        }

        return false;
    }

    void UpdateRunning()
    {
        var moveInput = playerActions.ReadMoveValue();
        var velocity = _body.velocity;
        var velocityDirection = Math.Sign(velocity.x);

        if (moveInput != 0)
        {
            var acceleration = velocityDirection == Math.Sign(moveInput)
                ? stats.acceleration
                : stats.turnAcceleration;
            
            velocity.x += moveInput * acceleration * Time.fixedDeltaTime;
            velocity.x = Mathf.Clamp(velocity.x, -stats.maxSpeed, stats.maxSpeed);
        }
        else if (velocityDirection != 0)
        {
            if (Mathf.Abs(velocity.x) < 0.01f)
            {
                velocity.x = 0;
            }
            else
            {
                velocity.x -= velocityDirection * stats.deceleration * Time.fixedDeltaTime;

                if (Math.Sign(velocity.x) != velocityDirection)
                    velocity.x = 0;
            }
        }

        _body.velocity = velocity;
    }

    void UpdateJumping()
    {
        _body.gravityScale = stats.gravityForce;

        if ((_isJumping && _body.velocity.y < 0) || _body.velocity.y < -5f)
        {
            _isFalling = true;
            _body.gravityScale *= stats.fallingGravityMultiplier;
        }

        if (_isGrounded && (_isFalling || (_isJumping && Time.time - _jumpingTimestamp > 0.1f)))
        {
            _isFalling = false;
            _isJumping = false;
            playerAudio.Land();
        }

        if (_isGrounded && Time.time - _jumpInputTimestamp < stats.jumpInputBuffer)
        {
            Jump();
        }
    }

    public void Jump()
    {
        _isJumping = true;
        _jumpingTimestamp = Time.time;
        _jumpInputTimestamp = 0;
        _body.velocity = new Vector2(_body.velocity.x, 0);
        _body.AddForce(new Vector2(0, stats.jumpForce), ForceMode2D.Impulse);
        playerAudio.Jump();
    }

    void UpdateDirection()
    {
        var moveInput = playerActions.ReadMoveValue();

        if ((moveInput < 0 && transform.localScale.x > 0) ||
            (moveInput > 0 && transform.localScale.x < 0))
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    void UpdateAnimations()
    {
        if (!playerActions.actions.Player.enabled)
            return;

        if (_isFalling)
            _animancer.Play(animations.fall);
        else if (_isJumping)
            _animancer.Play(animations.jump);
        else if (Mathf.Abs(_body.velocity.x) > 0.01)
            _animancer.Play(animations.walk);
        else
            _animancer.Play(animations.idle);
    }

    public void Die()
    {
        StartCoroutine(CO_Die());
    }

    IEnumerator CO_Die()
    {
        transform.Rotate(0, 0, 90);
        _collider.enabled = false;
        if (cleets) cleets.enabled = false;
        _body.bodyType = RigidbodyType2D.Kinematic;
        playerActions.DisableControls();
        // var state = _animancer.Play(animations.die);
        // yield return state;

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    [Serializable]
    class Stats
    {
        public float maxSpeed = 5;
        public float turnAcceleration = 5;
        public float acceleration = 5;
        public float deceleration = 5;
        
        public float jumpForce = 8;
        public float gravityForce = 1;
        public float fallingGravityMultiplier = 1.2f;
        public float jumpInputBuffer = 0.1f;
    }

    [Serializable]
    class Animations
    {
        public AnimationClip idle;
        public AnimationClip walk;
        public AnimationClip jump;
        public AnimationClip fall;
        public AnimationClip die;
    }
}