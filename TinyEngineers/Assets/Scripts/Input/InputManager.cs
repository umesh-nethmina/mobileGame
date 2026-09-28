using UnityEngine;
using System;

namespace TinyEngineers.Input
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        public Vector2 JoystickInput { get; private set; }
        
        public event Action<Vector2> OnTap;
        public event Action<Vector2, Vector2> OnDrag;
        public event Action<Vector2> OnSwipe;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void UpdateJoystickInput(Vector2 input)
        {
            JoystickInput = input;
        }

        public void TriggerTap(Vector2 position) => OnTap?.Invoke(position);
        public void TriggerDrag(Vector2 startPos, Vector2 delta) => OnDrag?.Invoke(startPos, delta);
        public void TriggerSwipe(Vector2 direction) => OnSwipe?.Invoke(direction);
    }
}
