using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="ScriptableObjects", menuName ="ScriptableObjects/Abilities/LashingAbility")]
public class LashingAbility_SO : ScriptableObject
{
    public bool abilityActive = false;

    public bool abilityReleased = false;
    public Vector2 gravityDirection = Vector2.zero;

    public event Action onAbilityPressed;

    public event Action onAbilityReleased;

    public float timeScale = 0f;
    public void ActivateAbility()
    {
        Time.timeScale = this.timeScale;
        if (onAbilityPressed != null)
        {
            onAbilityPressed.Invoke();
            
        }
    }
    public void DeactivateAbility()
    {
        Time.timeScale = 1f;
        if (onAbilityReleased != null)
        {
            onAbilityReleased.Invoke();
        }
        Debug.Log("EventFired : " + Time.time);
    }
}
