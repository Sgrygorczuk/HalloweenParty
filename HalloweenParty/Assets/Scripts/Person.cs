using UnityEngine;

public class Person : MonoBehaviour
{
    
    private SpriteRenderer _bodyRenderer;       //Used to update the body sprite
    private SpriteRenderer _headRenderer;       //Used to update the head sprite 
    private Animator _animator;                 //Used to access the animations 

    public Sprite[] bodies;                     //Reference too all available bodies sprites 
    public Sprite[] heads;                      //Reference too all available head sprites 

    private Vector3 _originalPosition;          //Stores the original position behind the door

    private bool _isWalking;                    //Tells us if the person is walking or standing still 

    // Start is called before the first frame update
    public void Start()
    {
        //Grabs the SpriteRenderer components from the child GameObjects Body_Sprite and Head_Sprite
        _bodyRenderer = transform.Find("Body_Sprite").gameObject.GetComponent<SpriteRenderer>();
        _headRenderer = transform.Find("Head_Sprite").gameObject.GetComponent<SpriteRenderer>();
        
        //Get the Animator and position directly from it's own GameObject  
        _animator = GetComponent<Animator>();
        _originalPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isWalking)
        {
            WalkIn();
        }
    }

    //Used in GameFlow to tell the person that they can start walking 
    public void StartWalking()
    {
        _isWalking = true;
        _animator.Play($"PersonWalking");
    }

    //Tells the GameFlow that the person has walked far enough that the door can be shut 
    public bool IsHalfWay()
    {
        return transform.position.x > 5;
    }

    //Performs the Walking Animation, moves the person in the right, positive x direction. Once player is past 
    //the given distance they go back to original spot and randomizes appearance. 
    private void WalkIn()
    {
        transform.position += Vector3.right / 8f;


        if (!(transform.position.x > 12)) return;
        transform.position = _originalPosition;
        _isWalking = false;
        RandomizePerson();
        _animator.Play($"PersonStill");
    }
    
    //Randomizes the head and body appearance of the person.  
    public void RandomizePerson()
    {
        var headIndex = Random.Range(0, heads.Length);
        var bodyIndex = Random.Range(0, bodies.Length);

        _headRenderer.sprite = heads[headIndex];
        _bodyRenderer.sprite = bodies[bodyIndex];
    }
}
