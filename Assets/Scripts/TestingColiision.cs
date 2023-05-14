using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingColiision : MonoBehaviour
{
    [SerializeField] MyRigidBody2D rigidbodyTesting;
    [SerializeField] float pushForce;


    [ContextMenu("push to left")]
    void PushToLeft()
    {
        rigidbodyTesting.AddForce(Vector2.left* pushForce);
    }

    [ContextMenu("push to right")]
    void PushToRight()
    {
        rigidbodyTesting.AddForce(Vector2.right * pushForce);
    }

}
