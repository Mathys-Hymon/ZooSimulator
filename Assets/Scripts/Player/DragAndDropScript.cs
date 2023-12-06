using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDropScript : MonoBehaviour
{
    [SerializeField] private LayerMask LayerZone;


    private GameObject GrabRef;
    private Collider2D GrabCollider;
    private bool MouseButtonDown;
    private bool CanDrop;


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject.layer == 3)
                {
                  MouseButtonDown = true;
                  GrabRef = hit.collider.gameObject;
                GrabCollider = hit.collider.gameObject.GetComponent<Collider2D>();
                GrabCollider.enabled = false;
                CanDrop = false;
                }
            
        }
        if(Input.GetMouseButtonUp(0)) 
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero, Mathf.Infinity, LayerZone);

            if (hit)
            {
                if (hit.collider.gameObject.layer == 7)
                {
                    CanDrop = true;
                    GrabCollider.enabled = true;
                    GrabRef = null;
                }
            }
            MouseButtonDown = false;
        }

        if(MouseButtonDown && GrabRef != null || !CanDrop && GrabRef!= null)
        {
            if(!MouseButtonDown)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero, Mathf.Infinity, LayerZone);

                if (hit)
                {
                    if(hit.collider.gameObject.layer == 7)
                    {
                    CanDrop = true;
                    GrabCollider.enabled = true;
                    GrabRef = null;
                    }
                }
                
            }
            if(GrabRef != null)
            {
                var GrabPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GrabPosition.z = 0;
                GrabRef.transform.position = GrabPosition;
            }
        }
    }
}
