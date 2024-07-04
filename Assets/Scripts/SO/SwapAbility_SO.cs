using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/Abilities/SwapAbility")]

public class SwapAbility_SO : ScriptableObject
{
    public GameObject swapTarget = null;

    public float targetDetectionRadius = 0.5f;

    public LayerMask whatIsSwapTarget;

}
