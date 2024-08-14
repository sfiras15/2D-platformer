using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISwappable
{
    public void Swap(Vector3 position);

    public void LockTarget();

    public void FreezePosition();
    public void UnFreezePosition();



}
