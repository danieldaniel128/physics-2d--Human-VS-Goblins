using System;
using System.Collections;
using UnityEngine;
public class MyBoxCollider2D : MonoBehaviour
{
    public float Width;
    public float Height;
    public float WidthOffSet;
    public float HeightOffSet;
    public float Mass;

    public float WidthRightAndOffset => Width + WidthOffSet;
    public float WidthLeftAndOffset => Width - WidthOffSet;
    public float HeightUpAndOffset => Height + HeightOffSet;
    public float HeightDownAndOffset => Height - HeightOffSet;
    public float RotationAngle => transform.rotation.eulerAngles.z;
    public event Action<MyBoxCollider2D> OnCollisionExit;
    public event Action<MyBoxCollider2D> OnCollisionEnter;
    public Predicate<MyBoxCollider2D> OnCollisionWith;

    public float CollidedTimer;

    //adding מקדם חיכוך

    public bool isColliding = false;
    public bool staticObject = false;
    public Quaternion EulerAngleEdges => Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);

    public bool FirstCollisionEnter = false;


    private void Start()
    {
        if (staticObject)
        { 
            Mass = float.PositiveInfinity;
        }
            Physics2DManager.Instance._myBoxColliders2D.Add(this);
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
        Gizmos.DrawRay(transform.position + EulerAngleEdges * ((Vector3.left * WidthLeftAndOffset + Vector3.down * HeightDownAndOffset) / 2f), EulerAngleEdges * Vector2.right * Width);
        Gizmos.DrawRay(transform.position + EulerAngleEdges * (Vector3.left * WidthLeftAndOffset + Vector3.up * HeightUpAndOffset) / 2f, EulerAngleEdges * Vector2.right * Width);
        Gizmos.DrawRay(transform.position + EulerAngleEdges * (Vector3.left * WidthLeftAndOffset + Vector3.down * HeightDownAndOffset) / 2f, EulerAngleEdges * Vector2.up * Height);
        Gizmos.DrawRay(transform.position + EulerAngleEdges * (Vector3.right * WidthRightAndOffset + Vector3.down * HeightDownAndOffset) / 2f, EulerAngleEdges * Vector2.up * Height);
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

       float thisMinX = transform.position.x - WidthLeftAndOffset / 2f;
       float thisMaxX = transform.position.x + WidthRightAndOffset / 2f;
        float thisMinY = transform.position.y - HeightDownAndOffset / 2f;
        float thisMaxY = transform.position.y + HeightUpAndOffset / 2f;

        // Rotate the other collider's position around this collider's center point
        //Vector2 otherColliderRotatedPos = Quaternion.Euler(0, 0, -otherCollider.RotationAngle) * (otherCollider.transform.position - transform.position);

        // Find the minimum and maximum x and y values for the other collider
        float otherMinX = otherCollider.transform.position.x - otherCollider.WidthLeftAndOffset / 2f;
        float otherMaxX = otherCollider.transform.position.x + otherCollider.WidthRightAndOffset / 2f;
        float otherMinY = otherCollider.transform.position.y - otherCollider.HeightDownAndOffset / 2f;
        float otherMaxY = otherCollider.transform.position.y + otherCollider.HeightUpAndOffset / 2f;

        // Check if the x and y ranges overlap
        bool xOverlap = (thisMinX <= otherMaxX && thisMaxX >= otherMinX) || (thisMinX >= otherMaxX && thisMaxX <= otherMinX);
        bool yOverlap = (thisMinY <= otherMaxY && thisMaxY >= otherMinY) || (thisMinY >= otherMaxY && thisMaxY <= otherMinY);

        if (xOverlap && yOverlap)
        {
            //isColliding=true;
            return true; //CheckLineSegmentIntersection(otherCollider);
        }
       
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


    

    
    public void InvokeOnCollisionExit(MyBoxCollider2D collider)
    {
        FirstCollisionEnter = true;
        OnCollisionExit?.Invoke(collider);
    }
    public void InvokeOnCollisionEnter(MyBoxCollider2D collider)
    {
        OnCollisionEnter?.Invoke(collider);
    }
    public void SetOnCollisionWith(Predicate<MyBoxCollider2D> predicate1)
    {
        OnCollisionWith = predicate1;
    }


}
