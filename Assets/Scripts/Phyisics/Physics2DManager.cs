using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    [SerializeField] float restitution = 0.5f;

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
            for (int j = i+1; j < _myBoxColliders2D.Count; j++)  //j=i+1 is optimizing run time
            {
                //if (_myBoxColliders2D[i] == _myBoxColliders2D[j])
                //    continue;
                if (_myBoxColliders2D[i].CheckCollision(_myBoxColliders2D[j]))
                {
                    _myBoxColliders2D[i].isColliding = true;
                    _myBoxColliders2D[j].isColliding = true;
                    if(!_myBoxColliders2D[i].staticObject)
                    CollisionImpact(_myBoxColliders2D[i], _myBoxColliders2D[j]);//one moving, one static
                    //else collisionImpact between 2 moving objects
                }
                else
                {
                    _myBoxColliders2D[i].isColliding = false;
                    _myBoxColliders2D[j].isColliding = false;
                }
            }
        }
    }

    void CollisionImpact(MyBoxCollider2D c1, MyBoxCollider2D c2)
    {
        MyRigidBody2D r1 = c1.GetComponent<MyRigidBody2D>();

        // Calculate the mass and velocities of the objects
        Vector2 r1Velocity = r1.velocity;
        Vector2 r2Velocity = Vector2.zero; // Floor has no velocity
        // Calculate the collision impact assuming a restitution value of 1 and an upward collision normal
        Vector2 collisionNormal = Vector2.up;
        Vector2 relativeVelocity = r1Velocity - r2Velocity;
        float impulseMagnitude = (-(1 + restitution) * Vector2.Dot(relativeVelocity, collisionNormal) / ((1 / c1.Mass) + (1 / c2.Mass)));
        Vector2 impulse = impulseMagnitude * collisionNormal;

        // Update the object's velocity
        r1.velocity += impulse / c1.Mass;
        r1.transform.position =  new Vector3(r1.transform.position.x,c2.transform.position.y + c2.Height);
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
