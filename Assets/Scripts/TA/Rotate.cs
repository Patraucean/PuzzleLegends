using UnityEngine;

namespace ATH
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] public Vector3 _direction;

        private void Update()
        {
            transform.Rotate(_direction * Time.deltaTime);
        }
    }
}