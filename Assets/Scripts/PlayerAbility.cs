using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] private KeyCode lashingAbilityButton;
    [SerializeField] private LashingAbility_SO lashingAbility_SO;

    [SerializeField] private KeyCode swapAbilityButton;
    [SerializeField] private SwapAbility_SO swapAbility_SO;

    private Collider2D[] swapTargets;

    private Collider2D closestTarget;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //First ability
        if (lashingAbility_SO != null)
        {
            if (Input.GetKey(lashingAbilityButton))
            {
                lashingAbility_SO.abilityActive = true;
                //ability_SO.abilityReleased = false;
                lashingAbility_SO.ActivateAbility();
            }
            if (Input.GetKeyUp(lashingAbilityButton))
            {
                lashingAbility_SO.abilityActive = false;
                //ability_SO.abilityReleased = true;
                lashingAbility_SO.DeactivateAbility();
            }
        }

        //Second Ability
        if (swapAbility_SO != null)
        {
            if (Input.GetKeyDown(swapAbilityButton) &&  swapAbility_SO.swapTarget == null)
            {
                ScanTarget();
            }
            else if (Input.GetKeyDown(swapAbilityButton) && swapAbility_SO.swapTarget != null)
            {
                SwapPosition();
            }
        }
        
    }

    private void ScanTarget()
    {
        swapTargets = Physics2D.OverlapCircleAll(transform.position, swapAbility_SO.targetDetectionRadius,swapAbility_SO.whatIsSwapTarget);
        if (swapTargets.Length > 0)
        {
            closestTarget = GetClosestTarget(swapTargets);

            if (closestTarget != null)
            {
                foreach (var target in swapTargets)
                {
                    if (target.TryGetComponent(out ISwappable iSwappable))
                    {
                        //Bounds interactableBounds = iSwappable.bounds;
                        //Vector3 closestPoint = interactableBounds.ClosestPoint(transform.position);
                        if (target == closestTarget)
                        {
                            // Show the UI for the closest interactable
                            //iSwappable.ShowUI();
                            iSwappable.LockTarget();
                            swapAbility_SO.swapTarget = target.gameObject;
                        }
                    }
                }
            }
        }
    }

    private void SwapPosition()
    {
        Vector3 auxPosition = swapAbility_SO.swapTarget.transform.position; 
        if (swapAbility_SO.swapTarget.TryGetComponent(out ISwappable iSwappable))
        {
            iSwappable.Swap(transform.position);
        }
        transform.position = auxPosition;
        swapAbility_SO.swapTarget = null;

    }


    private Collider2D GetClosestTarget(Collider2D[] colliders)
    {
        Collider2D closestCollider = colliders[0];
        float closestDistance = Vector3.Distance(transform.position, closestCollider.transform.position);

        for (int i = 1; i < colliders.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, colliders[i].transform.position);
            if (distance <= closestDistance)
            {
                closestCollider = colliders[i];
                closestDistance = distance;

            }
        }
        return closestCollider;
    }

    void OnDrawGizmos()
    {
        // Set the gizmo color (you can choose any color you like)
        Gizmos.color = Color.yellow;

        // Draw a wireframe sphere (circle in 2D) at the character's position with the detection radius
        Gizmos.DrawWireSphere(transform.position, swapAbility_SO.targetDetectionRadius);
    }
}
