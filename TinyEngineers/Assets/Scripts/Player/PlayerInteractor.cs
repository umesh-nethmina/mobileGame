using UnityEngine;
using TinyEngineers.Input;
using TinyEngineers.Interaction;

namespace TinyEngineers.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private float interactRange = 2f;
        [SerializeField] private LayerMask interactableLayer;
        
        private IInteractable currentTarget;

        private void OnEnable()
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.OnTap += HandleTap;
            }
        }

        private void OnDisable()
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.OnTap -= HandleTap;
            }
        }

        private void Update()
        {
            FindClosestInteractable();
        }

        private void FindClosestInteractable()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange, interactableLayer);
            
            IInteractable closest = null;
            float minDistance = float.MaxValue;

            foreach (var col in colliders)
            {
                IInteractable interactable = col.GetComponent<IInteractable>();
                if (interactable != null && interactable.CanInteract(gameObject))
                {
                    float distance = Vector3.Distance(transform.position, col.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closest = interactable;
                    }
                }
            }

            if (currentTarget != closest)
            {
                currentTarget = closest;
                // UI update could go here (e.g. showing interaction prompt)
            }
        }

        private void HandleTap(Vector2 screenPosition)
        {
            // For now, tap anywhere to interact with the closest object
            if (currentTarget != null)
            {
                currentTarget.Interact(gameObject);
                GetComponent<PlayerAnimator>()?.TriggerInteract();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactRange);
        }
    }
}
