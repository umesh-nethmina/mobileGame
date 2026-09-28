using UnityEngine;

namespace TinyEngineers.Interaction
{
    public interface IInteractable
    {
        void Interact(GameObject interactor);
        bool CanInteract(GameObject interactor);
        string GetInteractionPrompt();
    }
}
