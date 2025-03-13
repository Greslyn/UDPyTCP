using TMPro;
using UnityEngine;

public class UDPClientUI : MonoBehaviour
{
    public int serverPort = 5555;
    public string serverAddress = "127.0.0.1";
    [SerializeField] private UDPClient _client;
    [SerializeField] private TMP_InputField messageInput;

    public void SendServerMessage()
    {
        if (!_client.isServerConnect)
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
        _client.SendData(mensaje);
    }

    public void ConnectToServer()
    {
        _client.StarUDPClient(serverAddress, serverPort);
    }
}
