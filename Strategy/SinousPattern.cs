using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinousPattern : IPattern
{
    Transform obj;
    float mod;
    float speed;

    public SinousPattern(Transform objTransform, float objSpeed, float sinModifier)
    {
        obj = objTransform;
        mod = sinModifier;
        speed = objSpeed;
    }

    public void Pattern()
    {
        obj.Translate(new Vector3(Mathf.Sin(Time.time * mod) / 10, 0, 0) + (Vector3.forward * speed * Time.deltaTime));
    }
}
