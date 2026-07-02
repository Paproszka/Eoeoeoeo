using System.Linq;
using UnityEngine;

public class BallMinimapController : MonoBehaviour
{
    public enum CameraViewSize
    {
        Minimized,
        Maximized
    }

    public Animator animatorRef;
    public RenderTexture renderTextureRef;
    [Space]
    public Transform mainCameraTransform;
    public Transform playerTransform;
    [Space]
    public CameraViewSize currentCameraViewSize = CameraViewSize.Minimized;
    public int minSize = 5;
    public int maxSize = 10;
    public Vector3 offset = new Vector3(0, 10, 0);

    private Camera minimapCamera;
    private GameObject minimapFollower;

    void Start()
    {
        if (playerTransform == null)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (mainCameraTransform == null)
            mainCameraTransform = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();

        minimapFollower = new GameObject("MinimapFollower") 
        { 
            transform = 
            { 
                localPosition = playerTransform.position + offset, 
                localEulerAngles = new Vector3(0, mainCameraTransform.eulerAngles.y, 0) 
            } 
        };

        var camera = new GameObject("MinimapCamera")
        {
            transform =
            {
                parent = minimapFollower.transform,
                localPosition = new Vector3(0, 0, 0),
                localRotation = Quaternion.Euler(90, 0, 0)
            }
        };

        minimapCamera = camera.AddComponent<Camera>();
        minimapCamera.orthographic = true;
        minimapCamera.targetTexture = renderTextureRef;
        ResizeViewArea();
    }

    private void LateUpdate()
    {
        minimapFollower.transform.localPosition = playerTransform.position + offset;
        minimapFollower.transform.localEulerAngles = new Vector3(0, mainCameraTransform.eulerAngles.y, 0);
    }

    public void OnResizeButtonClick()
    {
        bool isMaximized = currentCameraViewSize == CameraViewSize.Maximized ? false : true;
        animatorRef.SetBool("IsMaximized", isMaximized);
    }

    public void SwitchCameraSize()
    {
        if (currentCameraViewSize == CameraViewSize.Maximized)
            currentCameraViewSize = CameraViewSize.Minimized;
        else
            currentCameraViewSize = CameraViewSize.Maximized;
        ResizeViewArea();
    }

    public void ResizeViewArea()
    {
        if (currentCameraViewSize == CameraViewSize.Maximized)
            minimapCamera.orthographicSize = maxSize;
        else
            minimapCamera.orthographicSize = minSize;
    }
}
