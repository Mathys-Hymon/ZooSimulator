using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class CarnivorScript : AnimalMasterScript
{

    private GameObject prey;
    public override void MoveTo(GameObject collision)
    {

        if(collision.GetComponent<AnimalMasterScript>() != null && collision.GetComponent<AnimalMasterScript>().species != species) 
        
        {
            CollisionRef = collision;
            moveTo = true;
            MVDistanceSave = new Vector2(collision.transform.position.x, collision.transform.position.y) - new Vector2(transform.position.x, transform.position.y);
        }

        base.MoveTo(collision);
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);


    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (Hunger < MaxFood / 10)
        //{
        //    print("flee");
        //}

        if (collision.gameObject.GetComponent<AnimalMasterScript>() != null && collision.gameObject.GetComponent<AnimalMasterScript>().species != species)
        {
            Attack();
            prey = collision.gameObject;
        }
    }



    protected void Attack()
    {
        if(prey != null)
        {
            prey.GetComponent<AnimalMasterScript>().GetHit(gameObject);
        }

    }
}
