using kcp2k;
using Mirror;
using UnityEngine;

public class AutoJoin : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private KcpTransport kcpTransport;

    private void Start()
    {
        if (Application.isEditor) { return; }
        if (networkManager.mode != NetworkManagerMode.Offline) { return; }

        networkManager.networkAddress = CommandLineArgs.IpAddress;
        kcpTransport.Port = CommandLineArgs.Port;

        if (CommandLineArgs.RunAsServer)
        {
            networkManager.StartServer();
        }
        else
        {
            networkManager.StartClient();
        }
    }
}
