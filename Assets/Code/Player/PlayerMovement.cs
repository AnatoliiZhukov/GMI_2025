using System;
using UnityEngine;

namespace Player
{
    /*
     * Processes input and moves CharacterController.
     */
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        private bool _isUsingNoclip;
        public void ToggleNoclip()
        {
            _isUsingNoclip = !_isUsingNoclip;
            
            _controller.height = _isUsingNoclip ? 0f : _initialHeight;
            InputProcessor = _isUsingNoclip ? InputProcessor_Noclip : InputProcessor_Walk;
        }
        
        [Header("Settings")]
        [SerializeField] private float _speed = 10f;
        
        [Header("Components")]
        private CharacterController _controller;
        private float _initialHeight;
        
        public Action<Vector3> InputProcessor;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Start()
        {
            InputProcessor = InputProcessor_Walk;
            _initialHeight = _controller.height;
        }

        private void InputProcessor_Walk(Vector3 moveInput)
        {
            // TODO Proper first-person movement
            _controller.Move(Vector3.down * 10f * Time.deltaTime);
        }

        private void InputProcessor_Noclip(Vector3 moveInput)
        {
            _controller.Move(_speed * Time.deltaTime * moveInput);
        }
    }
}
