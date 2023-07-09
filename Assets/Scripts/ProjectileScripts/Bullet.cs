using System;
using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField] protected int _speed;

    protected Rigidbody _rigidbody;

    protected Vector3 _targetDirection;

    private float t;
    private float livingTime = 10f;

    public Vector3 TargetDirection
    {
        get => _targetDirection;
        set => _targetDirection = value;
    }

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody>();
    }


    protected void Update()
    {
        t += Time.deltaTime;

        if (t >= livingTime)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void FixedUpdate()
    {
        _rigidbody.velocity = _targetDirection * (_speed * Time.deltaTime);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        
        if (enemy)
        {
            enemy.TakeDamage((int)_damage);
        }
        
        Destroy(gameObject);
    }
}
