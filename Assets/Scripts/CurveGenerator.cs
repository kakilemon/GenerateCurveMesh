using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CurveGenerator : MonoBehaviour
{
    public float radius;
    public int segN;
    public int division;
    public CurveFuncBase curveFunc;
    public bool drawGizmo;

    Segment[] segments;
    Vector3[] segPos;
    Vector3[] segDir;
    Vector3[] vertices;

    Mesh mesh;

    private void Start()
    {
        segments = new Segment[segN];
        for (int i = 0; i < segN; i++)
        {
            segments[i] = new Segment(division);
        }

        segPos = new Vector3[segN];
        segDir = new Vector3[segN];
        vertices = new Vector3[segN * division];

        SetSegPos();
        UpdateSegments();
        SetVertPos();
        CreateMesh();
    }

    private void Update()
    {
        SetSegPos();
        UpdateSegments();
        SetVertPos();
        UpdateMesh();
    }

    void SetSegPos()
    {
        for (int i = 0; i < segN; i++)
        {
            segPos[i] = curveFunc.CureveFunc((float)i / segN, Time.timeSinceLevelLoad);
            if(i > 0)
            {
                segDir[i - 1] = (segPos[i] - segPos[i - 1]).normalized;
            }
        }
        segDir[segN - 1] = (segPos[0] - segPos[segN - 1]).normalized;
    }

    void UpdateSegments()
    {
        segments[0].UpdateDir(segDir[0], segDir[segN - 1]);
        for (int i = 1; i < segN; i++)
        {
            segments[i].UpdateDir(segDir[i], segDir[i - 1], segments[i - 1].GetNormalBasis);
        }
    }

    void SetVertPos()
    {
        float skew = GetSkew();

        for (int i = 0; i < segN; i++)
        {
            var circle = segments[i].GetSegmentVertices(segPos[i], radius , skew / segN * i);

            for (int j = 0; j < division; j++)
            {
                vertices[i * division + j] = circle[j];
            }
        }
    }

    float GetSkew()
    {
        Vector3 dir = segments[segN - 1].GetDir;
        Vector3 n1 = segments[0].GetNormalBasis;
        Vector3 n2 = segments[segN - 1].GetNormalBasis;
        Vector3 startSkew = n1 - Vector3.Dot(n1, dir) * dir;
        Vector3 endSkew = n2 - Vector3.Dot(n2, dir) * dir;
        float sign = Mathf.Sign(Vector3.Dot(dir, Vector3.Cross(endSkew, startSkew)));
        float angle = Vector3.Angle(startSkew, endSkew);
        return sign * angle * Mathf.Deg2Rad;
    }

    void CreateMesh()
    {
        mesh = new Mesh();

        //Vector2[] uv = new Vector2[segN * division];
        int[] triangles = new int[segN * division * 2 * 3];
        int triIdx = 0;

        for(int i = 0; i < segN; i++)
        {
            for(int j = 0; j < division; j++)
            {
                triangles[triIdx++] = i * division + j;
                triangles[triIdx++] = i * division + (j + 1) % division;
                triangles[triIdx++] = ((i + 1) % segN) * division + j;

                triangles[triIdx++] = i * division + (j + 1) % division;
                triangles[triIdx++] = ((i + 1) % segN) * division + (j + 1) % division;
                triangles[triIdx++] = ((i + 1) % segN) * division + j;
            }
        }

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        //mesh.SetUVs(0, uv);

        mesh.RecalculateNormals();

        MeshFilter filter = GetComponent<MeshFilter>();
        filter.mesh = mesh;
    }

    void UpdateMesh()
    {
        mesh.SetVertices(vertices);
        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (drawGizmo && Application.isPlaying && vertices != null)
        {
            for (int i = 0; i < segN; i++)
            {
                for (int j = 0; j < division; j++)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(transform.position + vertices[i * division + j], transform.position + vertices[i * division + (j + 1) % division]);

                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(transform.position + vertices[i * division + j], transform.position + vertices[((i + 1) % segN) * division + j]);
                }
            }
        }
    }
}
