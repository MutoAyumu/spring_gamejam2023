using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PointHandle))]
public class PointHandleHelper : Editor
{
    PointHandle _target;

    private void OnEnable()
    {
        _target = target as PointHandle;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("追加"))
        {
            //値の保存を行う
            Undo.RecordObject(target, "Add Node");

            StartEndPoint pos = new StartEndPoint();

            if (_target.MovePointArray.Length > 0)
            {
                pos.Start = _target.MovePointArray[_target.MovePointArray.Length - 1].Start + Vector3.right;
                pos.End = _target.MovePointArray[_target.MovePointArray.Length - 1].End + Vector3.left;
            }
            else
            {
                pos.Start = Vector3.right;
                pos.End = Vector3.left;
            }

            ArrayUtility.Add(ref _target.MovePointArray, pos);
        }

        EditorGUIUtility.labelWidth = 50;
        int delete = -1;

        for (int i = 0; i < _target.MovePointArray.Length; i++)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.BeginHorizontal();

            int size = 50;
            EditorGUILayout.BeginVertical(GUILayout.Width(size));
            EditorGUILayout.LabelField("座標 " + i, GUILayout.Width(size));

            if (GUILayout.Button("削除", GUILayout.Width(size)))
            {
                delete = i;
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical();

            StartEndPoint point = new StartEndPoint();

            point.Start = EditorGUILayout.Vector3Field("開始点", _target.MovePointArray[i].Start);
            point.End = EditorGUILayout.Vector3Field("終了点", _target.MovePointArray[i].End);

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed Position");

                _target.MovePointArray[i] = point;
            }
        }

        EditorGUIUtility.labelWidth = 0;

        if (delete != -1)
        {
            Undo.RecordObject(target, "Delete Node");

            ArrayUtility.RemoveAt(ref _target.MovePointArray, delete);
        }
    }
    private void OnSceneGUI()
    {
        for (int i = 0; i < _target.MovePointArray.Length; i++)
        {
            Vector3 start;
            Vector3 end;

            start = _target.transform.TransformPoint(_target.MovePointArray[i].Start);
            end = _target.transform.TransformPoint(_target.MovePointArray[i].End);

            Vector3 newStart = start;
            Vector3 newEnd = end;

            newStart = Handles.PositionHandle(start, Quaternion.identity);
            newEnd = Handles.PositionHandle(end, Quaternion.identity);

            Handles.color = Color.red;
            Handles.DrawDottedLine(start, end, 10);

            if (newStart != start)
            {
                Undo.RecordObject(target, "Moved Start");
                _target.MovePointArray[i].Start = _target.transform.InverseTransformPoint(newStart);
            }
            if (newEnd != end)
            {
                Undo.RecordObject(target, "Moved End");
                _target.MovePointArray[i].End = _target.transform.InverseTransformPoint(newEnd);
            }

            //Handles.SphereHandleCap(0, start, Quaternion.identity, 0.2f, EventType.Repaint);
            //Handles.SphereHandleCap(0, end, Quaternion.identity, 0.2f, EventType.Repaint);
            Handles.Label(start, $"座標{i} : Start");
            Handles.Label(end, $"座標{i} : End");
        }
    }
}
