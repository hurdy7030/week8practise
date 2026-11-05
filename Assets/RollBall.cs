using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RollBall : MonoBehaviour

{
    
    Rigidbody rb;
    public float Force=100f;
    InputAction bowlbutt;

    private bool holdingButt;
    private bool roll;

    public LayerMask personaLayer;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.AddForce(transform.forward * Force);
        bowlbutt = InputSystem.actions.FindAction("Bowl");
    }

    // Update is called once per frame
    void Update()
    {
        if (bowlbutt.WasPressedThisFrame())
        {
            holdingButt = true;
        }else if (bowlbutt.WasReleasedThisFrame())
        {
            holdingButt = false;
            roll = true;
        }
    }

    void FixedUpdate()
    {
        if (holdingButt && rb.linearVelocity == Vector3.zero)
        {
         //find the direction the mouse is pointing towards   
         Debug.DrawRay(transform.position, FindDirection()*1000f, Color.red);
        }

        if (roll)
        {
            
            StartRoll();
            //roll the ball based on the mouse's direction
        }
    }

    void StartRoll()
    {
        if (Physics.Raycast(transform.position, FindDirection(), 1000f,personaLayer) == false)
        {
            rb.isKinematic = false;
            rb.AddForce(Force * transform.TransformDirection(FindDirection()), ForceMode.Impulse);
            roll = false;
        }
        Vector3 pointTo = FindDirection();
    }
    
    Vector3 FindDirection()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = 10f;
        Vector3 dir = Camera.main.ScreenToWorldPoint(mousePosition);
        dir.z = 10f;
        return dir.normalized;
    }
}
