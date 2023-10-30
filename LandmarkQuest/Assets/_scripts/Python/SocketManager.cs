using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using UnityEngine;

public class SocketManager : MonoBehaviour
{
    public string serverIP = "localhost";
    public int serverPort = 12345;
    public bool isBlinking{ get; private set; }
    private bool wasBlinking = false;
    public Vector2 leftEyeCoords { get; private set; }
    public Vector2 rightEyeCoords { get; private set; }
    public bool day;

    private TcpClient client;
    private NetworkStream stream;
    private BinaryReader reader;

    private void Start()
    {
        try
        {
            client = new TcpClient(serverIP, serverPort);
            stream = client.GetStream();
            reader = new BinaryReader(stream);
        }
        catch (Exception e)
        {
            Debug.LogError("Error connecting to the server: " + e.Message);
            return;
        }

        // Start receiving data from the server in a separate thread or coroutine
        // For example:
        //StartCoroutine(ReceiveData());
    }

    private void Update()
    {
        // Check and log the received gaze tracking status
        wasBlinking = isBlinking; // Store the previous state
        isBlinking = ReceiveGazeStatus();

        if (isBlinking && !wasBlinking)
        {
            Debug.Log("User started blinking");
            day = !day;
        }
        else if (!isBlinking && wasBlinking)
        {
            Debug.Log("User stopped blinking");
        }

        // Receive and update eye coordinates
        //ReceiveEyeCoordinates();
    }

    bool ReceiveGazeStatus()
    {
        try
        {
            // Receive the gaze tracking status (a single boolean) from the server
            byte[] buffer = new byte[1];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            if (bytesRead == 1)
            {
                return BitConverter.ToBoolean(buffer, 0);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error receiving gaze tracking status: " + e.Message);
        }
        return false;
    }

   /* void ReceiveEyeCoordinates()
    {
        try
        {
            // Receive the eye coordinates from the server
            byte[] buffer = new byte[16]; // Assuming 8 bytes for each eye (2 floats per eye)
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            if (bytesRead == 16)
            {
                // Parse the received data into left and right eye coordinates (2 floats each)
                leftEyeCoords = new Vector2(
                    BitConverter.ToSingle(buffer, 0),
                    BitConverter.ToSingle(buffer, 4)
                );
                rightEyeCoords = new Vector2(
                    BitConverter.ToSingle(buffer, 8),
                    BitConverter.ToSingle(buffer, 12)
                );
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error receiving eye coordinates: " + e.Message);
        }
    }*/

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
        if (reader != null)
        {
            reader.Close();
        }
    }
}
