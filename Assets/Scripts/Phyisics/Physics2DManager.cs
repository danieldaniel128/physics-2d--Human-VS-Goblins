using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Physics2DManager : MonoBehaviour
{
    public static Physics2DManager Instance;
    //after, i will do a collider script that different kinds of colliders will inherit from and the array will be of collider
    public List<MyBoxCollider2D> _myBoxColliders2D;//collider get added with the events.
    public List<MyRigidBody2D> _myRigidbody2Ds;
    
    //public event Action<MyBoxCollider2D> OnCollisionEnter;

    public event Action OnEverySecond;


    public event Action<MyBoxCollider2D> ColliderWasAdded;
    public event Action<MyBoxCollider2D> ColliderWasRemoved;

    /*[SerializeField]*/
    float _timer;
    [SerializeField] float _timerInSeconds;
    [SerializeField] float restitution = 0.5f;
    [SerializeField] float timeToExitCollision;

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
    private void FixedUpdate()
    {
        DoManagering();
        GameTimer();
    }
    void GameTimer() 
    {
        _timer += (1/50f);
        if (_timer >= 1)
        {
            _timer = 0;
            _timerInSeconds++;
            //InvokeOnEverySecond();
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
        if (_myBoxColliders2D.Count >= 2)
        {
            foreach (var c1 in _myBoxColliders2D)
            {
                foreach (var c2 in _myBoxColliders2D)
                {
                    if (c1 == c2)
                        continue;

                    if (c1.CheckCollision(c2))
                    {
                        // Determine the direction of the collision
                        Vector2 c1PreviousPosition = c1.transform.position - new Vector3((c1.GetComponent<MyRigidBody2D>().velocity * (1 / 50f)).x, (c1.GetComponent<MyRigidBody2D>().velocity * (1 / 50f)).y);
                        Vector2 c2PreviousPosition = c2.transform.position - new Vector3((c2.GetComponent<MyRigidBody2D>().velocity * (1 / 50f)).x, (c2.GetComponent<MyRigidBody2D>().velocity * (1 / 50f)).y);

                        bool collisionFromLeftToRight = c1PreviousPosition.x < c2PreviousPosition.x;
                        //bool collisionFromRightToLeft = c1PreviousPosition.x > c2PreviousPosition.x;
                        if (c1.staticObject && !c2.staticObject)
                        {
                            CollisionImpactStaticObject(c2, c1); // One moving, one static
                            if(!c2.FirstCollisionEnter)
                            c2.InvokeOnCollision(c1);
                        }
                        //else if (!c1.staticObject && !c2.staticObject)
                        //    // Handle the collision based on the direction
                        //    if (collisionFromLeftToRight)
                        //    {
                        //        CollisionImpact(c1, c2, collisionFromLeftToRight); // Both moving
                        //    }
                        //    else
                        //    {
                        //            CollisionImpact(c2, c1, collisionFromLeftToRight); // Both moving
                        //    }

                        c1.isColliding = true;
                        c2.isColliding = true;
                        c2.CollidedTimer += (1/50f);
                    }
                    else
                    {
                        if(c2.CollidedTimer > timeToExitCollision && c2.GetComponent<MyRigidBody2D>().velocity.y <= -0.3f && c2.GetComponent<MyRigidBody2D>().velocity.y<=0)
                        {
                            c2.CollidedTimer = 0;
                            c2.FirstCollisionEnter = false;
                        }//c1.FirstCollisionEnter = false;
                        c1.isColliding = false;
                        c2.isColliding = false;
                    }
                }
            }
        }
    }

    //for (int i = _myBoxColliders2D.Count-1; i > 0; i--)
    //{
    //    for (int j = _myBoxColliders2D.Count - 1; j > 0; j--)  //j=i+1 is optimizing run time
    //    {
    //            if (_myBoxColliders2D[i] == _myBoxColliders2D[j])
    //                continue;
    //        if (_myBoxColliders2D[i].CheckCollision(_myBoxColliders2D[j]))
    //        {
    //                //if(_myBoxColliders2D[i].FirstCollisionEnter)
    //                //OnCollisionEnter.Invoke()
    //            _myBoxColliders2D[i].isColliding = true;
    //            _myBoxColliders2D[j].isColliding = true;
    //            if (!_myBoxColliders2D[i].staticObject && _myBoxColliders2D[j].staticObject)
    //            {
    //                CollisionImpactStaticObject(_myBoxColliders2D[i], _myBoxColliders2D[j]);//one moving, one static
    //            }
    //            else
    //                CollisionImpact(_myBoxColliders2D[i], _myBoxColliders2D[j]);
    //            //else collisionImpact between 2 moving objects
    //        }
    //        else
    //        {
    //            _myBoxColliders2D[i].isColliding = false;
    //            _myBoxColliders2D[j].isColliding = false;
    //        }
    //    }
    //}



    void CollisionImpactStaticObject(MyBoxCollider2D c1, MyBoxCollider2D c2)
    {
        MyRigidBody2D r1 = c1.GetComponent<MyRigidBody2D>();

        // Calculate the mass and velocities of the objects
        Vector2 r1Velocity = r1.velocity;
        Vector2 r2Velocity = Vector2.zero; // Floor has no velocity
        // Calculate the collision impact assuming a restitution value of 1 and an upward collision normal
        Vector2 collisionNormal = Vector2.up;
        if (c2.transform.position.y + c2.Height/2 >= c1.transform.position.y)
                    collisionNormal = Vector2.right;
        Vector2 relativeVelocity = r1Velocity - r2Velocity;
        float impulseMagnitude = (-(1 + restitution) * Vector2.Dot(relativeVelocity, collisionNormal) / ((1 / c1.Mass) + (1 / c2.Mass)));
        Vector2 impulse = impulseMagnitude * collisionNormal;

        // Update the object's velocity
        r1.velocity += impulse / c1.Mass;
        if(collisionNormal.y!=0)
            r1.transform.position =  new Vector3(r1.transform.position.x,c2.transform.position.y + c2.Height / 2 + c1.Height/ 2);
        else
            r1.transform.position = new Vector3(c2.transform.position.x + c2.Width/2 +c1.Width/2, c1.transform.position.y);
    }

    void CollisionImpact(MyBoxCollider2D c1, MyBoxCollider2D c2,bool collisionFromLeftToRight)
    {
        MyRigidBody2D r1 = c1.GetComponent<MyRigidBody2D>();
        MyRigidBody2D r2 = c2.GetComponent<MyRigidBody2D>();

        // Calculate the velocities of the objects
        Vector2 r1Velocity = r1.velocity;
        Vector2 r2Velocity = r2.velocity;

        // Calculate the collision impact assuming a restitution value of 1 and an upward collision normal
        Vector2 collisionNormal;
        if (collisionFromLeftToRight)
            collisionNormal = Vector2.right;
        else
            collisionNormal = Vector2.left;
        Vector2 relativeVelocity = r1Velocity - r2Velocity;
        float impulseMagnitude = (-(1 + restitution) * Vector2.Dot(relativeVelocity, collisionNormal) / ((1 / c1.Mass) + (1 / c2.Mass)));

        // Check the relative directions of the objects
        bool r1MovingLeft = r1Velocity.x < 0f;
        bool r2MovingLeft = r2Velocity.x < 0f;

        // Adjust the impulse direction based on the relative directions
        if ((r1MovingLeft && !r2MovingLeft) || (!r1MovingLeft && r2MovingLeft))
        {
            impulseMagnitude *= -1f;
        }

        Vector2 impulse = impulseMagnitude * collisionNormal;
        Debug.Log($"{impulse}");
        // Update the objects' velocities
        r1.velocity += impulse / c1.Mass;
        r2.velocity -= impulse / c2.Mass;
        Debug.Log($"r1: {r1.velocity}, added {impulse / c1.Mass}. r2: {r2.velocity},added {impulse / c1.Mass}");
        //r1.transform.position = new Vector3(r2.transform.position.x - 1.2f*c2.Width, r1.transform.position.y);
        //r2.transform.position = new Vector3(r1.transform.position.x + 1.2f*c1.Width, r2.transform.position.y);
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
