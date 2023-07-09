using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _waveFinishedPanel;

    private Nexus _nexus;
    
    public bool GameOver = false;
    
    public bool IsGamePaused => _pauseMenuPanel.activeSelf;

    private void Awake()
    {
        _nexus = FindObjectOfType<Nexus>();
        _nexus.OnDeath += SetGameOver;
        
        _pauseMenuPanel.SetActive(false);
        _gameOverPanel.SetActive(false);
        _waveFinishedPanel.SetActive(false);
    }

    private void SetGameOver(Entity obj)
    {
        GameOver = true;
        
        _gameOverPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = IsGamePaused || GameOver;
        Time.timeScale = IsGamePaused || GameOver ? 0 : 1;

        if (GameOver)
        {
            _waveFinishedPanel.SetActive(false);
        }
    }
}
