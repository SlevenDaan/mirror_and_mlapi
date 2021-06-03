using Mirror;
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
        if (isClient) { ClientStart(); }
    }

    [Client]
    private void ClientStart()
    {
        if (!hasAuthority) { return; }
        for (int i = 0; i < CommandLineArgs.PlayerChildObjects; i++)
        {
            CmdSpawnChild();
        }
    }

    private void FixedUpdate()
    {
        if (isClient)
        {
            ClientFixedUpdate();
        }
    }

    [Client]
    private void ClientFixedUpdate()
    {
        if (!hasAuthority) { return; }

        timer -= Time.fixedDeltaTime;
        if (timer > 0) { return; }
        while (timer <= 0)
        {
            UpdateOnTimer();
            timer += TimeBetweenDirectionChanges;
        }
    }

    [Client]
    private void UpdateOnTimer()
    {
        CmdChangeDirection(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isServer)
        {
            BounceCounter.Bounces++;
        }
    }

    #region RPCs
    [Command(ignoreAuthority = false)]
    private void CmdChangeDirection(Vector3 direction)
    {
        rigidbody.velocity = direction.normalized * Speed;
    }

    [Command(ignoreAuthority = false)]
    private void CmdSpawnChild()
    {
        GameObject gameObject = Instantiate(playerChild, transform.position, transform.rotation);
        NetworkServer.Spawn(gameObject, connectionToClient);
    }
    #endregion
}
