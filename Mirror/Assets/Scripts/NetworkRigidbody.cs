using Mirror;
using System;
using UnityEngine;

public class NetworkRigidbody : NetworkBehaviour
{
    [Serializable]
    private struct RigidbodyNetworkData
    {
        public Vector3 velocity;
        public Vector3 position;
        public Quaternion rotation;
    }

    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] [SyncVar(hook = nameof(OnRigidbodyNetworkDataChanged))] private RigidbodyNetworkData rigidbodyNetworkData;

    private void OnRigidbodyNetworkDataChanged(RigidbodyNetworkData oldValue, RigidbodyNetworkData newValue)
    {
        if (!isClientOnly) { return; }
        rigidbody.velocity = newValue.velocity;
        transform.position = newValue.position;
        transform.rotation = newValue.rotation;
    }

    private void FixedUpdate()
    {
        if (isServer) { ServerFixedUpdate(); }
    }

    [Server]
    private void ServerFixedUpdate()
    {
        RigidbodyNetworkData newRigidbodyNetworkData = new RigidbodyNetworkData();
        newRigidbodyNetworkData.velocity = rigidbody.velocity;
        newRigidbodyNetworkData.position = transform.position;
        newRigidbodyNetworkData.rotation = transform.rotation;
        rigidbodyNetworkData = newRigidbodyNetworkData;
    }
}
