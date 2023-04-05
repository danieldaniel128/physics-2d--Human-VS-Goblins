using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public static class Physics2DManager
{
    //after, i will do a collider script that different kinds of colliders will inherit from and the array will be of collider
    static List<MyBoxCollider2D> _myBoxColliders2D = new List<MyBoxCollider2D>();
    public static event Action<MyBoxCollider2D> ColliderWasAdded;
    public static event Action<MyBoxCollider2D> ColliderWasRemoved;

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















}
