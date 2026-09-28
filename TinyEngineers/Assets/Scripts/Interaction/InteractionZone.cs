using UnityEngine;

namespace TinyEngineers.Interaction
{
    [RequireComponent(typeof(Collider))]
    public class InteractionZone : MonoBehaviour, IInteractable
    {
        [SerializeField] private string promptText = "[INTERACT]";
        [SerializeField] private UnityEngine.Events.UnityEvent onInteract;
        
        public bool IsActive { get; set; } = true;

        private void Awake()
        {
            Collider col = GetComponent<Collider>();
            col.isTrigger = true;
        }

        public void Interact(GameObject interactor)
        {
            if (IsActive)
            {
                onInteract?.Invoke();
            }
        }

        public bool CanInteract(GameObject interactor)
        {
            return IsActive;
        }

        public string GetInteractionPrompt()
        {
            return promptText;
        }
    }
}
