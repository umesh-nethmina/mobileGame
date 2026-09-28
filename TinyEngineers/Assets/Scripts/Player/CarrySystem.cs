using UnityEngine;

namespace TinyEngineers.Player
{
    public class CarrySystem : MonoBehaviour
    {
        [SerializeField] private Transform carryPoint;
        
        public GameObject CarriedObject { get; private set; }
        public bool IsCarrying => CarriedObject != null;

        public void PickUp(GameObject obj)
        {
            if (IsCarrying) return;
            
            CarriedObject = obj;
            CarriedObject.transform.SetParent(carryPoint);
            CarriedObject.transform.localPosition = Vector3.zero;
            CarriedObject.transform.localRotation = Quaternion.identity;
            
            // Disable physics on carried object
            Rigidbody rb = CarriedObject.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;
            
            Collider col = CarriedObject.GetComponent<Collider>();
            if (col != null) col.enabled = false;
        }

        public void Drop()
        {
            if (!IsCarrying) return;
            
            CarriedObject.transform.SetParent(null);
            
            // Re-enable physics
            Rigidbody rb = CarriedObject.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;
            
            Collider col = CarriedObject.GetComponent<Collider>();
            if (col != null) col.enabled = true;
            
            CarriedObject = null;
        }
    }
}
