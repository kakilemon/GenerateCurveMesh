using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helix : CurveFuncBase
{
    public int alpha;
    public int beta;
    public float r_outer;
    public float r_inner;

    public override Vector3 CureveFunc(float t, float time)
    {
        float theta = t * Mathf.PI * 2;
        float a = (r_outer + r_inner) / 2;
        float b = (r_outer - r_inner) / 2;

        return new Vector3(
           (a + b * Mathf.Cos(alpha * theta + time)) * Mathf.Cos(beta * theta),
           b * Mathf.Sin(alpha * theta + time),
           (a + b * Mathf.Cos(alpha * theta + time)) * Mathf.Sin(beta * theta)
           );
    }
}
