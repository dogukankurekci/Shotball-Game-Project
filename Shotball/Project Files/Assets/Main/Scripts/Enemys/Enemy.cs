using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public BallMovement ballMovement;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ballMovement = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallMovement>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
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
        }

        animator.SetTrigger("Dance");
        StartCoroutine(failed_delay());
    }
    IEnumerator failed_delay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
