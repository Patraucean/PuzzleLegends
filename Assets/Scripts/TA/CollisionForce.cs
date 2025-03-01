using UnityEngine;

namespace ATH
{
	public class CollisionForce : MonoBehaviour
	{
        [SerializeField] private Vector3 _force;

        private void OnCollisionEnter(Collision collision)
        {
        }

        private void OnCollisionExit(Collision collision)
        {
        }

    }
}