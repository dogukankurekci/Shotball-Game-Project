using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RunningEnemy : MonoBehaviour
{
    GameObject ball;
    Vector3 direction;
    Transform ballPos;
    CharacterController controller;
    Animator anim;

    [SerializeField]
    float moveSpeed = 5f;
    bool runArea;
    public static bool fail;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        anim.SetBool("Dance", false);
    }

    void Update()
    {
        if (ball == null)
        {
            fail = false;
            ball = GameObject.FindGameObjectWithTag("Ball");
            ballPos = ball.transform;
        }
        if (!fail)
        {
            if (runArea)
            {
                direction = ballPos.position - transform.position;
                direction.Normalize();
                Vector3 velocity = direction * moveSpeed;
                direction.y = 0;
                transform.rotation = Quaternion.LookRotation(direction);
                controller.Move(velocity * Time.deltaTime);
            }
        }
        else
        {
            moveSpeed = 0;
            anim.SetBool("Run", false);
            anim.SetBool("Dance", true);
        }
        transform.rotation = Quaternion.LookRotation(direction);


    }

    private void OnTriggerStay(Collider other)
    {
        if (!fail) {
            if (other.gameObject.tag == "Ball")
            {
                anim.SetBool("Run", true);
                runArea = true;
            }
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            anim.SetBool("Run", false);
            runArea = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            moveSpeed = 0;
            anim.SetBool("Run", false);
            anim.SetBool("Dance", true);
            fail = true;
            StartCoroutine(failed_delay());
        }
    }
    IEnumerator failed_delay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
