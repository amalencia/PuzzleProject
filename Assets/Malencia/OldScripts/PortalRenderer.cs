using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using RenderPipeline = UnityEngine.Rendering.RenderPipelineManager;

public class PortalRenderer : MonoBehaviour
{
    [SerializeField] private Transform orangePortal;
    [SerializeField] private Transform bluePortal;

    [SerializeField] private Camera _camera;
    [SerializeField] private Camera _mainCamera;

    private RenderTexture _orangePortalTexture;
    private RenderTexture _bluePortalTexture;
    [SerializeField] private Material _orangeMaterial;
    [SerializeField] private Material _blueMaterial;

    [SerializeField] private PortalManager _portalManager;
    [SerializeField] private PortalPool _bluePortalPool;
    [SerializeField] private PortalPool _orangePortalPool;

    private void Awake()
    {
        _mainCamera = GetComponent<Camera>();
        _orangePortalTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
        _bluePortalTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat .ARGB32);
    }

    private void Start()
    {
        _orangeMaterial.mainTexture = _orangePortalTexture;
        _blueMaterial.mainTexture = _bluePortalTexture;
    }

    public Material GetOrangePortalRenderTexture()
    {
        return _orangeMaterial;
    }

    public Material GetBluePortalRenderTexture() { return _blueMaterial; }

    private void OnEnable()
    {
        //RenderPipeline.beginCameraRendering += UpdateCamera;
    }

    private void OnDisable()
    {
        //RenderPipeline.beginCameraRendering -= UpdateCamera;
    }

    private void UpdateCamera(ScriptableRenderContext context, Camera camera)
    {
        if(_portalManager.CheckIfReadyForCamera() == true)
        {

            _bluePortalPool.SetMeshMaterial(_blueMaterial);
            if(_bluePortalPool.IsPlaced())
            {
                _camera.targetTexture = _orangePortalTexture;
                for(int i = 5; i >= 0; i--)
                {
                    CameraRendering(bluePortal, orangePortal, i, context);
                }
            }
            _orangePortalPool.SetMeshMaterial(_orangeMaterial);
            if(_orangePortalPool.IsPlaced())
            {
                _camera.targetTexture = _bluePortalTexture;
                for(int i = 5; i>= 0; i--)
                {
                    CameraRendering(orangePortal, bluePortal, i, context);
                }
            }
        }
        else
        {
            return;
        }

    }

    private void CameraRendering(Transform thisPortal, Transform otherPortal, int iterations, ScriptableRenderContext context)
    {
        Transform _portalCameraTransform = _camera.transform;
        _portalCameraTransform.position = transform.position;
        _portalCameraTransform.rotation = transform.rotation;

        for(int i = 0; i <= iterations; i++)
        {
            Vector3 relativePosition = thisPortal.InverseTransformPoint(_portalCameraTransform.position);
            relativePosition = Quaternion.Euler(0, 180, 0) * relativePosition;
            _portalCameraTransform.position = otherPortal.TransformPoint(relativePosition);

            Quaternion relativeRotation = Quaternion.Inverse(thisPortal.rotation) * _portalCameraTransform.rotation;
            relativeRotation = Quaternion.Euler(0, 180, 0) * relativeRotation;
            _portalCameraTransform.rotation = otherPortal.rotation * relativeRotation;
        }
        Plane _portalCameraDisplayPlane = new Plane(otherPortal.forward, otherPortal.position);
        Vector4 clipPlaneinWorld = new Vector4(_portalCameraDisplayPlane.normal.x, _portalCameraDisplayPlane.normal.y, _portalCameraDisplayPlane.normal.z, _portalCameraDisplayPlane.distance);
        Vector4 clipPlaneforCamera = Matrix4x4.Transpose(Matrix4x4.Inverse(_camera.worldToCameraMatrix)) * clipPlaneinWorld;
        Matrix4x4 newProjectionMatrix = _mainCamera.CalculateObliqueMatrix(clipPlaneforCamera);
        _camera.projectionMatrix = newProjectionMatrix;

        UniversalRenderPipeline.RenderSingleCamera(context, _camera);
    }
}
