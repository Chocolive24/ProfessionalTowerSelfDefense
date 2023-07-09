using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _aimPanel;

    private GameManager _gameManager;
    private AttackController _attackController;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _attackController = GetComponent<AttackController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager.GameOver)
        {
            return;
        }
        
        if (!_gameManager.IsGamePaused)
        {
            if (Input.GetMouseButton((int)MouseButton.LeftMouse))
            {
                _attackController.Shoot();
            }
            
            if (Input.GetMouseButtonUp((int)MouseButton.LeftMouse))
            {
                _attackController.DestroyLaser();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _attackController._currentProjType = ProjectileType.LASER;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _attackController._currentProjType = ProjectileType.CANON_BULLET;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _attackController._currentProjType = ProjectileType.FIRE_BALL;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_pauseMenuPanel.activeSelf)
            {
                _pauseMenuPanel.SetActive(true);
                _aimPanel.SetActive(false);
            }
            else
            {
                _pauseMenuPanel.SetActive(false);
                _aimPanel.SetActive(true);
            }
        }
    }
}
