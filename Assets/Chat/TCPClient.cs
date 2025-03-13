using UnityEngine;
using System.Net;
using System;
using System.Net.Sockets;
using TMPro;
public class TCPClient : MonoBehaviour
{
    private TcpClient tcpClient;
    private NetworkStream networkStream;
    private byte[] receiveBuffer;

    public bool inServerConnect;
    public TextMeshProUGUI textoCliente;

    public void ConnectToServer(string ipAddres, int port)
    {
        tcpClient = new TcpClient();
        tcpClient.Connect(IPAddress.Parse(ipAddres), port);
        networkStream = tcpClient.GetStream();
        receiveBuffer = new byte[tcpClient.ReceiveBufferSize];
        networkStream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, ReceiveData, null);
        inServerConnect = true;
    }
    private void ReceiveData(IAsyncResult result)
    {
        int bytesRead = networkStream.EndRead(result);
        byte[] receivedBytes = new byte[bytesRead];
        Array.Copy(receiveBuffer, receivedBytes, bytesRead);
        string receivedMessage = System.Text.Encoding.UTF8.GetString(receivedBytes);
        Debug.Log("Received fron client: " + receivedMessage);
        
        textoCliente.text = receivedMessage;
        networkStream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, ReceiveData, null);
    }

    public void SendData(string message)
    {
        try
        {
            byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(message);
            networkStream.Write(sendBytes, 0, sendBytes.Length);
            networkStream.Flush();
            Debug.Log("Sent to server: " + message);
        }
        catch
        {
            Debug.Log("There is no server to send the message: " + message);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
