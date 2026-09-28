using UnityEngine;

namespace TinyEngineers.Input
{
    public class GestureDetector : MonoBehaviour
    {
        private Vector2 startTouchPosition;
        private Vector2 currentTouchPosition;
        private bool stopTouch = false;

        public float swipeThreshold = 50f;

        private void Update()
        {
            if (UnityEngine.Input.touchCount > 0)
            {
                Touch touch = UnityEngine.Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    startTouchPosition = touch.position;
                    stopTouch = false;
                }
                else if (touch.phase == TouchPhase.Moved && !stopTouch)
                {
                    currentTouchPosition = touch.position;
                    Vector2 distance = currentTouchPosition - startTouchPosition;

                    if (distance.magnitude > swipeThreshold)
                    {
                        InputManager.Instance?.TriggerSwipe(distance.normalized);
                        stopTouch = true;
                    }
                    else
                    {
                        InputManager.Instance?.TriggerDrag(startTouchPosition, touch.deltaPosition);
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    if (!stopTouch)
                    {
                        InputManager.Instance?.TriggerTap(touch.position);
                    }
                    stopTouch = true;
                }
            }
            else if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                 // Mouse fallback for testing in editor
                 startTouchPosition = UnityEngine.Input.mousePosition;
                 stopTouch = false;
            }
            else if (UnityEngine.Input.GetMouseButton(0) && !stopTouch)
            {
                 currentTouchPosition = UnityEngine.Input.mousePosition;
                 Vector2 distance = currentTouchPosition - startTouchPosition;
                 
                 if (distance.magnitude > swipeThreshold)
                 {
                     InputManager.Instance?.TriggerSwipe(distance.normalized);
                     stopTouch = true;
                 }
                 else
                 {
                     Vector2 delta = new Vector2(UnityEngine.Input.GetAxis("Mouse X"), UnityEngine.Input.GetAxis("Mouse Y"));
                     InputManager.Instance?.TriggerDrag(startTouchPosition, delta);
                 }
            }
            else if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                 if (!stopTouch)
                 {
                     InputManager.Instance?.TriggerTap(UnityEngine.Input.mousePosition);
                 }
                 stopTouch = true;
            }
        }
    }
}
