using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceive : MonoBehaviour
{

    // receiving Thread
    Thread receiveThread;

    // udpclient object
    UdpClient client;

    public int port;

    public string lastReceivedUDPPacket;

    // Start is called before the first frame update
    void Start()
    
    {
        Debug.Log(">>> start >>");
        init();
    }

    // Update is called once per frame
    void Update()
    {

    }


    // init
    private void init()
    {
        Debug.Log(">>> init >>");

        // define port
        port = 25001;

        // ----------------------------
        Debug.Log("rec on " + port + " >>");

        receiveThread = new Thread(
            new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();

    }

    // receive thread 
    private void ReceiveData()
    {
        Debug.Log(">>>rec data on " + port + " >>");
        /* client = new UdpClient(port);
        client.Client.ReceiveTimeout = 1000;
        client.Client.Blocking = false;
        IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);

        */

        IPAddress ip = IPAddress.Any;
        IPEndPoint endPoint = new IPEndPoint(ip, port);
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        socket.Bind(endPoint);
        byte[] receiveBufferByte = new byte[1024];
        float[] receiveBufferFloat = new float[receiveBufferByte.Length / sizeof(float)];


        while (true)
        {
            try
            {
                Debug.Log("------while--------");

                /* byte[] data = client.Receive(ref anyIP);
                Debug.Log(data[0]);
                Debug.Log(data[1]);
                Debug.Log(data[2]);
                Debug.Log(data[3]);
                Debug.Log(data[4]);
                Debug.Log(data[5]);
                Debug.Log(data[6]);
                Debug.Log(data[7]);
                Debug.Log("---");

                string text = Encoding.ASCII.GetString(data);
                Debug.Log(text);

                // latest UDPpacket
                lastReceivedUDPPacket = text;
                */

                int numberOfBytesReceived = socket.Receive(receiveBufferByte);
                Debug.Log("numberOfBytesReceived "+ numberOfBytesReceived);
                if (numberOfBytesReceived > 0)
                {
                    for (int i = 0; i < numberOfBytesReceived / sizeof(float); i++)
                    {
                        receiveBufferFloat[i] = BitConverter.ToSingle(receiveBufferByte, i * sizeof(float));
                        if (i + 1 < numberOfBytesReceived / sizeof(float))
                            Debug.Log(receiveBufferFloat[i].ToString("n2"));
                        else
                            Debug.Log(receiveBufferFloat[i].ToString("n2"));

                    }

                }
            }
            catch (Exception err)
            {
                Debug.Log(err.ToString());
            }
        }
    }

    // getLatestUDPPacket
    // cleans up the rest
    public string getLatestUDPPacket()
    {
        return lastReceivedUDPPacket;
    }

}
