using UnityEngine;

public class Laser : Projectile
{
    private LineRenderer _lineRenderer;
    private Camera _mainCamera;
    public LineRenderer LineRenderer => _lineRenderer;

    protected override void Awake()
    {
        base.Awake();
        
        _lineRenderer = GetComponent<LineRenderer>();

        _mainCamera = Camera.main;
    }

    public void Shoot(Ray shootingRay, Vector3 adjustedCamPos, ref float timePassedCD)
    {
        Draw(adjustedCamPos, shootingRay.direction * _mainCamera.farClipPlane);

        if (Physics.Raycast(shootingRay, out RaycastHit hit))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();

            if (enemy)
            {
                enemy.TakeDamage((int)_damage);
                Damage += 0.2f;
                timePassedCD = 0f;
            }
            else
            {
                Damage = _baseDamage;
            }

            Draw(adjustedCamPos, hit.point);
        }
    }

    public void Draw(Vector3 startPos, Vector3 endPos)
    {
        _lineRenderer.enabled = true;
        
        _lineRenderer.SetPosition(0, startPos);
        _lineRenderer.SetPosition(1, endPos);
    }
}
