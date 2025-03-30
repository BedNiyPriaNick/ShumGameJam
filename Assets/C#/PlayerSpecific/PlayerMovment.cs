using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private float startSpeed;
    private float currentSpeed;

    [SerializeField] private float punchStartCooldown;
    private float punchCooldown;
    private bool punch = false;

    [SerializeField] private float invisibilityStartCooldown;
    private float invisibilityCooldown;

    [SerializeField] private GameObject deathScreen;

    [SerializeField] private Animator fistAnim;

    private Rigidbody2D rb;
    private Animator anim;

    private Camera main;

    private Vector2 moveVector;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = startSpeed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        main = Camera.main;
        punchCooldown = punchStartCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (punchCooldown > 0) { punchCooldown -= Time.deltaTime; }
        if (invisibilityCooldown > 0) { invisibilityCooldown -= Time.deltaTime; }
        Movement();
        Punching();
        Invisibility();
        Vector3 mouse = main.ScreenToWorldPoint(Input.mousePosition);
        //transform.Rotate(new Vector3(mouse.z, 0f, 0f));
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(0f, 0f, mouse.z, 0f), 0f);
        //transform.LookAt(new Vector3(0f, 0f, main.ScreenToWorldPoint(Input.mousePosition).z), transform.right);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
    }

    void Movement()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");
        if (moveVector.x > 0 || moveVector.x < 0 || moveVector.y > 0 || moveVector.y < 0)
        {
            anim.SetFloat("moveVector", 1);
        }
        else
        {
            anim.SetFloat("moveVector", 0);
        }
        rb.velocity = new Vector2(moveVector.x * currentSpeed, moveVector.y * currentSpeed);
    }

    void Punching()
    {
        if (Input.GetMouseButtonDown(0) && punchCooldown <= 0)
        {
            punch = true;
            Invoke("SetPunch", 0.2f);
            fistAnim.SetTrigger("Punch");
            punchCooldown = punchStartCooldown;
            Debug.Log("Перезарядка кулаків");
            //anim.SetTrigger("Punch");
        }
    }

    private void SetPunch()
    {
        punch = false;
    }
    private void SetInvisibility()
    {
        anim.SetBool("IsInvisible", false);
        invisibilityCooldown = invisibilityStartCooldown;
        currentSpeed = startSpeed;
        Debug.Log("Перезарядка невидимість");
    }

    void Invisibility()
    {
        if (Input.GetKeyDown(KeyCode.Space) && invisibilityCooldown <= 0)
        {
            punchCooldown = punchStartCooldown;
            currentSpeed = startSpeed * 0.1f;
            anim.SetBool("IsInvisible", true);
            Invoke("SetInvisibility", 10f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && punch == true)
        {
            Destroy(collision.gameObject);
            Debug.Log("Врага ліквідовано");
        }
    }

    private void OnDisable()
    {
        deathScreen.SetActive(true);
    }
}
