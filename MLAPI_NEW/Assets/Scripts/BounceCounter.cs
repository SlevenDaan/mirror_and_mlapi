using MLAPI;
using MLAPI.NetworkVariable;
using UnityEngine;

public class BounceCounter : NetworkBehaviour
{
    [SerializeField] private NetworkVariable<int> bounceCount = new NetworkVariable<int>();

    private static BounceCounter bounceCounter;
    public static int Bounces
    {
        get => bounceCounter.bounceCount.Value;
        set => bounceCounter.bounceCount.Value = value;
    }


    private void OnEnable()
    {
        bounceCounter = this;
    } 
}
