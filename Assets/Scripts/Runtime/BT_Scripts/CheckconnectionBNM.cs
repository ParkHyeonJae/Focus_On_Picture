using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckconnectionBNM : MonoBehaviour
{
    public GameObject NotConnectedBall;


    public GameObject Main_Manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // if (Main_Manager.GetComponent<Manager>().IsConnectedJoh.Equals(true))
        {

            Destroy(this.gameObject);
            NotConnectedBall.SetActive(false);

        }

      //  if (Main_Manager.GetComponent<Manager>().TriedOnce.Equals(true))
        {

            NotConnectedBall.SetActive(true);

        }
    }
}
