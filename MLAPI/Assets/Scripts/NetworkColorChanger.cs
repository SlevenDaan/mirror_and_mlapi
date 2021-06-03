using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkedVar;
using UnityEngine;

public class NetworkColorChanger : NetworkedBehaviour
{
    //Server vars
    [SerializeField] [SyncedVar] private Color color;
    //Client vars
    [SerializeField] private new Renderer renderer;
    private Material material;
    private float colorValue = 0f;

    private void Start()
    {
        material = new Material(Shader.Find("Standard"));
        renderer.material = material;
    }

    private void FixedUpdate()
    {
        if (IsClient) { ClientFixedUpdate(); }
    }

    private void ClientFixedUpdate()
    {
        material.color = color;

        if (!IsOwner) { return; }
        colorValue = (colorValue + Time.fixedDeltaTime) % 1f;
        Color newColor = new Color(colorValue, colorValue, colorValue, 1f);
        InvokeServerRpc(RpcUpdateColor, newColor, "Color");
    }

    [ServerRPC]
    private void RpcUpdateColor(Color color)
    {
        this.color = color;
        if (!IsClient)
        {
            material.color = this.color;
        }
    }
}
