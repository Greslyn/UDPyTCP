using UnityEngine;
using TMPro;

public class ClientUI : MonoBehaviour
{
    public int serverPort = 5555;
    public string serverAddress = "127.0.0.1";
    [SerializeField] private TMP_InputField messageInput;
    [SerializeField] private TCPClient _client;

    public void SendClientMessage()
    {
        if (!_client.inServerConnect)
        {
            Debug.Log("The client is not connected");
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
    public void ConnectClient()
    {
        _client.ConnectToServer(serverAddress, serverPort);
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
