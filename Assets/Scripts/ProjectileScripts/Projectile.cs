using UnityEngine;

public enum ProjectileType
{
    LASER,
    CANON_BULLET,
    FIRE_BALL,
}

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float _baseDamage;
    protected float _damage;

    public float BaseDamage
    {
        get => _baseDamage;
        set => _baseDamage = value;
    }

    protected virtual void Awake()
    {
        _damage = _baseDamage;
    }

    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }
}
