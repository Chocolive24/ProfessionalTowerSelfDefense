using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 200f;

    private GameManager _gameManager;
    
    private float _horizontalRotation = 0f;
    private float _verticalRotation   = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameManager.IsGamePaused)
        {
            float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

            _horizontalRotation += mouseX;
            //_horizontalRotation = Mathf.Clamp(_horizontalRotation, -90f, 90f);
        
            _verticalRotation -= mouseY;
            _verticalRotation = Mathf.Clamp(_verticalRotation, -75, 75);

            Quaternion finalRotation = Quaternion.Euler(_verticalRotation, _horizontalRotation, 0f);
        
            transform.rotation = finalRotation;
        }
    }
}
