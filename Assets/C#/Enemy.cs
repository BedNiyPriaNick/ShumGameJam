using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int speed;

    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
        if (Vector2.Distance(player.transform.position, transform.position) >= 0.5)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    public void TakeDamage()
    {
        Debug.Log("Врага ліквідовано");
        Destroy(this.gameObject);
    }
}
