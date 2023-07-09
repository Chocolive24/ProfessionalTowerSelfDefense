using System;
using UnityEngine;

public class AttackController : MonoBehaviour
{
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

    private Camera _mainCamera;

    private Vector3 targetPosition;
    private float _timePassedCD;

    private GameManager _gameManager;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _currentProjType = _baseProjectileType;
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        _timePassedCD += Time.deltaTime;
    }

    public void Shoot()
    {
        Vector3 viewportCenter = new Vector3(0.5f, 0.5f, 0f);
        Ray shootingRay = _mainCamera.ViewportPointToRay(viewportCenter);

        Vector3 cameraPos = _mainCamera.transform.position;
        Vector3 adjustedCamPos = new Vector3(cameraPos.x, cameraPos.y - 1f, cameraPos.z);

        targetPosition = shootingRay.direction * _mainCamera.farClipPlane;

        if (Physics.Raycast(shootingRay, out RaycastHit hit))
        {
            targetPosition = hit.point;
        }

        switch (_currentProjType)
        {
            case ProjectileType.LASER:
                HandleLaser(shootingRay, adjustedCamPos);
                break;
            case ProjectileType.CANON_BULLET:
                HandleBullet(shootingRay, adjustedCamPos);
                break;
            case ProjectileType.FIRE_BALL:
                HandleAoeBullet(shootingRay, adjustedCamPos);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void HandleLaser(Ray shootingRay, Vector3 adjustedCamPos)
    {
        if (_laser == null)
        {
            _laser = Instantiate(_laserPrefab, shootingRay.origin, Quaternion.identity).GetComponent<Laser>();
        }
        else
        {
            if (_timePassedCD >= _laserCooldown)
            {
                _laser.Shoot(shootingRay, adjustedCamPos, ref _timePassedCD);
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

    public void HandleBullet(Ray shootingRay, Vector3 adjustedCamPos)
    {
        if (_timePassedCD >= _bulletCooldown)
        {
            Bullet bullet = Instantiate(_bulletPrefab, adjustedCamPos, 
                Quaternion.identity).GetComponent<Bullet>();

            //Instantiate(_canonBulletPrefab, targetPosition, Quaternion.identity);
            
            Vector3 targetDirection = targetPosition - adjustedCamPos;
            bullet.TargetDirection = targetDirection.normalized;
            
            _timePassedCD = 0f;
        }
    }
    
    public void HandleAoeBullet(Ray shootingRay, Vector3 adjustedCamPos)
    {
        if (_timePassedCD >= _bulletCooldown)
        {
            AoeBullet aoeBullet = Instantiate(_aoeBulletPrefab, adjustedCamPos, 
                Quaternion.identity).GetComponent<AoeBullet>();

            Vector3 targetDirection = targetPosition - adjustedCamPos;
            aoeBullet.TargetDirection = targetDirection.normalized;

            if (Physics.Raycast(shootingRay, out RaycastHit hit))
            {
                aoeBullet.AoePos = hit.point;
            }

            _timePassedCD = 0f;
        }
    }
}
