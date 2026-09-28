using UnityEngine;
using TMPro;

namespace TinyEngineers.Interaction
{
    public class InteractionPromptUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI promptText;
        [SerializeField] private GameObject uiPanel;
        [SerializeField] private Transform targetTransform;
        [SerializeField] private Vector3 offset = new Vector3(0, 2f, 0);

        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
            Hide();
        }

        private void LateUpdate()
        {
            if (uiPanel.activeSelf && targetTransform != null && mainCamera != null)
            {
                transform.position = targetTransform.position + offset;
                transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                                 mainCamera.transform.rotation * Vector3.up);
            }
        }

        public void Show(string text, Transform target)
        {
            if (promptText != null) promptText.text = text;
            targetTransform = target;
            if (uiPanel != null) uiPanel.SetActive(true);
        }

        public void Hide()
        {
            if (uiPanel != null) uiPanel.SetActive(false);
            targetTransform = null;
        }
    }
}
