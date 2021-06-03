using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class NetworkRigidbody : NetworkBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;

    private void FixedUpdate()
    {
        if (IsServer) { ServerFixedUpdate(); }
    }

    private void ServerFixedUpdate()
    {
        UpdateLocationClientRpc(rigidbody.velocity, transform.position, transform.rotation);
    }

    [ClientRpc]
    private void UpdateLocationClientRpc(Vector3 velocity, Vector3 position, Quaternion rotation)
    {
        if (IsHost) { return; }
        rigidbody.velocity = velocity;
        transform.position = position;
        transform.rotation = rotation;
    }
}
