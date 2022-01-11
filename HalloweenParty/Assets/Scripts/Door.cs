using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;         //Used to update the sprite layer order 
    private Animator _animator;                     //Used to animate the door 
    private bool _isDoorOpen;                       //Tells us if the door finished it's animation 

    // Start is called before the first frame update
    private void Start()
    {
        //Grabs the SpriteRenderer components from the child GameObjects Door_Sprite 
        _spriteRenderer = transform.Find("Door_Sprite").gameObject.GetComponent<SpriteRenderer>();
        
        //Get the Animator and position directly from it's own GameObject  
        _animator = GetComponent<Animator>();
    }

    //Starts the door opening animations and updates the layer order 
    public void DoorOpen()
    {
        _animator.Play($"DoorOpen");
        StartCoroutine(WaitForAnimationToFinish(1, true));
    }
    
    //Starts the door closing animations and updates the layer order 
    public void CloseDoor()
    {
        _spriteRenderer.sortingOrder = 5;
        _animator.Play($"DoorClose");
        StartCoroutine(WaitForAnimationToFinish(5, false));
    }

    //Tells us if the door is opened or not
    public bool GetIsDoorOpen()
    {
        return _isDoorOpen;
    }
    
    //Waits the 1 second for the animation to complete to update the order and door state 
    private IEnumerator WaitForAnimationToFinish(int layerPosition, bool isDoorOpen)
    {
        yield return new WaitForSeconds(1f);
        _spriteRenderer.sortingOrder = layerPosition;
        _isDoorOpen = isDoorOpen;
    }
    
}
