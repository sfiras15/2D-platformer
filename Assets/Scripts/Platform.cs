using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private int requiredBoxes = 2;
    private int initialRequiredBoxesNumber;
    private void Awake()
    {
        initialRequiredBoxesNumber = requiredBoxes;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ISwappable iSwappable))
        {
            iSwappable.FreezePosition();

            requiredBoxes--;
            if (requiredBoxes <= 0)
            {
                //Add lock the position
                requiredBoxes = 0;
                Debug.Log("portal opened");
                // Open Portal;
                GameEventsManager.instance.OpenPortal();
                
            }
            
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ISwappable iSwappable))
        {
            iSwappable.UnFreezePosition();
            requiredBoxes++;
            Debug.Log("portal closed");
            if (requiredBoxes > initialRequiredBoxesNumber) requiredBoxes = initialRequiredBoxesNumber;
            if (requiredBoxes > 0) GameEventsManager.instance.ClosePortal();
        }
    }
}
