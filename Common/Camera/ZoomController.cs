using UnityEngine;
using UnityEngine.Assertions;

public class ZoomController : MonoBehaviour
{
    private Camera mainCamera;
    private bool isUpdating;
    private bool isZoomingOut = true;
    private float targetZoom;
    public float DefaultSize;
    public float DeltaZoomOut = 0.2f;
    public float DeltaZoomIn = 0.8f;
    public float Speed = 1;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        DefaultSize = mainCamera.orthographicSize;
    }

    private void Update()
    {
        if (!isUpdating)
            return;

        if (mainCamera == null)
            mainCamera = GetComponent<Camera>();

        //interpolation += Speed * Time.fixedDeltaTime;
        //var newZoom = PreciseLerp.Lerp(DefaultSize, targetZoom, interpolation, 0.80f);
        var currentZoom = mainCamera.orthographicSize;

        if (currentZoom == targetZoom)
        {
            isUpdating = false;
            return;
        }

        isUpdating = true;

        if (isZoomingOut)
        {
            currentZoom += DeltaZoomOut;

            if (currentZoom >= targetZoom)
            {
                currentZoom = targetZoom;
                isUpdating = false;
            }
        }
        else
        {
            currentZoom -= DeltaZoomIn;

            if (currentZoom <= targetZoom)
            {
                currentZoom = targetZoom;
                isUpdating = false;
            }
        }

        mainCamera.orthographicSize = currentZoom; //newZoom;
    }

    public void ResetCameraZoom()
    {
        SetCameraZoom(DefaultSize);
    }

    public void SetCameraZoom(float zoom)
    {
        Assert.IsTrue(zoom > 0);

        targetZoom = zoom;
        isUpdating = true;
        isZoomingOut = targetZoom > mainCamera.orthographicSize;
    }
}