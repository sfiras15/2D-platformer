using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private PlayerController playerController;
    [SerializeField] private Animator animator;
    private void Awake()
    {
        TryGetComponent<Rigidbody2D>(out rb2D);
        TryGetComponent<PlayerController>(out playerController);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null)
        {
            //Fix player animation when in the air, fix it when we change character with other animations
            if (playerController != null)
            {
                if (playerController.Grounded) animator.SetFloat("Speed", Vector2.SqrMagnitude(rb2D.velocity));
            }
            
        }
    }
}
