using TMPro;
using UnityEngine;

public class UDPServerUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int serverPort = 5555;
    [SerializeField] private UDPServer _server;
    [SerializeField] private TMP_InputField messageInput;

    public void SendServerMessage()
    {
        if (!_server.IsServerRunning)
        {
            Debug.Log("The server is not running");
            return;
        }

        if (messageInput.text == "")
        {
            Debug.Log("The Chat entry is empty");
            return;
        }

        string mensaje = messageInput.text;
        _server.SendData(mensaje);
    }

    public void StartServer()
    {
        _server.StartUdpServer(serverPort);
    }
}
