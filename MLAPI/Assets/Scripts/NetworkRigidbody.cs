using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class NetworkRigidbody : NetworkedBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;

    private void FixedUpdate()
    {
        if (IsServer) { ServerFixedUpdate(); }
    }

    private void ServerFixedUpdate()
    {
        InvokeClientRpcOnEveryone(RpcUpdateLocation, rigidbody.velocity, transform.position, transform.rotation, "Movement");
    }

    [ClientRPC]
    private void RpcUpdateLocation(Vector3 velocity, Vector3 position, Quaternion rotation)
    {
        if (IsHost) { return; }
        rigidbody.velocity = velocity;
        transform.position = position;
        transform.rotation = rotation;
    }
}
