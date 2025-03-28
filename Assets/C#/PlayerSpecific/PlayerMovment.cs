using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float damage;
    [SerializeField] private float fistSpeed;

    [SerializeField] private float punchStartCooldown;
    private float punchCooldown;
    private bool punch = false;

    [SerializeField] private float somersaultForce;
    [SerializeField] private float somersaultInvinsibility;
    [SerializeField] private float somersaultStartCooldown;
    private float somersaultCooldown;
    private bool somersault = false;

    private Rigidbody2D rb;
    private Animator anim;

    private Camera main;

    private Vector2 moveVector;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        main = Camera.main;
        punchCooldown = punchStartCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (punchCooldown > 0) { punchCooldown -= Time.deltaTime; }
        while (somersault == true)
        {
            transform.GetComponent<CapsuleCollider2D>().enabled = false;
            punch = false;
        }
        Movement();
        Punching();
        Somersault();
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
        rb.velocity = new Vector2(moveVector.x * speed, moveVector.y * speed);
    }

    void Punching()
    {
        if (Input.GetMouseButtonDown(0) && punchCooldown <= 0)
        {
            punch = true;
            Invoke("SetPunch", 0.2f);
            punchCooldown = punchStartCooldown;
            Debug.Log("Перезарядка кулаків");
            //anim.SetTrigger("Punch");
        }
    }

    private void SetPunch()
    {
        punch = false;
    }
    private void SetSomersault()
    {
        somersault = false;
    }

    void Somersault()
    {
        if (Input.GetKeyDown(KeyCode.Space) && somersaultCooldown <= 0)
        {
            somersault = true;
            //rb.AddForce(Vector2.up * somersaultForce, ForceMode2D.Impulse); ВИРІШИ ПРОБЛЕМУ З КУВЕРКОМ, БО В МЕНЕ ВИЛЕТІВ ЮНІТІ ІЗ ЗА ЦЬОГО ШМАТКА КОДА
            Invoke("SetSomersault", somersaultInvinsibility);
            somersaultCooldown = somersaultStartCooldown;
            Debug.Log("Перезарядка кувирка");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && punch == true)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage();
            Debug.Log("Враг отримав удар");
        }
    }
}
