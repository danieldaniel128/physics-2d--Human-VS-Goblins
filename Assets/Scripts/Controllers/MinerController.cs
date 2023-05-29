using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MinerController : MonoBehaviour
{
    [SerializeField] MyBoxCollider2D minerCollider;
    [SerializeField] MyRigidBody2D minerRigidBody;
    
    [SerializeField] float _minerSpeed;
    public float MinerSpeed { get { return _minerSpeed; } private set { _minerSpeed = value; } }

    private void Start()
    {
        minerCollider.OnCollisionEnter += OnMyCollisionEnter2D;
        minerCollider.OnCollisionExit += OnMyCollisionExit2D;
    }


    private void FixedUpdate()
    {
        MoveTowardsCastleAndMine();
    }

        void OnMyCollisionExit2D(MyBoxCollider2D collider)
        {
            if (collider.tag.Equals("Castle"))
            {
                MinerSpeed *= -1;
                transform.position += Vector3.left * 0.05f;
                transform.Rotate(0, 180, 0);
        }
            else if (collider.tag.Equals("Mine"))
            {
                MinerSpeed *= -1;
                transform.position += Vector3.right * 0.05f;
                transform.Rotate(0,180, 0);
            }

        //enemyCollider.OnCollisionEnter
    }

    void MoveTowardsCastleAndMine()
    {
        minerRigidBody.velocity = new Vector2(_minerSpeed, minerRigidBody.velocity.y); //speed directed to castle, or mone carring the resources
    }
    void OnMyCollisionEnter2D(MyBoxCollider2D collider)
    {
        minerCollider.SetOnCollisionWith(CanCollideWithCollider);
        
    }
    bool CanCollideWithCollider(MyBoxCollider2D collider)//true is cant, false is not
    {
        if (minerCollider.tag.Contains("Miner") && collider.tag.Contains("Miner"))
            return false;
        return true;
    }
}
