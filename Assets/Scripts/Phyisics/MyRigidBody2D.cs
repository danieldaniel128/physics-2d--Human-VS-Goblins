using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRigidBody2D : MonoBehaviour
{

    public Vector2 acceleration;
    //public Vector2 Force;
    public Vector2 velocity;
    public float gravity;

    public float x;
    public float y;
    [SerializeField] bool activeGravity;
    [SerializeField] bool moveRight;

    [SerializeField] float moveRightForceTest;
    [SerializeField] float TimeMove;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(activeGravity)
            GravityOfObject();
        if (moveRight)
            AddForceXAxisRight();
        UpdateXYValues();
    }

    void GravityOfObject() 
    {
        transform.position += new Vector3 (0, velocity.y * Time.deltaTime + - ((gravity) * Time.deltaTime * Time.deltaTime) /2);
        UpdateVelocity();
    }
    [ContextMenu("Force To Right and Active Gravity")]
    void AddForceXAxisRight() 
    {
        activeGravity=true;
        moveRight=true;
        transform.position += new Vector3((moveRightForceTest) * Time.deltaTime, 0);
    }

    private void UpdateVelocity()
    {
        if (activeGravity)
            velocity.y -= gravity* Time.deltaTime;
    }

    private void UpdateXYValues()
    {
        x=transform.position.x;
        y=transform.position.y;
    }


}
