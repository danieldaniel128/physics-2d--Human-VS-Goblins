using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
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
