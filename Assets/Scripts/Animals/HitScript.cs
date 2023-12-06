using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitScript : MonoBehaviour
{

    private int HitValue;

    public void Hit(int value)
    {
        HitValue = value;
        gameObject.transform.localScale = new Vector3(value/2, value/2, 1);
    }

    private void Start()
    {
        gameObject.GetComponent<TMP_Text>().SetText(HitValue.ToString());
    }

    private void Update()
    {
        transform.position += new Vector3(1 * Time.deltaTime, 1 * Time.deltaTime, 0);
        Invoke("KillSelf", 4f);

        if(gameObject.transform.localScale.x <= 0 || gameObject.transform.localScale.y <= 0)
        {
            KillSelf();
        } 
        else
        {
            gameObject.transform.localScale -= new Vector3(0.001f, 0.001f, 0);
        }
    }

    private void KillSelf()
    {
        Destroy(gameObject);
    }

}
