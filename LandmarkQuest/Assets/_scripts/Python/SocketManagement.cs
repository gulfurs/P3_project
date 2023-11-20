using System;
using System.Net.Sockets;
using UnityEngine;

public class SocketManagement : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    private byte[] buffer = new byte[4];

    public int currentMessage = 0; // Default value for no message

    void Start()
    {
        client = new TcpClient("localhost", 12345);  // Connect to the Python server
        stream = client.GetStream();
    }

    void Update()
    {
        if (IsStreamAvailable)
        {
            stream.Read(buffer, 0, 4);
            currentMessage = BitConverter.ToInt32(buffer, 0);
            //Debug.Log("Received message from Python: " + currentMessage);
        }
    }

    // Public method to get the current message
    public int GetCurrentMessage()
    {
        return currentMessage;
    }

    private void OnApplicationQuit()
    {
        // Clean up and close the client socket when the application is closed
        if (stream != null)
        {
            stream.Close();
        }
        if (client != null)
        {
            client.Close();
        }
    }

    public bool IsStreamAvailable
    {
        get { return stream != null && stream.DataAvailable; }
    }
}