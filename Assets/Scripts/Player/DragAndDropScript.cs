using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropScript : MonoBehaviour
{
    [SerializeField] private LayerMask LayerZone;

    private GameObject GrabRef;
    private bool MouseButtonDown;


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            print("ButtonDown");
            MouseButtonDown = true;
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition,);
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit)) 
            //{
            
            //}
        }

        if(Input.GetMouseButtonUp(0)) 
        {
            print("ButtonUP");
            MouseButtonDown = false;
        }

    }
}
