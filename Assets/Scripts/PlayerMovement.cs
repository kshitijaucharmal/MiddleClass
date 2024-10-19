using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player
    private Vector3 forward, right; // Movement directions
    private Animator animator;
    public GameObject particles;
    List<Collider> ragdollcolliders = new List<Collider>();
    void Awake(){
 //setRagdollColliders();
    }
    void setRagdollColliders(){
        Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();
        foreach(Collider c in colliders){
            if(c.gameObject != this.gameObject){
                c.isTrigger = true;
                ragdollcolliders.Add(c);
                
            }
        }

    }
    public void turnRagdollOn(){
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false; 
        animator.enabled = false;
        foreach(Collider c in ragdollcolliders){
            c.isTrigger = false;
            c.attachedRigidbody.velocity = Vector3.zero;
        }
    }
    void Start()
    {
        // Setting up movement directions according to isometric perspective
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        right = Camera.main.transform.right;
        right.y = 0;
        right = Vector3.Normalize(right);

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Getting input axes for movement
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (direction.magnitude > 0.1f) // Ensure some input is pressed
        {
            Move(direction);
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
            particles.SetActive(false);
        }
    }

    void Move(Vector3 direction)
    {
        // Translating movement direction to isometric space
        Vector3 rightMovement = right * direction.x;
        Vector3 upMovement = forward * direction.z;

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        // Moving the player
        transform.position += heading * moveSpeed * Time.deltaTime;

        // Optionally rotate player to face movement direction
        transform.forward = heading;
        particles.SetActive(true);
    }
}
