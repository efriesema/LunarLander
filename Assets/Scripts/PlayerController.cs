using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Lander properties
    public float sideThrust = 5.0f;
    public float upThrust = 10.0f;
    public float fuel = 10000f;
    public float playerAltitude;
    public Vector3 playerVelocity;
    public float fuelConsumptionRate = 2f;
    public float safeVelocity = 8.0f;


    //Controller properties
    private Rigidbody playerRb;  
    public bool outOfFuel = false;
    public bool didCrash;
    public bool hitTarget = false;
    public bool gameOver = false;

    public int score = 0;


    //Environmental conditions
    public float gravityModifier = 0.166667f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {


        //Update player altitude and velocity variable 
        playerAltitude = transform.position.y;
        playerVelocity = playerRb.velocity;


        //Take input control variables  
        // a,w,s,d  -- east,north,west,south side thrusts
        // space -- up thrust
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (!outOfFuel && !gameOver) { 
            playerRb.AddForce(Vector3.forward * sideThrust * verticalInput, ForceMode.Impulse);
            fuel -= Mathf.Abs(horizontalInput) * fuelConsumptionRate;
            playerRb.AddForce(Vector3.right * sideThrust * horizontalInput, ForceMode.Impulse);
            fuel -= Mathf.Abs(verticalInput) * fuelConsumptionRate;

            if (Input.GetKeyDown(KeyCode.Space) && !gameOver) {
                playerRb.AddForce(Vector3.up * upThrust, ForceMode.Impulse);
                fuel -= upThrust*fuelConsumptionRate;
            }
        }

        //Endgame conditions
        // Lander runs out of fuel
        //if (fuel <= 0 && !outOfFuel)
        //{
        //    outOfFuel = true;
        //    Debug.Log("You've run out of fuel and are going to crash! ");
        //    score -= 700;
        //}
        

        //Final Score Calculation
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Pad"))
        {
            gameOver = true;
            hitTarget = true;
            if(playerVelocity.magnitude< safeVelocity)
            {
                
                score += 5000 + (int)(.1 * fuel);
                didCrash = false;
                Debug.Log(string.Format("You landed on the pad safely with velocity, {0}. Score = {1}.\nGame Over.", playerVelocity.magnitude,score));
            } else
            {             
                score += 200 + (int)(.1*fuel);
                didCrash = true;
                Debug.Log(string.Format("You landed on the pad but at too fast a velocity, {0}. Score = {1}.\nGame Over.", playerVelocity.magnitude,score));
            }
            gameOver = true;
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            if (playerVelocity.magnitude < safeVelocity)
            {
                score += 1000 + (int)(.1 * fuel);
                didCrash = false;
                Debug.Log(string.Format("You missed the pad at velocity,{0}. Score = {1}.\nGame Over.", playerVelocity.magnitude, score));
            }
            else
            {
                score +=  (int)(.1 * fuel);
                didCrash = true;
                Debug.Log(string.Format("You crashed at velocity, {0}. Score = {1}.\nGame Over.", playerVelocity.magnitude,score));
                
            }

            gameOver = true;
        }
        else if (collision.gameObject.CompareTag("Building"))
        {
            if (playerVelocity.magnitude < safeVelocity)
            {
                score += -200 + (int)(.1 * fuel);
                didCrash = false;
                Debug.Log(string.Format("You crashed into a building at velocity,{0}! Score = {1}.\nGame Over.", playerVelocity.magnitude, score));
            }
            else
            {
                score += -200 + (int)(.1 * fuel);
                didCrash = true;
                Debug.Log(string.Format("You crashed into a building at velocity,{0}! Score = {1}.\nGame Over.", playerVelocity.magnitude, score));

            }

            gameOver = true;
        }
    }
}
