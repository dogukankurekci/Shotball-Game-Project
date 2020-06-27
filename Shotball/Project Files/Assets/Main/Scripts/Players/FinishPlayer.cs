using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPlayer : MonoBehaviour
{
    public BallMovement ballMovement;
    SmoothCamera smoothCamera;

    private Animator animator;
    public Color startColor;
    public Color endColor;

    public Direction finishDir;
    bool U, D, L, R;

    [SerializeField] float minDistance = 15F;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ballMovement = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallMovement>();
        smoothCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothCamera>();
    }

    private void FixedUpdate()
    {
        switch (finishDir)
        {
            case Direction.Constant:
                U = false; D = false; L = false; R = false;
                break;

            case Direction.Up:
                U = true; D = false; L = false; R = false;
                break;

            case Direction.Down:
                U = false; D = true; L = false; R = false;
                break;

            case Direction.Left:
                U = false; D = false; L = true; R = false;
                break;

            case Direction.Right:
                U = false; D = false; L = false; R = true;
                break;
        }
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            animator.SetBool("Idle", false);
            animator.SetTrigger("Pass");

            ballMovement.isMoving = false;

            transform.GetChild(3).GetComponent<Renderer>().material.color = endColor;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            #region Finish yönü için gereken islemler
            if (ballMovement.turnUp)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                ballMovement.turnUp = false;

                if (U)
                {
                    ballMovement.trails[0].SetActive(false);
                    ballMovement.trails[1].SetActive(true);

                    animator.SetTrigger("FinishGoal");

                    Time.timeScale = 0.6f;
                    smoothCamera.smoothSpeed = 1f;
                    smoothCamera.transform.rotation = Quaternion.Euler(30, 0, 0);
                    smoothCamera.offset = new Vector3(0, 20, -20);
                }
            }

            else if (ballMovement.turnDown)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                ballMovement.turnDown = false;

                if (D)
                {
                    ballMovement.trails[0].SetActive(false);
                    ballMovement.trails[1].SetActive(true);

                    animator.SetTrigger("FinishGoal");

                    Time.timeScale = 0.6f;
                    smoothCamera.smoothSpeed = 1f;
                    smoothCamera.transform.rotation = Quaternion.Euler(30, 180, 0);
                    smoothCamera.offset = new Vector3(0, 20, 20);
                }
            }
            else if (ballMovement.turnLeft)
            {
                transform.rotation = Quaternion.Euler(0, 270, 0);
                ballMovement.turnLeft = false;

                if (L)
                {
                    ballMovement.trails[0].SetActive(false);
                    ballMovement.trails[1].SetActive(true);

                    animator.SetTrigger("FinishGoal");

                    Time.timeScale = 0.6f;
                    smoothCamera.smoothSpeed = 1f;
                    smoothCamera.transform.rotation = Quaternion.Euler(30, -90, 0);
                    smoothCamera.offset = new Vector3(20, 20, 0);


                }
            }
            else if (ballMovement.turnRight)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                ballMovement.turnRight = false;

                if (R)
                {
                    ballMovement.trails[0].SetActive(false);
                    ballMovement.trails[1].SetActive(true);

                    animator.SetTrigger("FinishGoal");

                    Time.timeScale = 0.6f;
                    smoothCamera.smoothSpeed = 1f;
                    smoothCamera.transform.rotation = Quaternion.Euler(30, 90, 0);
                    smoothCamera.offset = new Vector3(-20, 20, 0);
                }
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
