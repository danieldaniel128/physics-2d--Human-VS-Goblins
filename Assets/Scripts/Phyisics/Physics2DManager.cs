using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Physics2DManager
{
    //after, i will do a collider script that different kinds of colliders will inherit from and the array will be of collider
    public static List<MyBoxCollider2D> _myBoxColliders2D = new List<MyBoxCollider2D>();//collider get added with the events.
    public static event Action<MyBoxCollider2D> ColliderWasAdded;
    public static event Action<MyBoxCollider2D> ColliderWasRemoved;

    public static float DeltaTime;


    public static void DoManagering()
    {
        CollidersAreTouching();
    }

    public static void PrintColliders()
    {
        foreach (var item in _myBoxColliders2D)
        {
            Debug.Log(item);
        }
        Debug.Log("///////////");
    }

    #region Colliding Logic 
    //OnCollision?
    public static void CollidersAreTouching() 
    {
        //_myBoxColliders2D.Where(a => _myBoxColliders2D.Where(b =>b!=a && Vector3.Distance(a.transform.position,b.transform.position) < )  )
        for (int i = 0; i < _myBoxColliders2D.Count; i++)
        {
            for (int j = i+1; j < _myBoxColliders2D.Count; j++)
            {
                if (_myBoxColliders2D[i].CheckCollision(_myBoxColliders2D[j]))
                {
                    _myBoxColliders2D[i].isColliding = true;
                    _myBoxColliders2D[j].isColliding = true;
                }
                else
                {
                    _myBoxColliders2D[i].isColliding = false;
                    _myBoxColliders2D[j].isColliding = false;
                }
            }
        }
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
