using InteractionSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Player
{
    /*
     * Manages input and core components.
     */
    [RequireComponent(typeof(Camera))]
    [RequireComponent(typeof(AudioListener))]
    [RequireComponent(typeof(UniversalAdditionalCameraData))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(Interactor))]
    public class PlayerCore : MonoBehaviour
    {
        [Header("Controls")]
        [SerializeField] private KeyCode _interactKey = KeyCode.F;
        [SerializeField] private KeyCode _toggleNoclipKey = KeyCode.N;
        [SerializeField, SerializeAs("_noclipUpKey")] private KeyCode _upKey = KeyCode.E;
        [SerializeField, SerializeAs("_noclipDownKey")] private KeyCode _downKey = KeyCode.Q;
        
        [Header("Camera")]
        [SerializeField] private float _lookSensitivity = 0.25f;
        [SerializeField] private float _topClamp = 90.0f;
        [SerializeField] private float _bottomClamp = -90.0f;
        
        [Header("Components")]
        private Camera _camera;
        private PlayerMovement _playerMovement;
        private Interactor _interactor;

        private Transform _cameraTransform;
        
        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _playerMovement = GetComponent<PlayerMovement>();
            _interactor = GetComponent<Interactor>();
        }

        private void Start()
        {
            _cameraTransform = _camera.transform;
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            HandleLook();
            HandleMovement();
            HandleInteraction();
        }

        private void HandleLook()
        {
            var mouseX = Input.GetAxis("Mouse X") * _lookSensitivity * 100f * Time.deltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * _lookSensitivity * 100f * Time.deltaTime;
            
            var currentRotation = transform.localEulerAngles;

            var newXRotation = currentRotation.x - mouseY;

            if (newXRotation > 180f) newXRotation -= 360f;
            newXRotation = Mathf.Clamp(newXRotation, _bottomClamp, _topClamp);
            
            transform.localRotation = Quaternion.Euler(newXRotation, currentRotation.y + mouseX, 0f);
        }
        
        private void HandleMovement()
        {
            if (Input.GetKeyDown(_toggleNoclipKey))
                _playerMovement.ToggleNoclip();
            
            var moveInput = Vector3.zero;
            moveInput.x = Input.GetAxis("Horizontal");
            moveInput.z = Input.GetAxis("Vertical");
    
            if (Input.GetKey(_upKey)) moveInput.y += 1;
            if (Input.GetKey(_downKey)) moveInput.y -= 1;

            if (moveInput == Vector3.zero) return;
            
            // Transform the input vector directly by the camera's rotation
            var transformedInput = _cameraTransform.TransformDirection(moveInput);

            // Process the input using the selected processor
            _playerMovement.InputProcessor?.Invoke(transformedInput);
        }

        private void HandleInteraction()
        {
            if (!Input.GetKeyDown(_interactKey)) return;
            
            _interactor.AttemptInteraction(_cameraTransform);
        }
    }
}
