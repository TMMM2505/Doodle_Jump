using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using System;

public class Hat : GameUnit
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public float changeInterval = 0.1f;
    public Action<Hat> RemoveFromList;

    private GameObject DespawnLineBot;
    private int currentSpriteIndex;
    private bool isFly = false;
    void Start()
    {
        currentSpriteIndex = 0;
    }
    void Update()
    {
        if (isFly)
        {
            StartCoroutine(ChangeSprite());
        }
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
    IEnumerator ChangeSprite()
    {
        while (true)
        {
            spriteRenderer.sprite = sprites[currentSpriteIndex];
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
            if (currentSpriteIndex == sprites.Length - 1)
            {
                break;
            }
            yield return new WaitForSeconds(changeInterval);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(Cache.keyPlayer))
        {
            Player player = collision.GetComponent<Player>();
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            player.DestroyHat();
            player.SetFlying(true);
            TF.SetParent(player.transform);
            TF.position = player.transform.position + new Vector3(0, 0.2f);
            isFly = true;
            if(rb)
            {
                Vector2 velecity = rb.velocity;
                velecity.y = 8f;
                rb.velocity = velecity;
            }
        }
    }
    public IEnumerator Release(float time)
    {
        isFly = false;
        TF.DOJump(new Vector2(TF.position.x - 1f, TF.position.y), 3f, 1, 1f);
        yield return new WaitForSeconds(time);
        if(this)
        {
            Destroy(gameObject);
        }

    }
    public void ResetHat() 
    {
        isFly = false;
    }
    public void SetIsFly(bool isFly)
    {
        this.isFly = isFly;
    }
}
