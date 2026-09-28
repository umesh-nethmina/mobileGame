using UnityEngine;

namespace TinyEngineers.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private CarrySystem carrySystem;
        
        private Animator animator;
        private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
        private static readonly int IsCarryingHash = Animator.StringToHash("IsCarrying");
        private static readonly int InteractHash = Animator.StringToHash("Interact");

        private void Awake()
        {
            animator = GetComponent<Animator>();
            if (playerController == null) playerController = GetComponent<PlayerController>();
            if (carrySystem == null) carrySystem = GetComponent<CarrySystem>();
        }

        private void Update()
        {
            if (animator == null) return;
            
            animator.SetBool(IsMovingHash, playerController != null && playerController.IsMoving);
            animator.SetBool(IsCarryingHash, carrySystem != null && carrySystem.IsCarrying);
        }

        public void TriggerInteract()
        {
            if (animator != null)
            {
                animator.SetTrigger(InteractHash);
            }
        }
    }
}
