﻿using Mirror;
using UnityEngine;

public class BounceCounter : NetworkBehaviour
{
    [SyncVar, SerializeField] private int bounceCount;

    private static BounceCounter bounceCounter;
    public static int Bounces
    {
        get => bounceCounter.bounceCount;
        set => bounceCounter.bounceCount = value;
    }


    private void OnEnable()
    {
        bounceCounter = this;
    } 
}
