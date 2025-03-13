using UnityEngine;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;
using System;

public class UDPServer : MonoBehaviour
{
    private UdpClient UdpServer;
    private IPEndPoint remoteEndPoint;
    public bool IsServerRunning = false;
    public void StartUdpServer(int port)
    {
        UdpServer = new UdpClient(port);
        remoteEndPoint = new IPEndPoint(IPAddress.Any, port);
        Debug.Log("Server started, waiting for connections...");
        UdpServer.BeginReceive(ReceiveData, null);
        IsServerRunning = true;
    }

    private void ReceiveData(IAsyncResult result)
    {
        byte[] receiveBytes = UdpServer.EndReceive(result, ref remoteEndPoint);
        string receiveMessage = System.Text.Encoding.UTF8.GetString(receiveBytes);
        Debug.Log("Received front client: " + receiveMessage);
        UdpServer.BeginReceive(ReceiveData, null);
    }

    public void SendData(string message)
    {
        try
        {
            byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(message);
            UdpServer.Send(sendBytes, sendBytes.Length, remoteEndPoint);
            Debug.Log("Sent to client: " + message);
        }
        catch
        {
            Debug.Log("There is no client to send the message: " + message);
        }
    }
}
