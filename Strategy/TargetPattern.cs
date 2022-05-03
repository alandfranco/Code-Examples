using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPattern : IPattern
{
    Transform obj;
    Transform target;
    float speed;

    public TargetPattern(Transform objTransform, float objSpeed, Transform objTarget)
    {
        obj = objTransform;
        target = objTarget;
        speed = objSpeed;
    }

    public void Pattern()
    {
        obj.LookAt(target);
        obj.position += (target.position - obj.position).normalized * speed * Time.deltaTime;
    }
}
