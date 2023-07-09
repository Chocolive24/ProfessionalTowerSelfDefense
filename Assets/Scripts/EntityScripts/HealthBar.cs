using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarImg;
    [SerializeField] private GameObject _lifeBarPanel;

    private Entity _entity;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        
        _entity = GetComponent<Entity>();

        _entity.OnHealthChange += UpdateHealthBar;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void UpdateHealthBar(int entityMaxHealth, int entityCurrHealth)
    {
        float healthInPercent = entityCurrHealth / (float)entityMaxHealth;

        _healthBarImg.fillAmount = healthInPercent;
    }

    // Update is called once per frame
    void Update()
    {
        _lifeBarPanel.transform.rotation = Quaternion.LookRotation(
            _lifeBarPanel.transform.position - _camera.transform.position);
    }
}
