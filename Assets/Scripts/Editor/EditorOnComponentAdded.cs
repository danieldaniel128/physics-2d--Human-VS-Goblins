using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class EditorOnComponentAdded
{
    static EditorOnComponentAdded()
    {
        ObjectFactory.componentWasAdded -= HandleComponentAdded;
        ObjectFactory.componentWasAdded += HandleComponentAdded;
        EditorApplication.quitting -= OnEditorQuiting;
        EditorApplication.quitting += OnEditorQuiting;
        Physics2DManager.OnAddToCollider(); 
    }



    private static void HandleComponentAdded(Component obj)
    {
        if (obj is MyBoxCollider2D)
        {
            (obj as MyBoxCollider2D).Width = obj.GetComponent<SpriteRenderer>().bounds.size.x;
            (obj as MyBoxCollider2D).Height = obj.GetComponent<SpriteRenderer>().bounds.size.y;
            Physics2DManager.InvokeColliderWasAdded(obj as MyBoxCollider2D);
        }
    }
    private static void OnEditorQuiting()
    {
        ObjectFactory.componentWasAdded -= HandleComponentAdded;
        EditorApplication.quitting -= OnEditorQuiting;
    }
}

[CustomEditor(typeof(MyBoxCollider2D))]
public class CustomInspectorMyBoxCollider2D : Editor
{
        public override void OnInspectorGUI()
        {
            MyBoxCollider2D myBoxCollider2D = (MyBoxCollider2D)target;
            myBoxCollider2D.Width = EditorGUILayout.FloatField("Width", myBoxCollider2D.Width);
            myBoxCollider2D.Height = EditorGUILayout.FloatField("Height", myBoxCollider2D.Height);
            myBoxCollider2D.WidthOffSet = EditorGUILayout.FloatField("Width OffSet", myBoxCollider2D.WidthOffSet);
            myBoxCollider2D.HeightOffSet = EditorGUILayout.FloatField("Height OffSet", myBoxCollider2D.HeightOffSet);
            myBoxCollider2D.Mass = EditorGUILayout.FloatField("Mass", myBoxCollider2D.Mass);
            EditorGUILayout.FloatField("Rotation Angle", myBoxCollider2D.RotationAngle);//readonly


        if (GUILayout.Button("Reset Values"))
            {
                myBoxCollider2D.Width = myBoxCollider2D.GetComponent<SpriteRenderer>().bounds.size.x;
                myBoxCollider2D.Height = myBoxCollider2D.GetComponent<SpriteRenderer>().bounds.size.y;
            }
        }
}
