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
        GamePhysicsManagement();
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

    public void GamePhysicsManagement()
    {
        CollidersAreTouching();
    }

    public void PrintColliders()//for editor scripts
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
            foreach (var c1 in _myBoxColliders2D.ToList())
            {
                foreach (var c2 in _myBoxColliders2D.ToList())
                {
                    if (c1 == c2)
                        continue;

                    if (c1.CheckCollision(c2))
                    {
                        // Determine the direction of the collision

                        c1.InvokeOnCollisionEnter(c2);//before collision effect events
                        c2.InvokeOnCollisionEnter(c1);

                        if (!c1.staticObject && c2.staticObject)
                        {
                            CollisionImpactStaticObject(c1, c2); // One moving, one static
                        }
                        else if (!c1.staticObject && !c2.staticObject)
                            if ((c1.OnCollisionWith != null && c1.OnCollisionWith(c2)) || (c2.OnCollisionWith != null && c2.OnCollisionWith(c1)))
                                {
                                    CollisionImpact(c1, c2); // Both moving
                                    CollisionImpact(c2, c1); // Both moving
                                }
                            else if(c1.OnCollisionWith == null && c2.OnCollisionWith == null) 
                            {
                                CollisionImpact(c1, c2); // Both moving
                                CollisionImpact(c2, c1); // Both moving
                            }
                            //else
                               //Debug.Log("collided with itself, goblin or miner");do something here, or in the on collisionwith method;
                        c1.InvokeOnCollisionExit(c2);//after collision effect events
                        c2.InvokeOnCollisionExit(c1);
                        c1.isColliding = true;
                        c2.isColliding = true;
                        c1.CollidedTimer += (1 / 50f);
                        c2.CollidedTimer += (1 / 50f);
                    }
                    else
                    {
                        c1.isColliding = false;
                        c2.isColliding = false;
                    }
                }
            }
        }
    }


    void CollisionImpactStaticObject(MyBoxCollider2D c1, MyBoxCollider2D c2)
    {
        MyRigidBody2D r1 = c1.GetComponent<MyRigidBody2D>();

        // Calculate the mass and velocities of the objects
        Vector2 r1Velocity = r1.velocity;
        Vector2 r2Velocity = Vector2.zero; // Floor has no velocity
        // Calculate the collision impact assuming a restitution value of 1 and an upward collision normal
        Vector2 collisionNormal = Vector2.up;
        if (c2.transform.position.y + c2.HeightUpAndOffset / 2 >= c1.transform.position.y)//somthing doesnt work here //fixed :)
            if(c2.transform.position.x >= c1.transform.position.x)    
                collisionNormal = Vector2.right;
            else
                collisionNormal = Vector2.left;
        Vector2 relativeVelocity = r1Velocity - r2Velocity;
        float impulseMagnitude = (-(1 + restitution) * Vector2.Dot(relativeVelocity, collisionNormal) / ((1 / c1.Mass) + (1 / c2.Mass)));
        Vector2 impulse = impulseMagnitude * collisionNormal;

        // Update the object's velocity
        r1.velocity += impulse / c1.Mass;
        if(collisionNormal.y!=0) //colliding from up or down
            r1.transform.position =  new Vector3(r1.transform.position.x,c2.transform.position.y + c2.HeightUpAndOffset / 2 + c1.HeightUpAndOffset / 2);
        else
            if(c2.transform.position.x >= c1.transform.position.x)
                r1.transform.position = new Vector3(c2.transform.position.x - c2.WidthRightAndOffset / 2 - c1.WidthRightAndOffset / 2, c1.transform.position.y);
            else
                r1.transform.position = new Vector3(c2.transform.position.x + c2.WidthRightAndOffset / 2 +c1.WidthRightAndOffset / 2, c1.transform.position.y);

    }

    void CollisionImpact(MyBoxCollider2D c1, MyBoxCollider2D c2)
    {
        MyRigidBody2D r1 = c1.GetComponent<MyRigidBody2D>();
        MyRigidBody2D r2 = c2.GetComponent<MyRigidBody2D>();
        // Calculate the velocities of the objects
        Vector2 r1Velocity = r1.velocity;
        Vector2 r2Velocity = r2.velocity;
        // Calculate the collision impact assuming a restitution value of 1 and an upward collision normal
        Vector2 collisionNormal;
        if (c2.transform.position.x >= c1.transform.position.x)
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
        // Update the objects' velocities
        r1.velocity -= impulse / c1.Mass;
        r2.velocity += impulse / c2.Mass;
        if (c2.transform.position.x >= c1.transform.position.x)//depends on the side, teleport the object so it will not collide multiple times in split seconds
        {
            r1.transform.position = new Vector3(c2.transform.position.x - c2.Width / 2 - c1.Width / 2 - 0.01f, c1.transform.position.y);//so they will not enter each other more than once in a split seconds
            r2.transform.position = new Vector3(c1.transform.position.x + c1.Width / 2 + c2.Width / 2 + 0.01f, c2.transform.position.y);
        }
        else
        {
            r1.transform.position = new Vector3(c2.transform.position.x + c2.Width / 2 + c1.Width / 2 + 0.01f, c1.transform.position.y);//so they will not enter each other more than once in a split seconds
            r2.transform.position = new Vector3(c1.transform.position.x - c1.Width / 2 - c2.Width / 2 - 0.01f, c2.transform.position.y);
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
