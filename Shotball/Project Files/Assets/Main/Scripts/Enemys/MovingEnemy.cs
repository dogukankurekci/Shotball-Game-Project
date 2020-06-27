using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MovingEnemy : MonoBehaviour
{
    private Animator anim;
    public Transform pointA, pointB;
    Vector3 pos;

    [SerializeField]bool moveA = true;
    [SerializeField]bool moveB;
    public float speed;
    bool stop;
    private void Start()
    {
        transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed);
        anim = GetComponent<Animator>();
        anim.SetBool("SidestepLeft", true);
    }
    private void FixedUpdate()
    {
        if (!stop)
        {
            if (moveA)
            {
                transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed);
            }
            else if (moveB)
                transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed);
        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PointA")
        {
            moveA = false;
            moveB = true;
            anim.SetBool("SidestepRight", true);
            anim.SetBool("SidestepLeft", false);
        }
        else if (other.gameObject.tag == "PointB")
        {
            moveA = true;
            moveB = false;
            anim.SetBool("SidestepRight", false);
            anim.SetBool("SidestepLeft", true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            speed = 0;
            anim.SetBool("Dance", true);
            stop = true;
            StartCoroutine(failed_delay());
        }
    }

    IEnumerator failed_delay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
