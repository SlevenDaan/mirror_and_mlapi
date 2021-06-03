using MLAPI;
using MLAPI.Transports.UNET;
using UnityEngine;

public class AutoJoin : MonoBehaviour
{
    [SerializeField] private UnetTransport transport;

    private void Start()
    {
        transport.ConnectAddress = CommandLineArgs.IpAddress;

        if (CommandLineArgs.RunAsServer)
        {
            NetworkingManager.Singleton.StartServer();
        }
        else
        {
            NetworkingManager.Singleton.StartClient();
        }
    }
}
