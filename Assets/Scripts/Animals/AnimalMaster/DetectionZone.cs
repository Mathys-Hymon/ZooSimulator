using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{

    [SerializeField] private AnimalMasterScript Ref;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ref.MoveTo(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Ref.CancelMoveTo(collision.gameObject);
    }
}
