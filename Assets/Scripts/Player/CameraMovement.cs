using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Vector2 minPosition;
    [SerializeField] private Vector2 maxPosition;
    [SerializeField] private float smoothingSpeed;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject PlayerRef;
    private GameObject target;
    private float x, y, z;
    private float yOffset = 3;
    private float zoom = 5f;


    void Start()
    {
        target = PlayerRef;
        x = transform.position.x;
        y = transform.position.y + yOffset;
        z = transform.position.z;
    }


    public void SetTarget(GameObject NewTarget)
    {
        if (NewTarget != null)
        {
            
            target = NewTarget;
        }
        
        else
        {
            target = PlayerRef;
        }

    }

    void Update()
    {
        if (target.transform.position.x < minPosition.x || target.transform.position.x > maxPosition.x)
        {
            x = Mathf.Clamp(target.transform.position.x, minPosition.x, maxPosition.x);
        }
        else if (Mathf.Abs(transform.position.x - target.transform.position.x) > 0.1f)
        {
            x = target.transform.position.x;
        }
        if (target.transform.position.y < minPosition.y || target.transform.position.y > maxPosition.y)
        {
            y = Mathf.Clamp(target.transform.position.y + yOffset, minPosition.y, maxPosition.y);
        }
        else if (Mathf.Abs(transform.position.y - target.transform.position.y) > 0.1f)
        {
            y = target.transform.position.y + yOffset;
        }
        Vector3 targetPosition = Vector3.Lerp(transform.position, new Vector3(x, y, z), smoothingSpeed * Time.deltaTime);
        transform.position = new Vector3(targetPosition.x, targetPosition.y, z);

        if(cam.orthographicSize <= 20 && cam.orthographicSize >= 3)
        {
            cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoom;
        }

        else if(cam.orthographicSize > 20)
        {
            cam.orthographicSize = 20;
            smoothingSpeed = 1.5f;
        }

        else if(cam.orthographicSize < 3)
        {
            cam.orthographicSize = 3;
            smoothingSpeed = 6;
        }

        if(smoothingSpeed >= 1.5f && smoothingSpeed <= 6)
        {
            smoothingSpeed -= -Input.GetAxis("Mouse ScrollWheel") * zoom / 4;
        }

        else if(smoothingSpeed > 6)
        {
            smoothingSpeed = 6;
        }

        else if(smoothingSpeed < 1.5f)
        {
            smoothingSpeed = 1.5f;
        }
    }

}