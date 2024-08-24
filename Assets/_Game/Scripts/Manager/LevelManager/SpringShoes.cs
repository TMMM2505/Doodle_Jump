using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringShoes : GameUnit
{
    public Sprite[] sprites;
    public float changeInterval = 0.1f;
    public Action<SpringShoes> RemoveFromList;

    private GameObject DespawnLineBot;
    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSpriteIndex = 0;
    }
    void Update()
    {
        if (TF.position.y <= DespawnLineBot.transform.position.y)
        {
            OnDespawn();
            RemoveFromList?.Invoke(this);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
    public void OnInit(GameObject despawnLine)
    {
        DespawnLineBot = despawnLine;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag(Cache.keyPlayer) && collision.relativeVelocity.y < 0)
        {
            if(gameObject.activeSelf)
            {
                StartCoroutine(ChangeSprite());
            }

            if((collision.relativeVelocity.y <= 0))
            {
                Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
                if (rb)
                {
                    Vector2 velecity = rb.velocity;
                    velecity.y = 20f;
                    rb.velocity = velecity;

                }
            }
        }
    }
    IEnumerator ChangeSprite()
    {
        while (true)
        {
            spriteRenderer.sprite = sprites[currentSpriteIndex];
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
            if(currentSpriteIndex == sprites.Length - 1)
            {
                break;
            }
            yield return new WaitForSeconds(changeInterval);
        }
    }
    public void ResetShoes()
    {
        spriteRenderer.sprite = sprites[1];
    }
}
