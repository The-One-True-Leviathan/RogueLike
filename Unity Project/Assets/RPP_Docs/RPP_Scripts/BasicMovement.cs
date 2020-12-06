using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    private bool playerIsMoving;
    private bool goingUp;
    private bool goingDown;
    private bool goingRight;
    private bool goingLeft;
    private float minimalSpeed = 2f;
    private float currentSpeed;
    public float maxSpeed = 10f;
    public float accelerationTime = 0.3f;
    Vector3 up = new Vector3(0f, 1f, 0f);
    Vector3 down = new Vector3(0f, -1f, 0f);
    Vector3 right = new Vector3(1f, 0f, 0f);
    Vector3 left = new Vector3(-1f, 0f, 0f);
    [SerializeField] Transform player;

    // Update is called once per frame
    void FixedUpdate()
    {
        CentralMovement();
        MovementCheck();

        if (playerIsMoving == true)
        {
            StartCoroutine(Accelerate());
            //Debug.Log(currentSpeed);
        }
        else
        {
            currentSpeed = minimalSpeed;
        }
    }

    void CentralMovement()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            player.position += up * Time.deltaTime * currentSpeed;
            goingUp = true;
        }
        else
        {
            goingUp = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            player.position += down * Time.deltaTime * currentSpeed;
            goingDown = true;
        }
        else
        {
            goingDown = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            player.position += right * Time.deltaTime * currentSpeed;
            goingRight = true;
        }
        else
        {
            goingRight = false;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            player.position += left * Time.deltaTime * currentSpeed;
            goingLeft = true;
        }
        else
        {
            goingLeft = false;
        }
    }

    void MovementCheck()
    {
        if (goingUp == false && goingDown == false && goingRight == false && goingLeft == false)
        {
            playerIsMoving = false;
        }
        else
        {
            playerIsMoving = true;
        }
    }

    IEnumerator Accelerate ()
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += maxSpeed/50;
            yield return new WaitForSeconds(accelerationTime/50);
        }
        else if (currentSpeed>=maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
    }
}
