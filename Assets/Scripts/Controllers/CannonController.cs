using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] Vector2 CannonDirection;
    [SerializeField] GameObject AmmoPrefub;
    [SerializeField] float cannonChargeForce;
    [SerializeField] float maxChargeForce;
    [SerializeField] float chargingSpeed;//power per second


    void Update()
    {
        RotateCannon();
        ShootBall();
    }

    void RotateCannon() 
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        float mousePosXDir = mouseWorldPosition.x - transform.position.x;
        float mousePosYDir = mouseWorldPosition.y - transform.position.y;
        float newAngleOfCannon = Mathf.Atan2(mousePosYDir, mousePosXDir) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, newAngleOfCannon);
        CannonDirection = new Vector2(mousePosXDir, mousePosYDir);
    }

    

    void ShootBall()
    {

        if (Input.GetMouseButtonUp(0)) 
        {
            //GameObject ball = Instantiate(AmmoPrefub,transform.position,Quaternion.identity);//parent later
            MyRigidBody2D AmmoPrefubRigidBody = AmmoPrefub.GetComponent<MyRigidBody2D>();
            AmmoPrefubRigidBody.velocity = Vector2.zero;
            AmmoPrefub.transform.position = transform.position;
            AmmoPrefub.SetActive(true);
            AmmoPrefubRigidBody.AddForce(CannonDirection * cannonChargeForce);
            cannonChargeForce = 0;
        }
        if (Input.GetMouseButton(0))
        {
            if (cannonChargeForce < maxChargeForce)
                cannonChargeForce += Time.deltaTime * chargingSpeed;
            else
                cannonChargeForce = maxChargeForce;
        }
    }

}
