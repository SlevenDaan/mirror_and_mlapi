using MLAPI;
using MLAPI.Transports.UNET;
using UnityEngine;

public class AutoJoin : MonoBehaviour
{
    [SerializeField] private UNetTransport transport;

    private void Start()
    {
        transport.ConnectAddress = CommandLineArgs.IpAddress;

        if (CommandLineArgs.RunAsServer)
        {
            NetworkManager.Singleton.StartServer();
        }
        else
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}
