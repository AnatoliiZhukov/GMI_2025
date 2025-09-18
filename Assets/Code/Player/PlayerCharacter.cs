using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Player
{
    [RequireComponent(typeof(Camera))]
    [RequireComponent(typeof(AudioListener))]
    [RequireComponent(typeof(UniversalAdditionalCameraData))]
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerCharacter : MonoBehaviour
    {
        [Header("Controls")]
        [SerializeField] private KeyCode up = KeyCode.E;
        [SerializeField] private KeyCode down = KeyCode.Q;
        [SerializeField] private float lookSensitivity = 0.25f;
        
        [Header("Components")]
        private Camera _camera;
        private PlayerMovement _playerMovement;
        
        private float _xRotation = 0f;
        
        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            var mouseX = Input.GetAxis("Mouse X") * lookSensitivity * 100f * Time.deltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * lookSensitivity * 100f * Time.deltaTime;
            
            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            
            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            
            HandleMovement();
        }

        private void HandleMovement()
        {
            var moveInput = Vector3.zero;
            moveInput.x = Input.GetAxis("Horizontal");
            moveInput.z = Input.GetAxis("Vertical");
            if (Input.GetKey(up)) moveInput.y += 1;
            if (Input.GetKey(down)) moveInput.y -= 1;
            _playerMovement.ProcessMoveInput(moveInput);
        }
    }
}
