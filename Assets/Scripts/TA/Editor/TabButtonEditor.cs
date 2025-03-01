using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

namespace ATH
{
    [CustomEditor(typeof(TabButton))]
    public class TabButtonEditor : ButtonEditor
    {
        SerializedProperty m_activatedProperty;
        SerializedProperty m_deactivatedProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_activatedProperty = serializedObject.FindProperty("_activated");
            m_deactivatedProperty = serializedObject.FindProperty("_deactivated");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();
            EditorGUILayout.PropertyField(m_activatedProperty);
            EditorGUILayout.PropertyField(m_deactivatedProperty);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
