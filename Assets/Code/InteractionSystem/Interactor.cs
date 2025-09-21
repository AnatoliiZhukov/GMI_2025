using UnityEngine;

namespace InteractionSystem
{
    /*
     * The component that triggers interactions.
     */
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private float interactRange = 3f;
        
        // Traces a ray and triggers the interaction if the ray hits an Interactable
        public void AttemptInteraction(Transform cameraTransform)
        {
            var ray = new Ray(cameraTransform.position, cameraTransform.forward);

            if (!Physics.Raycast(ray, out var hit, interactRange)) return;
            var hitInteractable = hit.collider.GetComponent<IInteractable>();
            hitInteractable?.Interact();
        }
    }
}
