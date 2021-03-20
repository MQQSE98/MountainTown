/// <summary>
/// This class clamps the Main Camera within a BaseLayer's boundaries. 
/// </summary>
/// 
/// <remarks>
/// This class will clamp the Main Camera to a BaseLayer regardless of size, 
/// orthographic size or aspect ratio.
/// </remarks>

using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraManager : MonoBehaviour
{
    [SerializeField] 
    Transform player = null;
    Transform CameraTransform;   
    
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    private float Zoom = 3f;
    private float Height;
    private float Width;

    public void Awake()
    {
        TilemapRenderer baseLayer = GameObject.Find("Base").GetComponent<TilemapRenderer>();
        GetTilemapBoundaries(baseLayer);
        SetClampHeight();
        SetClampWidth();
        CameraTransform = transform;
    }

    public void LateUpdate()
    {
        ZoomCameraIn();
        ZoomCameraOut();
        PlayerCameraClamp();
    }

    public float SetClampHeight()
    {
        return Height = 2f * Camera.main.orthographicSize;
    }

    public float SetClampWidth()
    {
        return Width = Height * Camera.main.aspect / 2;
    }

    public void GetTilemapBoundaries(TilemapRenderer baseLayer)
    {
        xMin = baseLayer.bounds.min.x;
        xMax = baseLayer.bounds.max.x;
        yMin = baseLayer.bounds.min.y;
        yMax = baseLayer.bounds.max.y;
    }

    public void PlayerCameraClamp()
    {
        SetClampHeight();
        SetClampWidth();
        float x = Mathf.Clamp(player.position.x, xMin + Width, xMax - Width);
        float y = Mathf.Clamp(player.position.y, yMin + Camera.main.orthographicSize, yMax - Camera.main.orthographicSize);
        CameraTransform.position = new Vector3(x, y, CameraTransform.position.z);
    }

    public void ZoomCameraIn()
    {
        if (Input.mouseScrollDelta.y > 0 && Camera.main.orthographicSize > 3f) 
        {
            Zoom -= 0.5f;
            Camera.main.orthographicSize = Zoom;
        }
    }

    public void ZoomCameraOut()
    {
        if (Input.mouseScrollDelta.y < 0 && Camera.main.orthographicSize < 5f)
        {
            Zoom += 0.5f;
            Camera.main.orthographicSize = Zoom;
        }
    }
}