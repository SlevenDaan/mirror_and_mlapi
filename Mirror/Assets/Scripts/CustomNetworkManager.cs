using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    private const string NetworkObjectsResourceFolderPath = "SpawnableNetworkObjects";
    private bool loadedNetworkObjectsResourceFolder = false;
    [Space]
    [SerializeField] private Agones.AgonesSdk agones;

    public override void OnStartServer()
    {
        if (loadedNetworkObjectsResourceFolder) { return; }
        LoadNetworkObjectsResourceFolder();
        ConnectAgones();
    }

    public override void OnStartClient()
    {
        if (loadedNetworkObjectsResourceFolder) { return; }
        LoadNetworkObjectsResourceFolder();
        foreach (var spawnPrefab in spawnPrefabs)
        {
            ClientScene.RegisterPrefab(spawnPrefab);
        }
    }

    private void LoadNetworkObjectsResourceFolder()
    {
        spawnPrefabs.AddRange(Resources.LoadAll<GameObject>(NetworkObjectsResourceFolderPath).ToList());
        loadedNetworkObjectsResourceFolder = true;
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        GameObject player = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        if (numPlayers <= 0)
        {
            ShutdownAgones();
        }
    }

    #region Agones
    private async void ConnectAgones()
    {
        if (await agones.Connect())
        {
            Debug.Log("Succesfully connected Agones sdk");
            await ReadyAgones();
        }
        else
        {
            Debug.LogError("Could not connect Agones sdk");
        }
    }

    private async Task ReadyAgones()
    {
        if (await agones.Ready())
        {
            Debug.Log("Succesfully readied Agones sdk");
        }
        else
        {
            Debug.LogError("Could not ready Agones sdk");
        }
    }

    private async void ShutdownAgones()
    {
        if (await agones.Shutdown())
        {
            Debug.Log("Succesfully shut down Agones sdk");
        }
        else
        {
            Debug.LogError("Could not shut down Agones sdk");
        }
    }
    #endregion
}
