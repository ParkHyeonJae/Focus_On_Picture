using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BNM_Scripts : MonoBehaviour
{
    public GameObject Bluetoothscript;
    public Transform Smell1;
    public Transform Smell2;
    public Transform Wind;
    public Transform Heat;
    public Transform Cold;

    public bool wind_on = false;
    public bool heat_on = false;
    public bool smell1_on = false;
    public bool smell2_on = false;
    public bool Cold_on = false;

    void Update()
    {
        float DistSMell = Vector3.Distance(Smell1.position, transform.position);
        float DistSMel2 = Vector3.Distance(Smell2.position, transform.position);
        float DistWind = Vector3.Distance(Wind.position, transform.position);
        float DistHeat = Vector3.Distance(Heat.position, transform.position);
        float DistCold = Vector3.Distance(Cold.position, transform.position);

        // Debug.Log(DistSMell);
        // Debug.Log(DistWind);
        // Debug.Log(DistHeat);

        if (DistSMell < 1f && smell1_on == false)
        {
            SwitchSender("Y");
            Debug.Log("SMELL1_ON");
            smell1_on = true;
        }

        if (DistSMel2 < 1f && smell2_on == false)
        {
            SwitchSender("Z");
            Debug.Log("SMELL2_ON");
            smell2_on = true;
        }

        if (DistWind < 1f && wind_on == false)
        {
            SwitchSender("B");
            Debug.Log("WIND_ON");
            wind_on = true;
        }

        if (DistHeat < 1f && heat_on == false)
        {
            SwitchSender("R");
            Debug.Log("HEAT_ON");
            heat_on = true;
        }

        if (DistCold < 1f && Cold_on == false)
        {
            SwitchSender("C");
            Debug.Log("FREEZING");
            Cold_on = true;
        }

        if (DistSMell > 1f && smell1_on == true)
        {
            SwitchSender("y");
            Debug.Log("SMELL_OFF");
            smell1_on = false;
        }

        if (DistSMel2 > 1f && smell2_on == true)
        {
            SwitchSender("z");
            Debug.Log("SMELL_OFF");
            smell2_on = false;
        }

        if (DistWind > 1f && wind_on == true)
        {
            SwitchSender("b");
            Debug.Log("WIND_OFF");
            wind_on = false;
        }

        if (DistHeat > 1f && heat_on == true)
        {
            SwitchSender("r");
            Debug.Log("SUN_OFF");
            heat_on = false;
        }

        if (DistCold > 1f && Cold_on == true)
        {
            SwitchSender("c");
            Debug.Log("FREEZING_OFF");
            Cold_on = false;
        }



    }

   

    void SwitchSender(string Character)
        {

            Bluetoothscript.GetComponent<manager>().SendData(Character);

        }
    }

