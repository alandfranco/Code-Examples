using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPattern : IPattern
{
    Transform obj;
    float speed;

    public NormalPattern(Transform objTransform, float objSpeed)
    {
        obj = objTransform;
        speed = objSpeed;
    }

    public void Pattern()
    {
        obj.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
