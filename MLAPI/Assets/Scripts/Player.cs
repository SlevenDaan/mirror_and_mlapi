using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class Player : NetworkedBehaviour
{
    [SerializeField] private GameObject playerChild;

    [SerializeField] private new Rigidbody rigidbody;

    private const float TimeBetweenDirectionChanges = 1;
    [SerializeField] private float timer = 1;

    private const float Speed = 10f;

    private void Start()
    {
        if (NetworkingManager.Singleton.IsClient) { ClientStart(); }
    }

    private void ClientStart()
    {
        if (!IsOwner) { return; }
        for (int i = 0; i < CommandLineArgs.PlayerChildObjects; i++)
        {
            InvokeServerRpc(RpcSpawnChild, "Spawning");
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
        InvokeServerRpc(RcpChangeDirection, new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)), "Movement");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsServer)
        {
            BounceCounter.Bounces++;
        }
    }

    #region RPCs
    [ServerRPC(RequireOwnership = true)]
    private void RcpChangeDirection(Vector3 direction)
    {
        rigidbody.velocity = direction.normalized * Speed;
    }

    [ServerRPC(RequireOwnership = true)]
    private void RpcSpawnChild()
    {
        GameObject gameObject = Instantiate(playerChild, transform.position, transform.rotation);
        gameObject.GetComponent<NetworkedObject>().Spawn();
        gameObject.GetComponent<NetworkedObject>().ChangeOwnership(GetComponent<NetworkedObject>().OwnerClientId);
    }
    #endregion
}
