using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveUI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _newWavePanel;
    [SerializeField] private TextMeshProUGUI _waveNbrTxt;
    
    [SerializeField] private GameObject _waveFinshiedPanel;

    [SerializeField] private float _transitionTime;

    private WaveManager _waveManager;

    public event Action OnWaveTransFinished;

    private void Awake()
    {
        _newWavePanel.SetActive(false);
        _waveFinshiedPanel.SetActive(false);

        _waveManager = FindObjectOfType<WaveManager>();
    }

    public IEnumerator NewWaveTransitionCO(int waveNbr)
    {
        _newWavePanel.SetActive(true);
        _waveNbrTxt.text = "Wave Number : " + waveNbr;

        yield return new WaitForSeconds(_transitionTime);
        
        _newWavePanel.SetActive(false);

        _waveManager.HasWaveStarted = true;
    }
    
    public IEnumerator WaveFinishedTransitionCO()
    {
        _waveFinshiedPanel.SetActive(true);

        yield return new WaitForSeconds(_transitionTime);
        
        _waveFinshiedPanel.SetActive(false);
        
        OnWaveTransFinished?.Invoke();
    }
}
