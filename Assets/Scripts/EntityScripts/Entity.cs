using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected int _maxHp;
    protected int _currentHp;

    [SerializeField] protected float _invicibilityTime;
    protected float _invicibilityTimePassed = 0f;

    public event Action<int, int> OnHealthChange;

    public event Action<Entity> OnDeath;

    public float InvicibilityTime => _invicibilityTime;
    public float InvicibilityTimePassed => _invicibilityTimePassed;

    protected bool _hasTakeDamage = false;

    protected void Awake()
    {
        _currentHp = _maxHp;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (_hasTakeDamage && _invicibilityTimePassed < _invicibilityTime)
        // {
        //     _invicibilityTimePassed += Time.deltaTime;
        // }
        // else
        // {
        //     _hasTakeDamage = false;
        //     _invicibilityTimePassed = 0f;
        // }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("damage");
        
        _currentHp -= damage;
        OnHealthChange?.Invoke(_maxHp, _currentHp);

        _hasTakeDamage = true;
        
        if (_currentHp <= 0)
        {
            OnDeath?.Invoke(this);
            
            Destroy(gameObject);
        }
        
        
    }
}
