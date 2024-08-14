using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Color openedGateColor;
    private Color initialGateColor;
    private SpriteRenderer spriteRenderer;
    private bool portalOpened = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialGateColor = spriteRenderer.color;

    }
    private void OnEnable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.onPortalOpened += OpenPortal;
            GameEventsManager.instance.onPortalClosed += ClosePortal;
        }
    }
    private void OnDisable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.onPortalOpened -= OpenPortal;
            GameEventsManager.instance.onPortalClosed -= ClosePortal;
        }
    }
    private void OpenPortal()
    {
        portalOpened = true;
        spriteRenderer.color = openedGateColor;
        //Add outline
    }
    private void ClosePortal()
    {
        portalOpened = false;
        spriteRenderer.color = initialGateColor;
        //Remove outline
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (portalOpened) GameEventsManager.instance.GoToNextLevel();
        }
    }
}
