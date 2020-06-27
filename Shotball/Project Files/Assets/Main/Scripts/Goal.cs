using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public BallMovement ballMovement;

    public GameObject confetti;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Time.timeScale = 1f;
            RunningEnemy.fail = true;
            ballMovement.movingDir = Direction.Constant;
            confetti.SetActive(true);

            StartCoroutine(finish_delay());
        }
    }

    IEnumerator finish_delay()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
