using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArduinoBluetoothAPI;
using System;

public class Main_Manager : MonoBehaviour
{

    // Use this for initialization
    private BluetoothHelper helper;

    private bool isScanning;
    private bool isConnecting;

    public string deviceName = "Type Name Here";
    public bool IsConnectedJoh = false;
    public bool TriedOnce = false;
    public string received_message;
    public string ToLoco = "0";

    private string data;

    private string tmp;

    private LinkedList<BluetoothDevice> devices;
    // Start is called before the first frame update
    void Start()
    {
        deviceName = "BrandNewMinds"; //bluetooth should be turned ON;
        data = "";
        tmp = "";
        try
        {
            BluetoothHelper.BLE = true;
            helper = BluetoothHelper.GetInstance(deviceName);
            helper.OnConnected += OnConnected;
            helper.OnConnectionFailed += OnConnectionFailed;
            helper.OnScanEnded += OnScanEnded;
            helper.OnDataReceived += OnDataReceived;

            helper.setCustomStreamManager(new MyStreamManager()); //implement your own way of delimiting the messages
            //helper.setTerminatorBasedStream("\n"); //every messages ends with new line character

        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

    }


    public IEnumerator SendData()
    {
        Debug.Log("Trigger?");

        while (helper.isConnected())
        {

            try
            {
                helper.SendData(ToLoco);
                Debug.Log(ToLoco);

            }
            catch (Exception ex) { }

            yield return new WaitForSeconds(1f); //was 1
        }
        StopCoroutine(SendData());
    }

    //Asynchronous method to receive messages
    void OnDataReceived(BluetoothHelper helper)
    {
        data += "\n<" + helper.Read();
    }

    void OnScanEnded(BluetoothHelper helper, LinkedList<BluetoothDevice> devices)
    {
        this.isScanning = false;
        this.devices = devices;
    }

    void OnConnected(BluetoothHelper helper)
    {
        try
        {
            helper.StartListening();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        Debug.Log("Connected");
        IsConnectedJoh = true;

    }

    void OnConnectionFailed(BluetoothHelper helper)
    {

        try
        {
            helper.StartListening();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        Debug.Log("Connection Failed");
        IsConnectedJoh = false;
        TriedOnce = true;

    }


    //Call this function to emulate message receiving from bluetooth while debugging on your PC.
    public void ButtonClick()
    {

        if (!helper.isConnected() && IsConnectedJoh == false)
        {
            if (helper.isDevicePaired())
                helper.Connect(); // tries to connect
        }

        if (helper.isConnected())
        {
            helper.Disconnect();
        }

    }

    public void SendToLoco()
    {
        StartCoroutine(SendData());
    }



    void OnDestroy()
    {
        if (helper != null)
            helper.Disconnect();
    }

}
