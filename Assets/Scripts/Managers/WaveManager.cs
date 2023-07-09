using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Enemy _baseEnemy;

    [SerializeField] private float _spawnTime;
    private float _timePassed;

    [SerializeField] private int _nbrofEnemyPerSpawn = 3;

    [SerializeField] private int _totalNbrEnemyWave1;
    private int _totalNbrEnemyThisWave;
    private int _nbrOfEnemySpawned;
    private int _nbrOfEnemyDead;

    private bool _hasWaveStarted = false;
    
    private int _waveNbr = 1;

    public List<Enemy> _enemies;
    
    [SerializeField] private Transform _leftPosX;
    [SerializeField] private Transform _rightPosX;
    [SerializeField] private Transform _maxPosZ;

    private WaveUI_Manager _waveUIManager;
    private MasterHand _masterHand;

    public bool HasWaveStarted
    {
        get => _hasWaveStarted;
        set => _hasWaveStarted = value;
    }

    private void Awake()
    {
        _enemies = new List<Enemy>();
        
        _totalNbrEnemyThisWave = _totalNbrEnemyWave1;
        
        _waveUIManager = FindObjectOfType<WaveUI_Manager>();
        _masterHand    = FindObjectOfType<MasterHand>();
        
        _masterHand.OnTowerPlaced += StartNewWave;
        
        _hasWaveStarted = false;
        StartCoroutine(nameof(StartGameCO));
    }

    private IEnumerator StartGameCO()
    {
        yield return new WaitForSeconds(3f);
        
        StartNewWave();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!_hasWaveStarted)
        {
            return;
        }
    
        _timePassed += Time.deltaTime;

        if (_timePassed >= _spawnTime && _nbrOfEnemySpawned < _totalNbrEnemyThisWave)
        {
            for (int i = 0; i < _nbrofEnemyPerSpawn; i++)
            {
                float rndXPos = Random.Range(_leftPosX.position.x, _rightPosX.position.x);
                float rndZPos = Random.Range(_leftPosX.position.z, _maxPosZ.position.z);
            
                Enemy enemy = Instantiate(_baseEnemy, new Vector3(rndXPos, 0, rndZPos), Quaternion.identity);

                enemy.OnDeath += IncrementEnemiesDead;
                
                _timePassed = 0f;

                _nbrOfEnemySpawned++;
                _enemies.Add(enemy);
            }
        }

        if (_nbrOfEnemyDead >= _totalNbrEnemyThisWave)
        {
            _hasWaveStarted = false;
            
            _waveNbr++;
            _nbrOfEnemySpawned = 0;
            _nbrOfEnemyDead = 0;
            
            _masterHand.GetComponent<MeshRenderer>().enabled = true;
            
            StartCoroutine(_waveUIManager.WaveFinishedTransitionCO());
        }
    }
    
    private void StartNewWave()
    {
        Debug.Log("START WAVE");
        
        _masterHand.GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(_waveUIManager.NewWaveTransitionCO(_waveNbr));

        if (_waveNbr <= 1)
        {
            return;
        }
        
        //_nbrofEnemyPerSpawn++;
        _totalNbrEnemyThisWave += 0;
    }

    private void IncrementEnemiesDead(Entity entity)
    {
        _nbrOfEnemyDead++;

        _enemies.Remove(entity.GetComponent<Enemy>());
    }

    public Enemy GetValidEnemy()
    {
        Enemy nonTargetedEnemy = _enemies.FirstOrDefault(enemy => !enemy.IsTargeted);

        if (!nonTargetedEnemy)
        {
            return FindObjectOfType<Enemy>();
        }

        return nonTargetedEnemy;
    }
        
}
