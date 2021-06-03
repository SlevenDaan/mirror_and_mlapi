using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private GameObject playerChild;

    [SerializeField] private new Rigidbody rigidbody;

    private const float TimeBetweenDirectionChanges = 1;
    [SerializeField] private float timer = 1;

    private const float Speed = 10f;

    private void Start()
    {
        if (NetworkManager.Singleton.IsClient) { ClientStart(); }
    }

    private void ClientStart()
    {
        if (!IsOwner) { return; }
        for (int i = 0; i < CommandLineArgs.PlayerChildObjects; i++)
        {
            SpawnChildServerRpc();
        }
    }

    private void FixedUpdate()
    {
        if (IsClient)
        {
            ClientFixedUpdate();
        }
    }

    private void ClientFixedUpdate()
    {
        if (!IsOwner) { return; }

        timer -= Time.fixedDeltaTime;
        if (timer > 0) { return; }
        while (timer <= 0)
        {
            UpdateOnTimer();
            timer += TimeBetweenDirectionChanges;
        }
    }

    private void UpdateOnTimer()
    {
        ChangeDirectionServerRpc(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsServer)
        {
            BounceCounter.Bounces++;
        }
    }

    #region RPCs
    [ServerRpc(RequireOwnership = true)]
    private void ChangeDirectionServerRpc(Vector3 direction)
    {
        rigidbody.velocity = direction.normalized * Speed;
    }

    [ServerRpc(RequireOwnership = true)]
    private void SpawnChildServerRpc()
    {
        GameObject gameObject = Instantiate(playerChild, transform.position, transform.rotation);
        gameObject.GetComponent<NetworkObject>().Spawn();
        gameObject.GetComponent<NetworkObject>().ChangeOwnership(GetComponent<NetworkObject>().OwnerClientId);
    }
    #endregion
}
