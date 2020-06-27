using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public BallMovement ballMovement;

    private Animator animator;
    public Color startColor;
    public Color endColor;
    [SerializeField]float minDistance = 30f;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ballMovement = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            #region Topun geldigi yöne dönmesini sağlayan islem  
            if (ballMovement.turnUp)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                ballMovement.turnUp = false;
            }

            else if (ballMovement.turnDown)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                ballMovement.turnDown = false;
            }
            else if (ballMovement.turnLeft)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                ballMovement.turnLeft = false;
            }
            else if (ballMovement.turnRight)
            {
                transform.rotation = Quaternion.Euler(0, 270, 0);
                ballMovement.turnRight = false;
            }
            #endregion

            animator.SetBool("Idle", true);

            ballMovement.isMoving = true;
            ballMovement.movingDir = Direction.Constant;

            other.transform.position = transform.GetChild(0).position;

            transform.GetChild(3).GetComponent<Renderer>().material.color = startColor;
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            #region Topu attıgı yöne dönmesini saglayan islem
            if (ballMovement.turnUp)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                ballMovement.turnUp = false;
            }

            else if (ballMovement.turnDown)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                ballMovement.turnDown = false;
            }
            else if (ballMovement.turnLeft)
            {
                transform.rotation = Quaternion.Euler(0, 270, 0);
                ballMovement.turnLeft = false;
            }
            else if (ballMovement.turnRight)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                ballMovement.turnRight = false;
            }
            #endregion

            if (ballMovement.canTargetPoint)
            {
                other.transform.position = transform.GetChild(0).position;
                ballMovement.canTargetPoint = false;
            }
        }
    }
} 
