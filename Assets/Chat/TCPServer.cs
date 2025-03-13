using UnityEngine;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;
using System;
public class TCPServer : MonoBehaviour
{
    private TcpListener tpcListener;
    private TcpClient connectedClient;
    private NetworkStream networkStream;
    private byte[] receiveBuffer;
    public bool isServerRunning;
    public void StartServer(int port)
    {
        tpcListener = new TcpListener(IPAddress.Any, port);
        tpcListener.Start();
        Debug.Log("Server started, waiting for connections...");
        tpcListener.BeginAcceptTcpClient(HandleIncomingConnection, null);
        isServerRunning = true;
    }

    private void HandleIncomingConnection(IAsyncResult result)
    {
        connectedClient = tpcListener.EndAcceptTcpClient(result);
        networkStream = connectedClient.GetStream();
        Debug.Log("Client connected: " + connectedClient);
        receiveBuffer = new byte[connectedClient.ReceiveBufferSize];
        networkStream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, ReceiveData, null);
        tpcListener.BeginAcceptTcpClient(HandleIncomingConnection, null);
    }

    private void ReceiveData(IAsyncResult result)
    {
        int bytesRead = networkStream.EndRead(result);
        if (bytesRead <= 0)
        {
            Debug.Log("Client disconneted: " + connectedClient.Client.RemoteEndPoint);
            connectedClient.Close();
            return;
        }
        byte[] receivedBytes = new byte[bytesRead];
        Array.Copy(receiveBuffer, receivedBytes, bytesRead);
        string receivedMessage = System.Text.Encoding.UTF8.GetString(receivedBytes);
        Debug.Log("Received front client: " + receivedMessage);
        networkStream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, ReceiveData, null);
    }

    public void SendData(string message)
    {
        try
        {
            byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(message);
            networkStream.Write(sendBytes, 0, sendBytes.Length);
            networkStream.Flush();
            Debug.Log("Sent to client: " + message);
        }
        catch
        {
            Debug.Log("There is no client to send the message: " + message);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
