using UnityEngine;

public class AoeBullet : Bullet
{
    [SerializeField] private GameObject _aoeDebug;
    [SerializeField] private float _aoeDuration;
    private float _timePassedCD;
    
    private bool _hasExploded;

    public Vector3 AoePos;
    
    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (_hasExploded)
        {
            _timePassedCD += Time.deltaTime;

            if (_timePassedCD >= _aoeDuration)
            {
                
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        Collider[] colliders = Physics.OverlapSphere(collision.transform.position, 4f);

        foreach (var collider in colliders)
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();

            if (enemy)
            {
                enemy.TakeDamage((int)_damage);
            }
        }
        
        
        
        Destroy(gameObject);
    }

    public void DebugAOe()
    {
        Instantiate(_aoeDebug, AoePos, Quaternion.identity);
    }
}
