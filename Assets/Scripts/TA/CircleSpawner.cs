using System.Collections.Generic;
using UnityEngine;

namespace ATH
{
    public class CircleSpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnRange;
        [SerializeField] private float _delay;
        [SerializeField] private GameObject _prefab;

        private List<GameObject> _spawnedElements = new();

        public float Delay => _delay;

        public async void SpawnElements(uint numberOfElements, System.Action<GameObject> onElementSpawned)
        {
            try
            {
                for (int i = 0; i < numberOfElements; i++)
                {
                    // Calculate a random angle between 0 and 360 degrees
                    float angle = Random.Range(0f, 360f);
                    // Convert the angle to radians
                    float angleRad = angle * Mathf.Deg2Rad;

                    // Calculate the position within the circle
                    float x = Mathf.Cos(angleRad) * _spawnRange;
                    float y = Mathf.Sin(angleRad) * _spawnRange;

                    // Instantiate the UI element
                    GameObject uiElement = Instantiate(_prefab, transform);
                    _spawnedElements.Add(uiElement);

                    // Set the local position of the UI element
                    uiElement.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
                    onElementSpawned?.Invoke(uiElement);
                    await Awaitable.WaitForSecondsAsync(_delay, destroyCancellationToken);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("[CIRCLE_SPAWNER]" + e);
            }
        }

        public void Clear()
        {
            for (int i = 0; i < _spawnedElements.Count; i++)
            {
                GameObject item = _spawnedElements[i];
                if (item != null)
                {
                    Destroy(item);
                }
            }
            _spawnedElements.Clear();
        }
    }
}
