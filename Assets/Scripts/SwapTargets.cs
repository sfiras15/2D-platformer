using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SwapTargets : MonoBehaviour, ISwappable
{
    [SerializeField] private SpriteShapeRenderer spriteShapeRenderer;

    private Rigidbody2D rb2d;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    public void Swap(Vector3 position)
    {
       Debug.Log("test");
       transform.position = position;
       if (spriteShapeRenderer != null)
       {
           spriteShapeRenderer.enabled = false;
       }
    }

    public void LockTarget()
    {
        if (spriteShapeRenderer != null)
        {
            spriteShapeRenderer.enabled = true;
        }
    }

    public void FreezePosition()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void UnFreezePosition()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
