/// <summary>
/// This class clamps the Main Camera within a Base grid layer boundaries. 
/// </summary>
/// 
/// <remarks>
/// This class will clamp the Main Camera to a Base grid layer regardless of size, 
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

    private float zoom = 3f;
    private float height;
    private float width;

    public void Awake()
    {
        TilemapRenderer BaseLayer;
        try
        {
            BaseLayer = GameObject.Find("BaseLayer").GetComponent<TilemapRenderer>();
        }
        catch
        {
            BaseLayer = GameObject.Find("Base").GetComponent<TilemapRenderer>();
        }
        GetTilemapBoundaries(BaseLayer);
        SetClampHeight();
        SetClampWidth();
        CameraTransform = transform;
    }

    public void SwitchTownScene(string baseLayerName)
    {
        TilemapRenderer baseLayer = GameObject.Find("" + baseLayerName + "Base").GetComponent<TilemapRenderer>();
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
    public void GetTilemapBoundaries(TilemapRenderer baseLayer)
    {
        xMin = baseLayer.bounds.min.x;
        xMax = baseLayer.bounds.max.x;
        yMin = baseLayer.bounds.min.y;
        yMax = baseLayer.bounds.max.y;
    }

    public float SetClampHeight()
    {
        return height = 2f * Camera.main.orthographicSize;
    }

    public float SetClampWidth()
    {
        return width = height * Camera.main.aspect / 2;
    }  

    public void PlayerCameraClamp()
    {
        SetClampHeight();
        SetClampWidth();
        float x = ClampX();
        float y = ClampY();
        CameraTransform.position = new Vector3(x, y, CameraTransform.position.z);
    }

    public float ClampX()
    {
        return Mathf.Clamp(player.position.x, xMin + width, xMax - width);
    }

    public float ClampY()
    {
        return Mathf.Clamp(player.position.y, yMin + Camera.main.orthographicSize, yMax - Camera.main.orthographicSize);
    }

    public void ZoomCameraIn()
    {
        if (Input.mouseScrollDelta.y > 0 && Camera.main.orthographicSize > 3f) 
        {
            zoom -= 0.5f;
            Camera.main.orthographicSize = zoom;
        }
    }

    public void ZoomCameraOut()
    {
        if (Input.mouseScrollDelta.y < 0 && Camera.main.orthographicSize < 5f)
        {
            zoom += 0.5f;
            Camera.main.orthographicSize = zoom;
        }
    }
}
