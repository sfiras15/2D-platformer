using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuItem : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float gravityIntensity = 2f;
    private Vector2 initialPosition;
    private Vector2 finalPosition;

    private Vector2 gravityDirection= Vector2.zero;
    
    private void Awake()
    {
        initialPosition = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        // If it's the right direction
        if (index == 0)
        {
            finalPosition = new Vector2(initialPosition.x + 12, initialPosition.y);
            gravityDirection = Vector2.right * gravityIntensity;
        }
        // If it's the upper direction
        else if (index == 1)
        {
            finalPosition = new Vector2(initialPosition.x, initialPosition.y + 12);
            gravityDirection = Vector2.up * gravityIntensity;
        }
        // If it's the left direction
        else if (index == 2)
        {
            finalPosition = new Vector2(initialPosition.x - 12, initialPosition.y);
            gravityDirection = Vector2.left * gravityIntensity;
        }
        // If it's the left direction
        else 
        { 
            finalPosition = new Vector2(initialPosition.x, initialPosition.y - 12); 
            gravityDirection= Vector2.down * gravityIntensity;
        }
        
    }

    public Vector2 GravityDirection
    {
        get { return gravityDirection; }   
    }
    public void Select()
    {
       // selecting = true;
        StartCoroutine(MoveItem(initialPosition,finalPosition));
        //selecting = false;
        //Debug.Log("selecting : " + index);
    }

    public void Deselect()
    {
       
        //deselecting = true;
        StartCoroutine(MoveItem(finalPosition, initialPosition));
        //deselecting = false;
        //Debug.Log("deselecting : " + index);
    }

    public IEnumerator MoveItem(Vector2 a , Vector2 b)
    {
        float currentTime = 0;
        //float duration = 1f;
        while (currentTime < duration)
        {
            currentTime += Time.unscaledDeltaTime;
            transform.position = Vector3.Lerp(a, b, currentTime / duration);
            yield return null;
        }
        transform.position = b;
    }
}
