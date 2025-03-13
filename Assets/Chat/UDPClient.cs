using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class UDPClient : MonoBehaviour
{
    private UdpClient udpClient;
    private IPEndPoint remoteEndPoint;
    public bool isServerConnect = false;

    public void StarUDPClient(string IpAddress, int port)
    {
        udpClient = new UdpClient();
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IpAddress), port);
        udpClient.BeginReceive(ReceiveData, null);
        SendData("Hallo server!");
        isServerConnect = true;
    }

    public void ReceiveData(IAsyncResult result)
    {
        byte[] receiveBytes = udpClient.EndReceive(result, ref remoteEndPoint);
        string receiveMessage = System.Text.Encoding.UTF8.GetString(receiveBytes);
        Debug.Log("Received front server: " + receiveMessage);
        udpClient.BeginReceive(ReceiveData, null);
    }
    public void SendData(string message)
    {
        byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(message);
        udpClient.Send(sendBytes, sendBytes.Length, remoteEndPoint);
        Debug.Log("Sent to server: " + message);
    }
}
