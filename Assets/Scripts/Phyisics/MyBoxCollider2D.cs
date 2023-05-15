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
    public bool staticObject = false;
    public Quaternion EulerAngleEdges => Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);

    private void Start()
    {
        if(staticObject)
            Mass = float.PositiveInfinity;

    }

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
        Gizmos.DrawRay(transform.position + EulerAngleEdges * ((Vector3.left * Width + Vector3.down * Height) / 2f), EulerAngleEdges * Vector2.right * Width);
        Gizmos.DrawRay(transform.position + EulerAngleEdges * (Vector3.left * Width + Vector3.up * Height) / 2f, EulerAngleEdges * Vector2.right * Width);
        Gizmos.DrawRay(transform.position + EulerAngleEdges * (Vector3.left * Width + Vector3.down * Height) / 2f, EulerAngleEdges * Vector2.up * Height);
        Gizmos.DrawRay(transform.position + EulerAngleEdges * (Vector3.right * Width + Vector3.down * Height) / 2f, EulerAngleEdges * Vector2.up * Height);
    }
    private void DrawMyBoxColliderInGame()
    {
        Debug.DrawRay(transform.position + EulerAngleEdges * ((Vector3.left * Width + Vector3.down * Height) / 2f), EulerAngleEdges * Vector2.right * Width);
        Debug.DrawRay(transform.position + EulerAngleEdges * (Vector3.left * Width + Vector3.up * Height) / 2f, EulerAngleEdges * Vector2.right * Width);
        Debug.DrawRay(transform.position + EulerAngleEdges * (Vector3.left * Width + Vector3.down * Height) / 2f, EulerAngleEdges * Vector2.up * Height);
        Debug.DrawRay(transform.position + EulerAngleEdges * (Vector3.right * Width + Vector3.down * Height) / 2f, EulerAngleEdges * Vector2.up * Height);
    }
    public bool CheckCollision(MyBoxCollider2D otherCollider)
    {
        //Find the minimum and maximum x and y values for this collider

       float thisMinX = transform.position.x - Width / 2f;
       float thisMaxX = transform.position.x + Width / 2f;
        float thisMinY = transform.position.y - Height / 2f;
        float thisMaxY = transform.position.y + Height / 2f;

        // Rotate the other collider's position around this collider's center point
        //Vector2 otherColliderRotatedPos = Quaternion.Euler(0, 0, -otherCollider.RotationAngle) * (otherCollider.transform.position - transform.position);

        // Find the minimum and maximum x and y values for the other collider
        float otherMinX = otherCollider.transform.position.x - otherCollider.Width / 2f;
        float otherMaxX = otherCollider.transform.position.x + otherCollider.Width / 2f;
        float otherMinY = otherCollider.transform.position.y - otherCollider.Height / 2f;
        float otherMaxY = otherCollider.transform.position.y + otherCollider.Height / 2f;

        // Check if the x and y ranges overlap
        bool xOverlap = (thisMinX <= otherMaxX && thisMaxX >= otherMinX) || (thisMinX >= otherMaxX && thisMaxX <= otherMinX);
        bool yOverlap = (thisMinY <= otherMaxY && thisMaxY >= otherMinY) || (thisMinY >= otherMaxY && thisMaxY <= otherMinY);

        if (xOverlap && yOverlap)
        {
            //isColliding=true;
            return true; //CheckLineSegmentIntersection(otherCollider);
        }
        //if (Mathf.Max(Height / 2, Width / 2) == Width / 2)
        //{
        //    Vector2[] collisionCorners = otherCollider.GetCornerPoints();
        //    foreach (var item in collisionCorners)
        //    {
        //        float DistanceFromCorner = Vector2.Distance(item, transform.position);//(6.63, -0.41)
        //        if (DistanceFromCorner <= Width / 2 || DistanceFromCorner <= Height / 2)
        //        {
        //            if (DistanceFromCorner <= Height / 2)
        //            {
        //                Debug.Log("works");
        //                return true;
        //            }
        //        }
        //    }

        //}
        //else
        //    foreach (var item in otherCollider.GetCornerPoints())
        //    {
        //        float DistanceFromCorner = Vector2.Distance(item, transform.position);//(3.32, -0.17)
        //        if (DistanceFromCorner <= Width / 2 || DistanceFromCorner <= Height / 2)
        //        {
        //            if (DistanceFromCorner <= Width / 2)
        //            {
        //                Debug.Log("works");
        //                return true;
        //            }
        //        }
        //    }

        return false;
    }






    public Vector2[] GetCornerPoints()
    {
        // Calculate the corners of the collider based on its width, height, and offset values
        Vector2 topLeft = transform.position + EulerAngleEdges * new Vector2(-Width / 2f + WidthOffSet, Height / 2f - HeightOffSet);
        Vector2 topRight = transform.position + EulerAngleEdges * new Vector2(Width / 2f - WidthOffSet, Height / 2f - HeightOffSet);
        Vector2 bottomLeft = transform.position + EulerAngleEdges * new Vector2(-Width / 2f + WidthOffSet, -Height / 2f + HeightOffSet);
        Vector2 bottomRight = transform.position + EulerAngleEdges * new Vector2(Width / 2f - WidthOffSet, -Height / 2f + HeightOffSet);
        return new Vector2[] { topLeft, topRight, bottomRight, bottomLeft };
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

    private bool LineSegmentIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        // Calculate the line segment parameters
        Vector2 dirA = p2 - p1;
        Vector2 dirB = p4 - p3;
        float den = dirA.x * dirB.y - dirA.y * dirB.x;

        // Check if the line segments are parallel or coincident
        const float epsilon = 0.0001f;
        if (Mathf.Abs(den) < epsilon)
        {
            return false;
        }

        // Calculate the intersection point parameters
        Vector2 diff = p1 - p3;
        float t = (diff.x * dirB.y - diff.y * dirB.x) / den;
        float u = (diff.x * dirA.y - diff.y * dirA.x) / den;

        // Check if the intersection point is within the line segments
        if (t >= 0f && t <= 1f && u >= 0f && u <= 1f)
        {
            return true;
        }

        return false;
    }


    #region Ai Tests
    //public bool CheckCollision(MyBoxCollider2D otherCollider)
    //{
    //    Vector2[] cornersA = GetCornerPoints();
    //    Vector2[] cornersB = otherCollider.GetCornerPoints();

    //    for (int i = 0; i < cornersA.Length; i++)
    //    {
    //        for (int j = 0; j < cornersB.Length; j++)
    //        {
    //            if (CheckPointInsideCollider(cornersA[i], otherCollider) ||
    //                CheckPointInsideCollider(cornersB[j], this))
    //            {
    //                Debug.Log("Collision detected");
    //                return true;
    //            }
    //        }
    //    }

    //    return false;
    //}

    private bool CheckPointInsideCollider(Vector2 point, MyBoxCollider2D collider)
    {
        Vector2[] corners = collider.GetCornerPoints();
        Vector2 v0 = corners[corners.Length - 1];
        bool isInside = false;

        for (int i = 0; i < corners.Length; i++)
        {
            Vector2 v1 = corners[i];
            Vector2 v2 = corners[(i + 1) % corners.Length];

            if (IsPointOnLeftSide(point, v0, v1) != IsPointOnLeftSide(point, v1, v2))
            {
                isInside = !isInside;
            }

            v0 = v1;
        }

        return isInside;
    }

    private bool IsPointOnLeftSide(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
    {
        return ((lineEnd.x - lineStart.x) * (point.y - lineStart.y) -
                (lineEnd.y - lineStart.y) * (point.x - lineStart.x)) > 0f;
    }
    #endregion


}
