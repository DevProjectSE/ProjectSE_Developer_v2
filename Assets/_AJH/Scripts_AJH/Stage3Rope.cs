using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Rope : MonoBehaviour
{
    public GameObject knife;

    
    void OnTriggerEnter(Collider other)
    {
            //print("충돌함" + other.gameObject.name);   
        
        if (other.gameObject.CompareTag("Knife"))
        {
            //print("충돌함2");
            Destroy(gameObject);
            Stage3Item.Instance.ropeNum --;
            print(Stage3Item.Instance.ropeNum); 
        }
    }


}
