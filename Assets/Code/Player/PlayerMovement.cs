using UnityEngine;

/*
 * Simple flying movement script to fly around the scene in play mode.
 */
namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float lookSensitivity = 1f;
        [SerializeField] private float speed = 10f;
        
        [Header("Components")]
        private CharacterController _controller;
        private PlayerCharacter _playerCharacter;
        
        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            
            _playerCharacter = GetComponent<PlayerCharacter>();
            if (_playerCharacter == null)
                Debug.LogError("PlayerMovement can only exist on a GameObject with a PlayerCharacter component.");
        }

        private void Update()
        {
            
        }

        public void ProcessLookInput(Vector2 inLookInput)
        {
            
        }
        
        public void ProcessMoveInput(Vector3 inMoveInput)
        {
            _controller.Move(speed * Time.deltaTime * inMoveInput);
        }
    }
}
