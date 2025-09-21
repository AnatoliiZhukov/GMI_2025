using InteractionSystem;
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
        [Header("Settings")]
        [SerializeField] private KeyCode upKey = KeyCode.E;
        [SerializeField] private KeyCode downKey = KeyCode.Q;
        
        [SerializeField] private KeyCode interactKey = KeyCode.F;
        
        [SerializeField] private float lookSensitivity = 0.25f;
        [SerializeField] private float topClamp = 90.0f;
        [SerializeField] private float bottomClamp = -90.0f;
        
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
        }

        private void Update()
        {
            HandleLook();
            HandleMovement();
            HandleInteraction();
        }

        private void HandleLook()
        {
            var mouseX = Input.GetAxis("Mouse X") * lookSensitivity * 100f * Time.deltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * lookSensitivity * 100f * Time.deltaTime;
            
            var currentRotation = transform.localEulerAngles;

            var newXRotation = currentRotation.x - mouseY;

            if (newXRotation > 180f) newXRotation -= 360f;
            newXRotation = Mathf.Clamp(newXRotation, bottomClamp, topClamp);
            
            transform.localRotation = Quaternion.Euler(newXRotation, currentRotation.y + mouseX, 0f);
        }
        
        private void HandleMovement()
        {
            var moveInput = Vector3.zero;
            moveInput.x = Input.GetAxis("Horizontal");
            moveInput.z = Input.GetAxis("Vertical");
    
            if (Input.GetKey(upKey)) moveInput.y += 1;
            if (Input.GetKey(downKey)) moveInput.y -= 1;

            if (moveInput == Vector3.zero) return;
            
            // Transform the input vector directly by the camera's rotation
            var transformedInput = _cameraTransform.TransformDirection(moveInput);

            _playerMovement.ProcessMoveInput(transformedInput);
        }

        private void HandleInteraction()
        {
            if (!Input.GetKeyDown(interactKey)) return;
            
            _interactor.AttemptInteraction(_cameraTransform);
        }
    }
}
