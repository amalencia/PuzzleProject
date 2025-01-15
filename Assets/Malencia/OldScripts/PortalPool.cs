using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPool : MonoBehaviour
{
    [SerializeField] private bool _isPlaced;
    [Header("EllipseComponents")]
    [SerializeField] private int _ellipseSegments;
    [SerializeField] private float _xScale;
    [SerializeField] private float _yScale;
    private LineRenderer _lineRenderer;
    private Vector3[] _ellipsePoints = new Vector3[0];
    private Vector3[] _ellipsePointsForLine = new Vector3[0];

    [Header("Mesh Components")]
    [SerializeField] private Material thisPortalInnerColor;
    [SerializeField] private Material thisPortalOuterColor;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    [Header("Mesh Parts-Vert,tris,norm,uv")]
    [SerializeField] private Vector3[] portalVertices = new Vector3[0];
    [SerializeField] private int[] portalTris = new int[0];
    [SerializeField] private Vector3[] portalNorm = new Vector3[0];
    [SerializeField] private Vector2[] portalUVs = new Vector2[0];

    [Header("Test Renderer")]
    [SerializeField] private Camera _camera;
    [SerializeField] private PortalPool _bluePool;
    [SerializeField] private bool _portalReady;
    private bool _followCamera;
    [SerializeField] private PortalRenderer _portalCameraScript;
    [SerializeField] private PortalManager _portalManager;
    [SerializeField] private PortalRenderer _otherPortalCamera;

    [Header("PortalPool Revised Variables")]
    [SerializeField] private MeshCollider _outlineMesh;
    [SerializeField] private Mesh mesh;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = thisPortalInnerColor;
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        portalVertices = InitializePortalVertices();
        portalTris = InitializePortalTris();
        portalNorm = InitializePortalNorms();
        portalUVs = InitializePortalUVs();
        InitializePortal();
    }

    private void InitializePortal()
    {
        mesh = new Mesh();
        mesh.vertices = portalVertices;
        mesh.triangles = portalTris;
        mesh.normals = portalNorm;
        mesh.uv = portalUVs;
        meshFilter.mesh = mesh;
        _outlineMesh.sharedMesh = mesh;
        meshRenderer.enabled = false;
    }

    private Vector2[] InitializePortalUVs()
    {
        float[] vertXValues = new float[_ellipseSegments];
        float[] vertYValues = new float[_ellipseSegments];
        for (int i = 0; i < _ellipseSegments; i++)
        {
            vertXValues[i] = _ellipsePoints[i].x;
            vertYValues[i] = _ellipsePoints[i].y;
        }
        float lowestX = FindLowestValue(vertXValues);
        float lowestY = FindLowestValue(vertYValues);
        for (int i = 0; i < _ellipseSegments; i++)
        {
            vertXValues[i] += Mathf.Abs(lowestX);
            vertYValues[i] += Mathf.Abs(lowestY);
        }
        float highestX = FindHighestValue(vertXValues);
        float highestY = FindHighestValue(vertYValues);
        for (int i = 0; i < _ellipseSegments; i++)
        {
            vertXValues[i] /= highestX;
            vertYValues[i] /= highestY;
        }
        Vector2[] returnVector = new Vector2[_ellipseSegments];
        for (int i = 0; i < _ellipseSegments; i++)
        {
            returnVector[i] = new Vector2(vertXValues[i], vertYValues[i]);
        }
        return returnVector;
    }

    private Vector3[] InitializePortalNorms()
    {
        Vector3[] GetNorms = new Vector3[_ellipseSegments];
        for (int i = 0; i < _ellipseSegments; i++)
        {
            GetNorms[i] = new Vector3(0, 0, 1);
        }
        return GetNorms;
    }

    private int[] InitializePortalTris()
    {
        int numOfTris = _ellipseSegments - 2;
        int size = numOfTris * 3;
        int[] ints = new int[size];
        int trackerInt = 0;
        for (int i = 0; i < numOfTris; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                ints[trackerInt] = i + j;
                if (j == 0)
                {
                    ints[trackerInt] = 0;
                }
                trackerInt++;
            }
        }
        return ints;
    }

    private Vector3[] InitializePortalVertices()
    {
        Vector3[] tempVertices = new Vector3[_ellipseSegments];
        _ellipsePoints = new Vector3[_ellipseSegments];
        _ellipsePointsForLine = new Vector3[_ellipseSegments];
        for (int i = 0; i < _ellipseSegments; i++)
        {
            float _angle = ((float)i / (float)_ellipseSegments) * 360 * Mathf.Deg2Rad;
            float x = Mathf.Sin(_angle) * _xScale;
            float y = Mathf.Cos(_angle) * _yScale;
            tempVertices[i] = new Vector3(x, y, 0);
            _ellipsePoints[i] = Vector3.ProjectOnPlane(tempVertices[i], new Vector3(0, 0, 1));
        }
        return _ellipsePoints;
    }

    public void PortalLocation(Vector3 location, Vector3 normal)
    {
        transform.position = location + 0.01f * normal;
        SetRotation(normal);
        _camera.transform.forward = normal;
        SetOuterLine();
        meshRenderer.enabled = true;
        _portalReady = true;
        _portalManager.CheckIfReadyForCamera();

    }

    private void SetRotation(Vector3 normal)
    {
        float testIfPerpendicular = Mathf.Abs(Vector3.Dot(new Vector3(0, 0, 1), normal));
        for (int i = 0; i < _ellipseSegments; i++)
        {
            if (testIfPerpendicular <= 0.01f)
            {
                if (Mathf.Abs(normal.y) < 0.01f)
                {
                    _ellipsePointsForLine[i] = new Vector3(0, _ellipsePoints[i].y, _ellipsePoints[i].x);
                }
                else
                {
                    _ellipsePointsForLine[i] = new Vector3(_ellipsePoints[i].x, 0, _ellipsePoints[i].y);
                }
            }
            else
            {
                _ellipsePointsForLine[i] = _ellipsePoints[i];
            }
        }
        Vector3 upVector = _ellipsePointsForLine[0];
        Quaternion rotation = Quaternion.LookRotation(-normal, upVector);
        transform.rotation = rotation;
    }

    private void SetOuterLine()
    {
        _lineRenderer.startWidth = 0.1f;
        _lineRenderer.endWidth = 0.1f;
        _lineRenderer.material = thisPortalOuterColor;
        _lineRenderer.positionCount = _ellipseSegments + 1;
        Vector3[] lineVector = new Vector3[_ellipseSegments + 1];
        for (int i = 0; i < _ellipseSegments; i++)
        {
            lineVector[i] = transform.TransformPoint(_ellipsePoints[i]);
        }
        lineVector[_ellipseSegments] = transform.TransformPoint(_ellipsePoints[0]);
        _lineRenderer.SetPositions(lineVector);
    }
    public bool IsThisPortalReady()
    {
        return _portalReady;
    }

    private float FindHighestValue(float[] array)
    {
        float defaultHigh = array[0];
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] > defaultHigh)
            {
                defaultHigh = array[i];
            }
        }
        return defaultHigh;
    }
    private float FindLowestValue(float[] array)
    {
        float defaultLow = array[0];

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] < defaultLow)
            {
                defaultLow = array[i];
            }
        }
        return defaultLow;
    }
    public bool IsPlaced()
    {
        return _isPlaced;
    }
    public void SetMeshMaterial(Material material)
    {
        meshRenderer.material = material;
    }
}
