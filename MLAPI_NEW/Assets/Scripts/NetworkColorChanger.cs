using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

public class NetworkColorChanger : NetworkBehaviour
{
    //Server vars
    [SerializeField] private NetworkVariable<Color> color = new NetworkVariable<Color>();
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
        material.color = color.Value;

        if (!IsOwner) { return; }
        colorValue = (colorValue + Time.fixedDeltaTime) % 1f;
        Color newColor = new Color(colorValue, colorValue, colorValue, 1f);
        UpdateColorServerRpc(newColor);
    }

    [ServerRpc]
    private void UpdateColorServerRpc(Color color)
    {
        this.color.Value = color;
        if (!IsClient)
        {
            material.color = this.color.Value;
        }
    }
}
