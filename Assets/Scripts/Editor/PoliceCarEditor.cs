using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PoliceCarController))]
[ExecuteInEditMode]
public class PoliceCarEditor : Editor
{
    SerializedProperty boxCollider;
    SerializedProperty range;

    private void OnEnable()
    {
        boxCollider = serializedObject.FindProperty("boxCollider");
        range = serializedObject.FindProperty("range");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PoliceCarController myScript = (PoliceCarController)target;
        myScript.ScaleCollider();
    }

}
