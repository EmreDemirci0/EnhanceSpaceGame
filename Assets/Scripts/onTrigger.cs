using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onTrigger : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("girdi");
      //  other.gameObject.tag = "item";

        GameController.isShoot = true;
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("cikti");

    //    //  other.gameObject.tag = "item";

    //    GameController.isShoot = false;
    //}

}
