using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 1f;
    private bool canMove = false;


    private void Update()
    {
        if (canMove)
        {
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(transform.localScale.x, 0) * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canMove = collision.gameObject.tag == "Ground";
    }
}
