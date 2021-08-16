using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkifconnected : MonoBehaviour
{

	public GameObject BTFixer;
	//public GameObject LocoCanvas;
//	public Animator startscreen;


    // Update is called once per frame
    void Update()
    {
     
        
        
        if (BTFixer.GetComponent<Main_Manager>().IsConnectedJoh == true)
        {

           // LocoCanvas.GetComponent<OverallControll_Original>().enabled = true;
           // LocoCanvas.GetComponent<OverallControll_Be>().enabled = true;
            //startscreen.SetTrigger("Away");

            Destroy(this.gameObject);

        }
        
    }

}
