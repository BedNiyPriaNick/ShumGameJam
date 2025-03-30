using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float speed;

    private GameObject playerObject;

    private bool canSeePlayer;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (canSeePlayer)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, playerObject.transform.position - transform.position);
            if (Vector2.Distance(playerObject.transform.position, transform.position) >= 0.2)
            {
                anim.SetBool("Walking", true);
                transform.position = Vector2.MoveTowards(transform.position, playerObject.transform.position, speed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("Walking", false);
                anim.SetTrigger("Attack");
            }
        }
        if (Vector2.Distance (transform.position, playerObject.transform.position) <= 4.75f)
        {
            canSeePlayer = true;
        }
        else
        {
            anim.SetBool("Walking", false);
            canSeePlayer = false;
        }
    }

    public void Attack()
    {
        if (Vector2.Distance(playerObject.transform.position, transform.position) <= 1)
            playerObject.SetActive(false);
    }
}
