using Mirror;
using UnityEngine;

public class NetworkColorChanger : NetworkBehaviour
{
    //Server vars
    [SerializeField] [SyncVar] private Color color;
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
        if (isClient) { ClientFixedUpdate(); }
    }

    [Client]
    private void ClientFixedUpdate()
    {
        material.color = color;

        if (!hasAuthority) { return; }
        colorValue = (colorValue + Time.fixedDeltaTime) % 1f;
        Color newColor = new Color(colorValue, colorValue, colorValue, 1f);
        CmdUpdateColor(newColor);
    }

    [Command]
    private void CmdUpdateColor(Color color)
    {
        this.color = color;
        if (!isClient)
        {
            material.color = this.color;
        }
    }
}
