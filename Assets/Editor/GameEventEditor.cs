using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameEventScriptableObject))]
public class ObjectBuilderEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameEventScriptableObject gameEvent = (GameEventScriptableObject)target;
        if(GUILayout.Button("Raise"))
        {
            gameEvent.Raise();
        }
    }
}