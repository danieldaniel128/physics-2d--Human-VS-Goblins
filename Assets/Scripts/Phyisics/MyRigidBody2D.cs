using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRigidBody2D : MonoBehaviour
{

    //public Vector2 Force;
    public Vector2 velocity;
    public Vector2 acceleration;


    public float gravity;

    public float x;
    public float y;

    
    // Start is called before the first frame update
    void Start()
    {
        acceleration.y = -gravity;
    }

    // Update is called once per frame
    private void Update()
    {
        //put in a function
        

        MoveObjects();
        UpdateVelocityValue();
        UpdateXYValues();
    }

    void MoveObjects()
    {
        MoveObjectOnYAxis();
        MoveObjectOnXAxis();
    }

    void MoveObjectOnYAxis()//if acceleration.y is 0 its motion velocity doesnt change. first low of newton.
    {
        transform.position += new Vector3 (0, velocity.y * Time.deltaTime +  ((acceleration.y) * Time.deltaTime * Time.deltaTime) /2);
    }
    void MoveObjectOnXAxis()//if acceleration.x is 0 its motion velocity doesnt change. first low of newton.
    {
        transform.position += new Vector3(velocity.x * Time.deltaTime + ((acceleration.x) * Time.deltaTime * Time.deltaTime) / 2, 0);
    }



    public void AddForce(Vector2 ForcePower)//not a real force, but like a kick. 
    {
        velocity += ForcePower;
    }

    private void UpdateVelocityValue()
    {
        velocity += acceleration * Time.deltaTime;
    }

    public void CalculateForcesAndAcceleration() 
    {
    
    }

    private void UpdateXYValues()
    {
        x=transform.position.x;
        y=transform.position.y;
    }


}
