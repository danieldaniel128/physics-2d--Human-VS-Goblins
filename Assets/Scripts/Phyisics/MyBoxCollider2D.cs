using System;
using UnityEngine;
public class MyBoxCollider2D : MonoBehaviour
{
    public float Width;
    public float Height;
    public float WidthOffSet;
    public float HeightOffSet;
    public float Mass;
    public float RotationAngle;
    public event Action<MyBoxCollider2D> OnCollision;

    //adding מקדם חיכוך

    private Quaternion _eulerAngleEdges => Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + _eulerAngleEdges * ((Vector3.left * Width + Vector3.down * Height) / 2f), _eulerAngleEdges * Vector2.right * Width);
        Gizmos.DrawRay(transform.position + _eulerAngleEdges * (Vector3.left * Width + Vector3.up * Height) / 2f, _eulerAngleEdges * Vector2.right * Width);
        Gizmos.DrawRay(transform.position + _eulerAngleEdges * (Vector3.left * Width + Vector3.down * Height) / 2f, _eulerAngleEdges * Vector2.up * Height);
        Gizmos.DrawRay(transform.position + _eulerAngleEdges * (Vector3.right * Width + Vector3.down * Height) / 2f, _eulerAngleEdges * Vector2.up * Height);
    }
}
