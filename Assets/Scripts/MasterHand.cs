using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MasterHand : MonoBehaviour
{
    [SerializeField] private Tower _alliedTowerPrefab;

    [SerializeField] private List<Transform> _towerTransforms;

    [SerializeField] private float _moveTime;
    
    private Tower _alliedTower;

    private Vector3 _initialPos;
    private Vector3? _targetPos;

    private float _t;

    public GameObject CubeMesh;
    
    private WaveUI_Manager _waveUIManager;

    public event Action OnTowerPlaced;

    public Vector3 InitialPos
    {
        get => _initialPos;
        set => _initialPos = value;
    }

    private void Awake()
    {
        _initialPos = transform.position;
        
        _waveUIManager = FindObjectOfType<WaveUI_Manager>();
        _waveUIManager.OnWaveTransFinished += AddTower;

        CubeMesh.SetActive(false);
    }

    private void AddTower()
    {
        if (_towerTransforms.Count > 0)
        {
            _alliedTower = Instantiate(_alliedTowerPrefab, transform.position, Quaternion.identity);

            int rndProj = Random.Range(1, 4);

            switch (rndProj)
            {
                case 1:
                    _alliedTower.GetComponent<AI_TowerAttackController>()._currentProjType = ProjectileType.LASER;
                    break;
                case 2:
                    _alliedTower.GetComponent<AI_TowerAttackController>()._currentProjType = ProjectileType.CANON_BULLET;
                    break;
                case 3:
                    _alliedTower.GetComponent<AI_TowerAttackController>()._currentProjType = ProjectileType.FIRE_BALL;
                    break;
            }
            
            int rndIdx = Random.Range(0, _towerTransforms.Count);
            _targetPos = _towerTransforms[rndIdx].position;
            _towerTransforms.Remove(_towerTransforms[rndIdx]);

            StartCoroutine(InterpolateMoveCo(_initialPos, _targetPos.Value));
        }
        else
        {
            OnTowerPlaced?.Invoke();
        }
        
    }
    
    public IEnumerator InterpolateMoveCo(Vector3 startPos, Vector3 endPos) 
    {
        float countTime = 0;
        
        while( countTime <= _moveTime) 
        { 
            float percentTime = countTime / _moveTime;
            transform.position = Vector3.Lerp(startPos, endPos, percentTime);
            
            yield return null; // wait for next frame
            countTime += Time.deltaTime;
        }
        
        // because of the frame rate and the way we are running Lerp(),
        // the last timePercent in the loop may not = 1
        // therefore, this line ensures we end exactly where desired.
        transform.position = endPos;
        
        OnTowerPlaced?.Invoke();

        _alliedTower = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (_alliedTower)
        {
            _alliedTower.transform.position = transform.position;
        }
    }
}
