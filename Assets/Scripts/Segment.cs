using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment
{
    Vector3 dir;
    Vector3 normalBasis1, normalBasis2;
    Vector3[] vertices;

    public Vector3 GetDir => dir;
    public Vector3 GetNormalBasis => normalBasis1;

    public Segment(int vertexN)
    {
        vertices = new Vector3[vertexN];
    }

    public void UpdateDir(Vector3 dir, Vector3 previousDir)
    {
        float t1 = Vector3.Dot(dir, Vector3.up);
        float t2 = Vector3.Dot(dir, Vector3.forward);
        Vector3 basis = Mathf.Abs(t1) > Mathf.Abs(t2) ? Vector3.forward : Vector3.up;
        UpdateDir(dir, previousDir, basis);
    }

    public void UpdateDir(Vector3 dir, Vector3 previousDir, Vector3 previousNormalBasis)
    {
        this.dir = dir;

        Vector3 interpolateDir = (dir + previousDir) / 2;
        if(interpolateDir != Vector3.zero)
        {
            normalBasis1 = (previousNormalBasis - Vector3.Dot(previousNormalBasis, interpolateDir) * interpolateDir).normalized;
            normalBasis2 = Vector3.Cross(interpolateDir, normalBasis1);
        }
        else
        {
            normalBasis1 = previousNormalBasis;
            normalBasis2 = Vector3.Cross(dir, normalBasis1);
        }

    }

    public Vector3[] GetSegmentVertices(Vector3 pos, float radius, float skew = 0)
    {
        int n = vertices.Length;
        for (int i = 0; i < n; i++)
        {
            vertices[i] = pos + (normalBasis1 * Mathf.Cos(Mathf.PI * 2 / n * i + skew) + normalBasis2 * Mathf.Sin(Mathf.PI * 2 / n * i + skew)) * radius;
        }

        return vertices;
    }

}
