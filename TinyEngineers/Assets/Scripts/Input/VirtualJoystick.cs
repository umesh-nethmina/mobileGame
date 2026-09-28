using UnityEngine;
using UnityEngine.EventSystems;

namespace TinyEngineers.Input
{
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private RectTransform joystickBackground;
        [SerializeField] private RectTransform joystickHandle;
        
        private Vector2 inputVector;
        private float joystickRadius;

        private void Start()
        {
            if (joystickBackground != null)
            {
                joystickRadius = joystickBackground.rect.width / 2f;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (joystickBackground == null || joystickHandle == null) return;

            Vector2 position;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, eventData.pressEventCamera, out position))
            {
                position.x = (position.x / joystickRadius);
                position.y = (position.y / joystickRadius);

                inputVector = new Vector2(position.x, position.y);
                inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

                joystickHandle.anchoredPosition = new Vector2(inputVector.x * joystickRadius, inputVector.y * joystickRadius);
                
                InputManager.Instance?.UpdateJoystickInput(inputVector);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            inputVector = Vector2.zero;
            if (joystickHandle != null)
                joystickHandle.anchoredPosition = Vector2.zero;
            
            InputManager.Instance?.UpdateJoystickInput(inputVector);
        }
    }
}
