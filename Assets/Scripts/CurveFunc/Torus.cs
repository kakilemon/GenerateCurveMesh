using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torus : CurveFuncBase
{
    public float radius;
    public override Vector3 CureveFunc(float t, float time)
    {
        float theta = t * Mathf.PI * 2;

        return new Vector3(
            Mathf.Cos(theta),
            Mathf.Sin(theta),
            0
            ) * radius;
    }
}
