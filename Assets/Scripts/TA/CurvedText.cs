using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace ATH
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class CurvedText : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float _multiplier;

        private TextMeshProUGUI _text;

        private List<Vector3[]> _characterVerticies = new List<Vector3[]>();
        private float _minX = float.MaxValue;
        private float _maxX = float.MinValue;

        private void Awake()
        {
            if (_text == null)
            {
                _text = GetComponent<TextMeshProUGUI>();
            }
        }

        private void OnEnable()
        {
            _text.OnPreRenderText += UpdateMesh;
            //UpdateMesh(_text.textInfo);
        }

        private void OnDisable()
        {
            _text.OnPreRenderText -= UpdateMesh;
        }

        private void UpdateMesh(TMP_TextInfo textInfo)
        {
            CacheVertsPosition(textInfo);
            UpdateTextVerts();

            //_text.ForceMeshUpdate();
        }

        private void CacheVertsPosition(TMP_TextInfo textInfo)
        {
            _characterVerticies.Clear();

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                var characterInfo = textInfo.characterInfo[i];
                var currentVerts = textInfo.meshInfo[characterInfo.materialReferenceIndex].vertices;
                _characterVerticies.Add(currentVerts);

                foreach (var vert in currentVerts)
                {
                    if (vert.x < _minX)
                        _minX = vert.x;
                    if (vert.x > _maxX)
                        _maxX = vert.x;
                }
            }
        }

        private void UpdateTextVerts()
        {
            for (int i = 0; i < _characterVerticies.Count; i++)
            {
                for (int j = 0; j < _characterVerticies[i].Length; j++)
                {
                    var normalizedPosition = 1 - (_maxX - _characterVerticies[i][j].x) / (_maxX - _minX);
                    _characterVerticies[i][j] += new Vector3(0, _curve.Evaluate(normalizedPosition) * _multiplier, 0);
                }
            }

            for (int i = 0; i < _text.textInfo.meshInfo.Length; i++)
            {
                var meshInfo = _text.textInfo.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                _text.UpdateGeometry(meshInfo.mesh, i);
                _text.SetVerticesDirty();
            }
        }
    }
}