using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWheel : MonoBehaviour
{
    [SerializeField] private Vector2 mousePositionNormalized;
    [SerializeField] private Vector2 mousePosition;
    [SerializeField] private int selection;
    [SerializeField] private int previousSelection;
    [SerializeField] private MenuItem[] directions;
    [SerializeField] private KeyCode selectKey;
    [SerializeField] private KeyCode deselectKey;
    [SerializeField] private LashingAbility_SO ability_SO;

    private void Awake()
    {
        MenuItem[] items = GetComponentsInChildren<MenuItem>();
        directions = items;
    }

    private void OnEnable()
    {
        if (ability_SO != null)
        {
            ability_SO.onAbilityPressed += ActivateCanvas;
            ability_SO.onAbilityReleased += DeactivateCanvas;
        }
    }

    private void OnDisable()
    {
        if (ability_SO != null)
        {
            ability_SO.onAbilityPressed -= ActivateCanvas;
            ability_SO.onAbilityReleased -= DeactivateCanvas;
        }
    }

    public void ActivateCanvas()
    {
        for (int i = 0; i < directions.Length; i++)
        {
            directions[i].gameObject.SetActive(true);
        }
    }

    public void DeactivateCanvas()
    {
        ability_SO.gravityDirection = directions[selection].GravityDirection;
        for (int i = 0; i < directions.Length; i++)
        {
            directions[i].gameObject.SetActive(false);
        }
    }

    void Start()
    {
        for (int i = 0; i < directions.Length; i++)
        {
            directions[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (ability_SO.abilityActive)
        {
            mousePosition = Input.mousePosition;
            mousePositionNormalized = new Vector2(Input.mousePosition.x - Screen.width / 2f, Input.mousePosition.y - Screen.height / 2f);
            float angle = Mathf.Atan2(mousePositionNormalized.y, mousePositionNormalized.x) * Mathf.Rad2Deg;
            angle = (angle + 360) % 360;

            int previous = selection;

            if (angle >= 45 && angle < 135)
            {
                selection = 1;
            }
            else if (angle >= 135 && angle < 225)
            {
                selection = 2;
            }
            else if (angle >= 225 && angle < 315)
            {
                selection = 3;
            }
            else
            {
                selection = 0;
            }

            if (previous != selection)
            {
                previousSelection = previous;
                directions[previousSelection].Deselect();
                directions[selection].Select();
                ability_SO.gravityDirection = directions[selection].GravityDirection;
                ability_SO.ActivateAbility();  // Invoke the ability pressed event to handle immediate response
            }
        }
    }
}
