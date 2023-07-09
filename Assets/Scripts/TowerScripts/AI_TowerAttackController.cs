using System;
using UnityEngine;

public class AI_TowerAttackController : MonoBehaviour
{
    [SerializeField] private GameObject _projectilOrigin;

    [SerializeField] private ProjectileType _baseProjectileType;
    public ProjectileType _currentProjType;

    [Header("Projectile Prefabs")]
    [SerializeField] private GameObject _laserPrefab;
    private Laser _laser;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _aoeBulletPrefab;

    [Header("Cooldowns")]
    [SerializeField] private float _laserCooldown;
    [SerializeField] private float _bulletCooldown;
    [SerializeField] private float _aoeBulletCooldown;

    private float _timePassedCD;

    private WaveManager _waveManager;

    private bool _hasATarget = false;
    private Enemy _currentEnemyTarget;

    private void Awake()
    {
        _currentProjType = _baseProjectileType;

        _waveManager = FindObjectOfType<WaveManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _timePassedCD += Time.deltaTime;
        
        if (!_hasATarget)
        {
            _currentEnemyTarget = _waveManager.GetValidEnemy();
            _hasATarget = true;
        }

        if (_currentEnemyTarget)
        {
            _currentEnemyTarget.IsTargeted = true;
            Vector3 dir = _currentEnemyTarget.transform.position - _projectilOrigin.transform.position;
            Ray shootingRay = new Ray(_projectilOrigin.transform.position, dir.normalized);
            
            switch (_currentProjType)
            {
                case ProjectileType.LASER:
                    HandleLaser(shootingRay);
                    break;
                case ProjectileType.CANON_BULLET:
                    HandleBullet();
                    break;
                case ProjectileType.FIRE_BALL:
                    HandleAoeBullet(shootingRay);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        else
        {
            _hasATarget = false;
            if (_laser)
            {
                DestroyLaser();
            }
        }
    }
    
    private void HandleLaser(Ray shootingRay)
    {
        if (_laser == null)
        {
            _laser = Instantiate(_laserPrefab, shootingRay.origin, Quaternion.identity).GetComponent<Laser>();
        }
        else
        {
            if (_timePassedCD >= _laserCooldown)
            {
                _laser.Draw(shootingRay.origin, _currentEnemyTarget.transform.position);
                
                if (Physics.Raycast(shootingRay, out RaycastHit hit))
                {
                    Enemy enemy = hit.collider.GetComponent<Enemy>();
        
                    if (enemy)
                    {
                        enemy.TakeDamage((int)_laser.Damage);
                        _laser.Damage += 0.2f;
                        _timePassedCD = 0f;
                    }
                    else
                    {
                        _laser.Damage = _laser.BaseDamage;
                    }
        
                    _laser.Draw(shootingRay.origin, hit.point);
                }
            }
        }
    }

    public void DestroyLaser()
    {
        if (_laser)
        {
            Destroy(_laser.gameObject);
        }
    }

    public void HandleBullet()
    {
        if (_timePassedCD >= _bulletCooldown)
        {
            Bullet bullet = Instantiate(_bulletPrefab, _projectilOrigin.transform.position, 
                Quaternion.identity).GetComponent<Bullet>();

            Vector3 targetDirection = _currentEnemyTarget.transform.position - _projectilOrigin.transform.position;
            bullet.TargetDirection = targetDirection.normalized;
            
            _timePassedCD = 0f;
        }
    }
    
    public void HandleAoeBullet(Ray shootingRay)
    {
        if (_timePassedCD >= _bulletCooldown)
        {
            AoeBullet aoeBullet = Instantiate(_aoeBulletPrefab, _projectilOrigin.transform.position, 
                Quaternion.identity).GetComponent<AoeBullet>();

            Vector3 targetDirection = _currentEnemyTarget.transform.position - _projectilOrigin.transform.position;
            aoeBullet.TargetDirection = targetDirection.normalized;

            if (Physics.Raycast(shootingRay, out RaycastHit hit))
            {
                aoeBullet.AoePos = hit.point;
            }

            _timePassedCD = 0f;
        }
    }
}
