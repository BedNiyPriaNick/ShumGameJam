using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private Camera main;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = main.ScreenToWorldPoint(Input.mousePosition);
        //transform.Rotate(new Vector3(mouse.z, 0f, 0f));
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(0f, 0f, mouse.z, 0f), 0f);
        //transform.LookAt(new Vector3(0f, 0f, main.ScreenToWorldPoint(Input.mousePosition).z), transform.right);

    }
}
