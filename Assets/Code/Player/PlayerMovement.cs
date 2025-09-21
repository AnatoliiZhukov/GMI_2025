using UnityEngine;

namespace Player
{
    /*
     * Processes input and moves CharacterController.
     */
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float speed = 10f;
        
        [Header("Components")]
        private CharacterController _controller;
        
        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }
        
        public void ProcessMoveInput(Vector3 inMoveInput)
        {
            _controller.Move(speed * Time.deltaTime * inMoveInput);
        }
    }
}
