using System.Linq;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    public enum CameraViewSize
    {
        Minimized,
        Maximized
    }

    public Animator animatorRef;
    public RenderTexture renderTextureRef;
    [Space]
    public CameraViewSize currentCameraViewSize = CameraViewSize.Minimized;
    public int minSize = 5;
    public int maxSize = 10;

    private Camera minimapCamera;

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player").transform;
        var camera = new GameObject("MinimapCamera")
        {
            transform =
            {
                parent = player,
                localPosition = new Vector3(0, 10, 0),
                localRotation = Quaternion.Euler(90, 0, 0)
            }
        };

        minimapCamera = camera.AddComponent<Camera>();
        minimapCamera.orthographic = true;
        minimapCamera.targetTexture = renderTextureRef;
        ResizeViewArea();

        if (animatorRef == null)
            animatorRef = GetComponent<Animator>();



    }

    public void OnResizeButtonClick()
    {
        if (animatorRef.GetBool("IsMinimized") == false)
            animatorRef.SetBool("IsMaximized", true);
        else
            animatorRef.SetBool("IsMaximized", false);

    }

    public void ResizeViewArea()
    {
        if (currentCameraViewSize == CameraViewSize.Minimized)
            currentCameraViewSize = CameraViewSize.Maximized;
        else
            currentCameraViewSize = CameraViewSize.Minimized;

        if (currentCameraViewSize == CameraViewSize.Maximized)
            minimapCamera.orthographicSize = maxSize;
        else
            minimapCamera.orthographicSize = minSize;
    }
}
