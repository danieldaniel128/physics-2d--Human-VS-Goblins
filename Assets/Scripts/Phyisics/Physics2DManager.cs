using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Physics2DManager
{
    //after, i will do a collider script that different kinds of colliders will inherit from and the array will be of collider
    static List<MyBoxCollider2D> _myBoxColliders2D = new List<MyBoxCollider2D>();//collider get added with the events.
    public static event Action<MyBoxCollider2D> ColliderWasAdded;
    public static event Action<MyBoxCollider2D> ColliderWasRemoved;

    public static float DeltaTime;





    #region Colliding Logic 
    //OnCollision?
    public void CollidersAreTouching() 
    {
        //_myBoxColliders2D.Where(a => _myBoxColliders2D.Where(b =>b!=a && Vector3.Distance(a.transform.position,b.transform.position) < )  )
        
    
    }
    #endregion



    #region ColliderWasAdded Subsribtion 
    public static void InvokeColliderWasAdded(MyBoxCollider2D colliderWasAdded)
    {
        ColliderWasAdded?.Invoke(colliderWasAdded);
        Debug.Log("invoked");//
        foreach(var adsdas in _myBoxColliders2D)
            Debug.Log(adsdas);
    }

    public static void InvokeColliderWasRemoved(MyBoxCollider2D colliderWasAdded)
    {
        ColliderWasRemoved?.Invoke(colliderWasAdded);
        Debug.Log("invoked");//
        foreach (var adsdas in _myBoxColliders2D)
            Debug.Log(adsdas);
    }
    public static void OnAddToCollider() 
    {
        ColliderWasAdded += AddToColliderList;
    }
    static void AddToColliderList(MyBoxCollider2D collider) 
    {
        _myBoxColliders2D.Add(collider);
    }

    public static void OnRemoveCollider()
    {
        ColliderWasRemoved += RemoveColliderList;
    }
    static void RemoveColliderList(MyBoxCollider2D collider)
    {
        _myBoxColliders2D.Remove(collider);
    }
    #endregion














}
