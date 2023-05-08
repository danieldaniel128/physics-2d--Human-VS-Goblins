using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        float mousePosXDir = mouseWorldPosition.x - transform.position.x;
        float mousePosYDir = mouseWorldPosition.y - transform.position.y;
        float newAngleOfCannon = Mathf.Atan2(mousePosYDir, mousePosXDir)* Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0, newAngleOfCannon);

    }



    

}
