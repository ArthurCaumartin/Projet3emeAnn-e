using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraControler)), CanEditMultipleObjects]
public class CameraControlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CameraControler controler = (CameraControler)target;
        if(GUILayout.Button("Look At Target"))
            controler.LookAtTarget();
        base.OnInspectorGUI();
    }
}
