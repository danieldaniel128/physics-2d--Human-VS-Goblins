using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Physics2DManager : MonoBehaviour
{
    public static Physics2DManager Instance;
    //after, i will do a collider script that different kinds of colliders will inherit from and the array will be of collider
    public List<MyBoxCollider2D> _myBoxColliders2D;//collider get added with the events.
    public List<MyRigidBody2D> _myRigidbody2Ds;

    public event Action OnEverySecond;


    public event Action<MyBoxCollider2D> ColliderWasAdded;
    public event Action<MyBoxCollider2D> ColliderWasRemoved;


    /*[SerializeField]*/
    float _timer;
    [SerializeField] float _timerInSeconds;


    //public static float DeltaTime;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        //AddOnEverySecond(DoManagering);
    }
    private void Update()
    {
        DoManagering();
        GameTimer();
    }
    void GameTimer() 
    {
        _timer += Time.deltaTime;
        if (_timer >= 1)
        {
            _timer = 0;
            _timerInSeconds++;
            InvokeOnEverySecond();
        }
    }

    public void DoManagering()
    {
        CollidersAreTouching();
    }

    public void PrintColliders()
    {
        foreach (var item in _myBoxColliders2D)
        {
            Debug.Log(item);
        }
        Debug.Log("///////////");
    }

    #region Colliding Logic 
    //OnCollision?
    public void CollidersAreTouching() 
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
    public void InvokeColliderWasAdded(MyBoxCollider2D colliderWasAdded)
    {
        ColliderWasAdded?.Invoke(colliderWasAdded);
        Debug.Log("invoked");//
        foreach(var adsdas in _myBoxColliders2D)
            Debug.Log(adsdas);
    }

    public void InvokeColliderWasRemoved(MyBoxCollider2D colliderWasAdded)
    {
        ColliderWasRemoved?.Invoke(colliderWasAdded);
        Debug.Log("invoked");//
        foreach (var adsdas in _myBoxColliders2D)
            Debug.Log(adsdas);
    }
    public void OnAddToCollider() 
    {
        ColliderWasAdded += AddToColliderList;
    }
    void AddToColliderList(MyBoxCollider2D collider) 
    {
        _myBoxColliders2D.Add(collider);
    }

    public void OnRemoveCollider()
    {
        ColliderWasRemoved += RemoveColliderList;
    }
    void RemoveColliderList(MyBoxCollider2D collider)
    {
        _myBoxColliders2D.Remove(collider);
    }
    #endregion



    #region OnEverySecond
    public void AddOnEverySecond(Action action) 
    {
        OnEverySecond += action;
    }

    public void InvokeOnEverySecond() 
    {
        OnEverySecond?.Invoke();
    }

    public void RemoveOnEverySecond(Action action)
    {
        OnEverySecond -= action;
    }
    #endregion 








}
