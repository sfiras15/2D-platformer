using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SwapTargets : MonoBehaviour, ISwappable
{
    [SerializeField] private SpriteShapeRenderer spriteShapeRenderer;
    public void Swap(Vector3 position)
    {
       Debug.Log("test");
       transform.position = position;
       if (spriteShapeRenderer != null)
       {
           spriteShapeRenderer.enabled = false;
       }
    }

    public void LockItIn()
    {
        if (spriteShapeRenderer != null)
        {
            spriteShapeRenderer.enabled = true;
        }
    }

    private void Awake()
    {
        
    }
}
