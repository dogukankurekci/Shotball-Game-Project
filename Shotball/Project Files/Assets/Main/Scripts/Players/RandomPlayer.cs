using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlayer : MonoBehaviour
{
    public BallMovement ballMovement;

    private Animator animator;
    public Color startColor;
    public Color endColor;

    private List<int> _validNumbers;
    public int number;
    private void Awake()
    {
        _validNumbers = new List<int>();
        for (int i = 0; i < 4; i++)
            _validNumbers.Add(i);
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ballMovement = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallMovement>();

        if (ballMovement.isMoving == false && ballMovement.canRandom)
        {
            switch (number)
            {
                case 0:
                    ballMovement.movingDir = Direction.Up;
                    ballMovement.rb.constraints = RigidbodyConstraints.FreezePositionX;
                    break;
                case 1:
                    ballMovement.movingDir = Direction.Down;
                    ballMovement.rb.constraints = RigidbodyConstraints.FreezePositionX;
                    break;
                case 2:
                    ballMovement.movingDir = Direction.Left;
                    ballMovement.rb.constraints = RigidbodyConstraints.FreezePositionZ;
                    break;
                case 3:
                    ballMovement.movingDir = Direction.Right;
                    ballMovement.rb.constraints = RigidbodyConstraints.FreezePositionZ;
                    break;
            }
        }
     
        Debug.Log(number);
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


            ballMovement.isMoving = false;
            ballMovement.canRandom = true;

            ballMovement.movingDir = Direction.Constant;

            other.transform.position = transform.GetChild(0).position;

            transform.GetChild(3).GetComponent<Renderer>().material.color = startColor;

            if (_validNumbers.Count == 0)
            {
                for (int i = 0; i < 4; i++)
                    _validNumbers.Add(i);
            }
            else
                number = GetRandomNumber();

        }
    }


    private int GetRandomNumber()
    {
        var nextIndex = Random.Range(0, _validNumbers.Count - 1);
        var result = _validNumbers[nextIndex];
        _validNumbers.RemoveAt(nextIndex);
        return result;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            animator.SetTrigger("Pass");

            ballMovement.canRandom = false;

            transform.GetChild(3).GetComponent<Renderer>().material.color = endColor;
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
        }
    }
}
