using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected int _maxHp;
    protected int _currentHp;

    [SerializeField] protected float _invicibilityTime;
    protected float _invicibilityTimePassed = 0f;

    public List<GameObject> _gameObjectMeshes;

    public bool IsDead = false;
    
    public event Action<int, int> OnHealthChange;

    public event Action<Entity> OnDeath;

    public float InvicibilityTime => _invicibilityTime;
    public float InvicibilityTimePassed => _invicibilityTimePassed;

    protected bool _hasTakeDamage = false;
    
    private ParticleSystem _particleSystemDeath;

    protected void Awake()
    {
        _currentHp = _maxHp;
        
        _particleSystemDeath = GetComponent<ParticleSystem>();
        
        if (_particleSystemDeath)
        {
            _particleSystemDeath.Pause();
        }
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

    public virtual void TakeDamage(int damage)
    {
        if (IsDead)
        {
            return;
        }
        
        _currentHp -= damage;
        OnHealthChange?.Invoke(_maxHp, _currentHp);

        _hasTakeDamage = true;
        
        if (_currentHp <= 0)
        {
            IsDead = true;
            
            OnDeath?.Invoke(this);

            if (_particleSystemDeath)
            {
                _particleSystemDeath.Play();
            }

            StartCoroutine(nameof(DestroyCO));
        }
    }

    private IEnumerator DestroyCO()
    {
        if (_particleSystemDeath)
        {
            foreach (var mesh in _gameObjectMeshes)
            {
                mesh.SetActive(false);
            }

            yield return new WaitForSeconds(1f);
        }
        
        Destroy(gameObject);
    }
}
