using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Components
    private Rigidbody2D rb;

    //Player
    [SerializeField] private float walkspeed = 35f;
    [SerializeField] private float speedLimiter = 0.7f;
    [SerializeField] private float swingingLimiter = 10f;
    [SerializeField] private float swingingVelocity = 0.05f;
    private float swinging = 0.6f;
    private bool swingingFlag = false;
    private float inputHorizontal;
    private float inputVertical;

    // Start is called before the first frame update
    void Start(){
        rb = gameObject.GetComponent<Rigidbody2D>();
        Debug.Log("Player.Start");
    }

    // Update is called once per frame
    void Update(){
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() {
        if (inputHorizontal != 0 || inputVertical != 0) {

            if (inputHorizontal != 0 && inputVertical != 0) {
                inputHorizontal *= speedLimiter;
                inputVertical *= speedLimiter;
            }

            //Swinging movement
            swingingMovement(swingingLimiter, swingingVelocity * (walkspeed/2));
            

            rb.velocity = new Vector2(inputHorizontal * walkspeed, inputVertical * walkspeed);
        } else {
            rb.velocity = new Vector2(0, 0);
            swinging = 0;
            transform.rotation = Quaternion.identity;
        }
    }

    private void swingingMovement(float swingingLimiter, float swingingVelocity) {
        if (swingingFlag) {
            swinging -= swingingVelocity;
        } else swinging += swingingVelocity;
        transform.rotation = Quaternion.Euler(0, 0, swinging);

        if (swinging > swingingLimiter) {
            swingingFlag = true;
        } else if (swinging < -swingingLimiter) {
            swingingFlag = false;
        }
    }
}
