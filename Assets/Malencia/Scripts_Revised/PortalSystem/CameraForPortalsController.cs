
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using RenderPipeline = UnityEngine.Rendering.RenderPipelineManager;

public class CameraForPortalsController : MonoBehaviour
{
    [Header("Camera Stuff")]
    [SerializeField] private Camera _portalCamera;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private int _portalCameraRenderIterations;
    private RenderTexture bluePortalTexture;
    private RenderTexture pinkPortalTexture;

    [Header("Portal Stuff")]
    [SerializeField] private Portal bluePortal;
    [SerializeField] private Portal pinkPortal;
    [SerializeField] private PortalGameManager _portalManager;

    private void Awake()
    {
        bluePortalTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
        pinkPortalTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
    }

    private void Start()
    {
        bluePortal.Renderer.material.mainTexture = bluePortalTexture;
        pinkPortal.Renderer.material.mainTexture =pinkPortalTexture;
    }

    private void OnEnable()
    {

        RenderPipeline.beginCameraRendering += UpdateCamera;
    }

    private void OnDisable()
    {
        RenderPipeline.beginCameraRendering -= UpdateCamera;
    }

    void UpdateCamera(ScriptableRenderContext SRC, Camera camera)
    {
        if(_portalManager.PortalsReadyForRendering())
        {
            if(bluePortal.IsPlaced)
            {
                _portalCamera.targetTexture = bluePortalTexture;
                for(int i = _portalCameraRenderIterations; i >= 0; i--)
                {
                    RenderCamera(bluePortal, pinkPortal, i, SRC);
                }
            }

            if(pinkPortal.IsPlaced)
            {
                _portalCamera.targetTexture = pinkPortalTexture;
                for(int i = _portalCameraRenderIterations;i >= 0;i--)
                {
                    RenderCamera(pinkPortal, bluePortal, i, SRC);
                }
            }
        }
    }

    private void RenderCamera(Portal thisPortal, Portal otherPortal, int iterator, ScriptableRenderContext SRC)
    {
        Transform inTransform = thisPortal.transform;
        Transform outTransform = otherPortal.transform;

        Transform cameraTransform = _portalCamera.transform;
        cameraTransform.position = transform.position;
        cameraTransform.rotation = transform.rotation;

        for (int i = 0; i <= iterator; ++i)
        {
            // Position the camera behind the other portal.
            Vector3 relativePos = inTransform.InverseTransformPoint(cameraTransform.position);
            relativePos = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativePos;
            cameraTransform.position = outTransform.TransformPoint(relativePos);

            // Rotate the camera to look through the other portal.
            Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * cameraTransform.rotation;
            relativeRot = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativeRot;
            cameraTransform.rotation = outTransform.rotation * relativeRot;
        }

        // Set the camera's oblique view frustum.
        Plane p = new Plane(-outTransform.forward, outTransform.position);
        Vector4 clipPlaneWorldSpace = new Vector4(p.normal.x, p.normal.y, p.normal.z, p.distance);
        Vector4 clipPlaneCameraSpace =
            Matrix4x4.Transpose(Matrix4x4.Inverse(_portalCamera.worldToCameraMatrix)) * clipPlaneWorldSpace;

        Matrix4x4 newMatrix = _mainCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);
        _portalCamera.projectionMatrix = newMatrix;

        // Render the camera to its render target.
        UniversalRenderPipeline.RenderSingleCamera(SRC, _portalCamera);
    }
}
