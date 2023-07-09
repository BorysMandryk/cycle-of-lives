using UnityEngine;

public class CheckpointFreezeType : FreezeType
{
    public override void Freeze()
    {
        base.Freeze();
        GameManager.Instance.SetCheckpoint(transform);
    }
}