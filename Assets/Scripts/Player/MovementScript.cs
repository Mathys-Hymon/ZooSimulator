using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField] Vector2 MaxPosition;
    [SerializeField] Vector2 MinPosition;
    [SerializeField] GameObject PauseMenuRef;
    private Vector2 Direction;
    private bool PauseMenuOpen;

    private void Update()
    {
        Direction.x = Input.GetAxis("Horizontal")*10;
        Direction.y = Input.GetAxis("Vertical")*10;


        if(transform.position.x < MaxPosition.x)
        {
            transform.position = transform.position + (new Vector3(Direction.x, 0 , 0) * Time.deltaTime);
        }

        else
        {
            transform.position = transform.position + (new Vector3(-15, 0, 0) * Time.deltaTime);
        }

        if (transform.position.x > MinPosition.x)
        {
            transform.position = transform.position + (new Vector3(Direction.x, 0, 0) * Time.deltaTime);
        }
        else
        {
            transform.position = transform.position + (new Vector3(15, 0, 0) * Time.deltaTime);
        }

        if (transform.position.y < MaxPosition.y)
        {
            transform.position = transform.position + (new Vector3(0, Direction.y, 0) * Time.deltaTime);
        }
        else
        {
            transform.position = transform.position + (new Vector3(0, -15 , 0) * Time.deltaTime);
        }

        if (transform.position.y > MinPosition.y)
        {
            transform.position = transform.position + (new Vector3(0, Direction.y, 0) * Time.deltaTime);
        }
        else
        {
            transform.position = transform.position + (new Vector3(0, 15, 0) * Time.deltaTime);
        }


        if(Input.GetKeyDown(KeyCode.Escape))
        {
            FlipFlopMenu();
        }

    }

    public void FlipFlopMenu()
    {
        if (!PauseMenuOpen)
        {
            PauseMenuRef.SetActive(true);
            Time.timeScale = 0;
            PauseMenuOpen = true;
        }
        else
        {
            PauseMenuRef.SetActive(false);
            Time.timeScale = 1;
            PauseMenuOpen = false;
        }
    }

}
