using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private float Jumpforce;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.relativeVelocity.y <= 0) && collision.transform.CompareTag(Cache.keyPlayer))
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb)
            {
                Vector2 velecity = rb.velocity;
                velecity.y = Jumpforce;
                rb.velocity = velecity;
            }
        }

    }
}
