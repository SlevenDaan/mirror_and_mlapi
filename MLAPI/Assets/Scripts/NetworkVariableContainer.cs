using MLAPI;
using MLAPI.NetworkedVar;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public struct NetworkVariableContainer : INetworkedVar
{
    public bool CanClientRead(ulong clientId)
    {
        throw new System.NotImplementedException();
    }

    public bool CanClientWrite(ulong clientId)
    {
        throw new System.NotImplementedException();
    }

    public string GetChannel()
    {
        throw new System.NotImplementedException();
    }

    public bool IsDirty()
    {
        throw new System.NotImplementedException();
    }

    public void ReadDelta(Stream stream, bool keepDirtyDelta)
    {
        throw new System.NotImplementedException();
    }

    public void ReadField(Stream stream)
    {
        throw new System.NotImplementedException();
    }

    public void ResetDirty()
    {
        throw new System.NotImplementedException();
    }

    public void SetNetworkedBehaviour(NetworkedBehaviour behaviour)
    {
        throw new System.NotImplementedException();
    }

    public void WriteDelta(Stream stream)
    {
        throw new System.NotImplementedException();
    }

    public void WriteField(Stream stream)
    {
        throw new System.NotImplementedException();
    }
}
