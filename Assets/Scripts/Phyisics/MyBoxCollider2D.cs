using UnityEngine;
using UnityEngine.PlayerLoop;

public class MyBoxCollider2D : MonoBehaviour
{
    public float Width;
    public float Height;
    public float WidthOffSet;
    public float HeightOffSet;
    public float Mass;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
                Gizmos.DrawRay(transform.position + (Vector3.left * Width + Vector3.down * Height) / 2f, Vector2.right * Width);
    }

    //adding מקדם חיכוך
}
