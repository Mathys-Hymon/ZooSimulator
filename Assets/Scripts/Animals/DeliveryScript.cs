using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryScript : MonoBehaviour
{
  

    public void SpawnAnimal(GameObject Animal)
    {
        Instantiate(Animal);
        Animal.transform.position = gameObject.transform.position;
    }
}
