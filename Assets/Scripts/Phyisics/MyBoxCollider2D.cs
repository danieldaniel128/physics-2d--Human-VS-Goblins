using System;
using UnityEngine;
public class MyBoxCollider2D : MonoBehaviour
{
    public float Width;
    public float Height;
    public float WidthOffSet;
    public float HeightOffSet;
    public float Mass;
    public float RotationAngle => transform.rotation.eulerAngles.z;
    public event Action<MyBoxCollider2D> OnCollision;

    //adding מקדם חיכוך

    public bool isColliding = false;

    private Quaternion _eulerAngleEdges => Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
    //private void Update()
    //{
    //    DrawMyBoxColliderInGame();
    //}
    private void OnDrawGizmos()
    {
        
        if(isColliding)
            Gizmos.color = Color.red;
        else 
            Gizmos.color = Color.green;
        DrawMyBoxColliderGizmo();
    }

    private void DrawMyBoxColliderGizmo() 
    {
        Gizmos.DrawRay(transform.position + _eulerAngleEdges * ((Vector3.left * Width + Vector3.down * Height) / 2f), _eulerAngleEdges * Vector2.right * Width);
        Gizmos.DrawRay(transform.position + _eulerAngleEdges * (Vector3.left * Width + Vector3.up * Height) / 2f, _eulerAngleEdges * Vector2.right * Width);
        Gizmos.DrawRay(transform.position + _eulerAngleEdges * (Vector3.left * Width + Vector3.down * Height) / 2f, _eulerAngleEdges * Vector2.up * Height);
        Gizmos.DrawRay(transform.position + _eulerAngleEdges * (Vector3.right * Width + Vector3.down * Height) / 2f, _eulerAngleEdges * Vector2.up * Height);
    }
    private void DrawMyBoxColliderInGame()
    {
        Debug.DrawRay(transform.position + _eulerAngleEdges * ((Vector3.left * Width + Vector3.down * Height) / 2f), _eulerAngleEdges * Vector2.right * Width);
        Debug.DrawRay(transform.position + _eulerAngleEdges * (Vector3.left * Width + Vector3.up * Height) / 2f, _eulerAngleEdges * Vector2.right * Width);
        Debug.DrawRay(transform.position + _eulerAngleEdges * (Vector3.left * Width + Vector3.down * Height) / 2f, _eulerAngleEdges * Vector2.up * Height);
        Debug.DrawRay(transform.position + _eulerAngleEdges * (Vector3.right * Width + Vector3.down * Height) / 2f, _eulerAngleEdges * Vector2.up * Height);
    }
    public bool CheckCollision(MyBoxCollider2D otherCollider)
    {
        // Find the minimum and maximum x and y values for this collider
        float thisMinX = transform.position.x - Width / 2f;
        float thisMaxX = transform.position.x + Width / 2f;
        float thisMinY = transform.position.y - Height / 2f;
        float thisMaxY = transform.position.y + Height / 2f;

        // Rotate the other collider's position around this collider's center point
        Vector2 otherColliderRotatedPos = Quaternion.Euler(0, 0, -otherCollider.RotationAngle) * (otherCollider.transform.position - transform.position);

        // Find the minimum and maximum x and y values for the other collider
        float otherMinX = otherColliderRotatedPos.x - otherCollider.Width / 2f;
        float otherMaxX = otherColliderRotatedPos.x + otherCollider.Width / 2f;
        float otherMinY = otherColliderRotatedPos.y - otherCollider.Height / 2f;
        float otherMaxY = otherColliderRotatedPos.y + otherCollider.Height / 2f;

        // Check if the x and y ranges overlap
        bool xOverlap = (thisMinX <= otherMaxX && thisMaxX >= otherMinX);
        bool yOverlap = (thisMinY <= otherMaxY && thisMaxY >= otherMinY);

        if (xOverlap && yOverlap)
        {
            //return true;
            return CheckLineSegmentIntersection(otherCollider);
        }

        return false;
    }
    private bool CheckLineSegmentIntersection(MyBoxCollider2D other)
    {
        // Calculate the corners of the two colliders
        Vector2[] cornersA = GetCornerPoints();
        Vector2[] cornersB = other.GetCornerPoints();

        // Loop over each line segment of the two colliders and check for intersection
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (LineSegmentIntersection(cornersA[i], cornersA[(i + 1) % 4], cornersB[j], cornersB[(j + 1) % 4]))
                {
                    return true; // Intersection found
                }
            }
        }

        return false; // No intersection found
    }

    private Vector2[] GetCornerPoints()
    {
        // Calculate the corners of the collider based on its width, height, and offset values
        Vector2 topLeft = transform.position + _eulerAngleEdges * new Vector2(-Width / 2f + WidthOffSet, Height / 2f - HeightOffSet);
        Vector2 topRight = transform.position + _eulerAngleEdges * new Vector2(Width / 2f - WidthOffSet, Height / 2f - HeightOffSet);
        Vector2 bottomLeft = transform.position + _eulerAngleEdges * new Vector2(-Width / 2f + WidthOffSet, -Height / 2f + HeightOffSet);
        Vector2 bottomRight = transform.position + _eulerAngleEdges * new Vector2(Width / 2f - WidthOffSet, -Height / 2f + HeightOffSet);

        return new Vector2[] { topLeft, topRight, bottomRight, bottomLeft };
    }

    private bool LineSegmentIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        // Calculate the line segment parameters
        float x1 = p1.x, y1 = p1.y, x2 = p2.x, y2 = p2.y, x3 = p3.x, y3 = p3.y, x4 = p4.x, y4 = p4.y;

        // Calculate the denominator of the two linear equations
        float den = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);

        // Check if the line segments are parallel or coincident
        if (Mathf.Approximately(den, 0f))
        {
            return false;
        }

        // Calculate the intersection point parameters
        float t1 = (x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4);
        float t2 = (x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3);

        // Calculate the intersection point
        float t = t1 / den;
        float u = -t2 / den;
        Vector2 intersectionPoint = new Vector2(x1 + t * (x2 - x1), y1 + t * (y2 - y1));

        // Check if the intersection point is within the line segments
        if (t >= 0f && t <= 1f && u >= 0f && u <= 1f)
        {
            return true;
        }

        return false;
    }
}
