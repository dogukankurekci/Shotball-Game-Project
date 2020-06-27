using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Direction
{
    Constant, Up, Down, Right, Left
}

public class BallMovement : MonoBehaviour
{
    Swipe swipeContoller;

    [HideInInspector]
    public Rigidbody rb;

    public float speed;

    [HideInInspector]
    public bool isMoving, canTargetPoint, canRandom;

    [HideInInspector]
    public bool turnUp, turnDown, turnRight, turnLeft = false;

    public Direction movingDir;

    [Header("Trail Particles")]
    public GameObject[] trails;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        swipeContoller = GetComponent<Swipe>();
        isMoving = true;
    }

    private void Update()
    {

        if (isMoving)
        {
            if (swipeContoller.SwipeUp )
            {
                canTargetPoint = true;
                rb.constraints = RigidbodyConstraints.FreezePositionX;
                movingDir = Direction.Up;
            }

            if (swipeContoller.SwipeDown )
            {
                canTargetPoint = true;
                rb.constraints = RigidbodyConstraints.FreezePositionX;
                movingDir = Direction.Down;
            }

            if (swipeContoller.SwipeRight)
            {
                canTargetPoint = true;
                rb.constraints = RigidbodyConstraints.FreezePositionZ;
                movingDir = Direction.Right;
            }

            if (swipeContoller.SwipeLeft)
            {
                canTargetPoint = true;
                rb.constraints = RigidbodyConstraints.FreezePositionZ;
                movingDir = Direction.Left;
            }
        }
        else
        {
            if (isMoving)
            {
                if (swipeContoller.SwipeUp)
                {
                    canTargetPoint = true;
                    rb.constraints = RigidbodyConstraints.FreezePositionX;
                    movingDir = Direction.Up;
                }

                if (swipeContoller.SwipeDown)
                {
                    canTargetPoint = true;
                    rb.constraints = RigidbodyConstraints.FreezePositionX;
                    movingDir = Direction.Down;
                }

                if (swipeContoller.SwipeRight)
                {
                    canTargetPoint = true;
                    rb.constraints = RigidbodyConstraints.FreezePositionZ;
                    movingDir = Direction.Right;
                }

                if (swipeContoller.SwipeLeft)
                {
                    canTargetPoint = true;
                    rb.constraints = RigidbodyConstraints.FreezePositionZ;
                    movingDir = Direction.Left;
                }
            }
        }
        
       
    }

    private void FixedUpdate()
    {
        switch (movingDir)
        {
            case Direction.Constant:
                rb.velocity = Vector3.zero;
                break;

            case Direction.Up:
                turnUp = true;
                rb.velocity = new Vector3(0, 0, speed * Time.fixedDeltaTime);
                break;

            case Direction.Down:
                turnDown = true;
                rb.velocity = new Vector3(0, 0, -speed * Time.fixedDeltaTime);
                break;

            case Direction.Left:
                turnLeft = true;
                rb.velocity = new Vector3(-speed * Time.fixedDeltaTime, 0, 0);
                break;

            case Direction.Right:
                turnRight = true;
                rb.velocity = new Vector3(speed * Time.fixedDeltaTime, 0, 0);
                break;
        }
    }

   
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Block")
        {
            Time.timeScale = 1f;
            RunningEnemy.fail = true;
            isMoving = false;
            movingDir = Direction.Constant;
            StartCoroutine(failed_delay());
        }
    }

    IEnumerator failed_delay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
