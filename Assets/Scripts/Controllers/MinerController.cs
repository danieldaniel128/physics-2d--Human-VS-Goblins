using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MinerController : MonoBehaviour
{
    [SerializeField] MyBoxCollider2D minerCollider;

    private void Start()
    {
        minerCollider.OnCollisionEnter += OnMyCollisionEnter2D;
        //minerCollider.OnCollisionExit += OnMyCollisionExit2D;
    }
    //void OnMyCollisionExit2D(MyBoxCollider2D collider)
    //{

    //}
    void OnMyCollisionEnter2D(MyBoxCollider2D collider)
    {
        collider.SetOnCollisionWith(CanCollideWithCollider);
    }
    bool CanCollideWithCollider(MyBoxCollider2D collider)//true is cant, false is not
    {
        if (collider.tag.Contains("Miner"))
            return false;
        return true;
    }
}
