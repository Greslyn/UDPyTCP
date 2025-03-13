using UnityEngine;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;
using System;
using TMPro;
public class ServerUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int serverPort = 5555;
    [SerializeField] private TCPServer _server;
    [SerializeField] private TMP_InputField messageInput;

    public void SendServerMessage()
    {
        if (!_server.isServerRunning)
        {
            Debug.Log("The server is not running");
            return;
        }

        if(messageInput.text == "")
        {
            Debug.Log("The Chat entry is empty");
            return;
        }

        string mensaje = messageInput.text;
        _server.SendData(mensaje);
    }

    public void StartServer()
    {
        _server.StartServer(serverPort);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
