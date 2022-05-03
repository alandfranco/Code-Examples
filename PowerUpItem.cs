using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpItem : MonoBehaviour {

    void Update()
    {
        transform.localEulerAngles += Vector3.up * Time.deltaTime * FlyWeightPointer.PowerUp.speed;
    }
}
