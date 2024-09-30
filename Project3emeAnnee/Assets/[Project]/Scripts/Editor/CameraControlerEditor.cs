using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraControler)), CanEditMultipleObjects]
public class CameraControlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CameraControler controler = (CameraControler)target;
        base.OnInspectorGUI();
        GUILayout.Space(10);
        if (GUILayout.Button("Look At Target")) controler.LookAtTarget();
        GUILayout.Space(15);
        if (GUILayout.Button("Save Moving Data")) controler.SaveMovingData();
        if (GUILayout.Button("Save TD Data")) controler.SaveTowerDefenceData();
        GUILayout.Space(15);
        if (GUILayout.Button("Reset")) controler.Reset();
    }
}
