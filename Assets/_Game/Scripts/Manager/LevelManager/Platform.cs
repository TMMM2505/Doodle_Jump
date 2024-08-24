using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Platform : GameUnit
{
    [SerializeField] private float Jumpforce;

    public Action<Platform> RemoveFromList;

    private PlatformType type;
    private GameObject DespawnLineBot;


    // for dynamic
    private Vector2 positionA;
    private Vector2 positionB;
    private Vector2 targetPosition = Vector2.zero;
    private float moveDuration;

    private void Start()
    {
        //if (type == PlatformType.DynamicPlatform)
        //{
        //    positionA = new Vector2(-2f, TF.position.y);
        //    positionB = new Vector2(2f, TF.position.y);
        //    targetPosition = positionA;
        //    moveDuration = UnityEngine.Random.Range(5, 10);
        //    int r = UnityEngine.Random.Range(0, 2);
        //    if (r == 0)
        //    {
        //        MoveToPos1();
        //    }
        //    else
        //    {
        //        MoveToPos2();
        //    }
        //}
    }
    protected void Update()
    {
        if(TF.position.y <= DespawnLineBot.transform.position.y)
        {
            OnDespawn();
            RemoveFromList?.Invoke(this);
        }
    }
    public void OnInit(GameObject DespawnLineBot, PlatformType type, Vector2 pos, Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
        TF.position = pos;
        this.type = type;
        this.DespawnLineBot = DespawnLineBot;
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.relativeVelocity.y <= 0) && collision.transform.CompareTag(Cache.keyPlayer))
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if(rb)
            {
                Vector2 velecity = rb.velocity;
                velecity.y = Jumpforce;
                rb.velocity = velecity;
            }
        }
        if(type == PlatformType.DissappearPlatform)
        {
            if (collision.relativeVelocity.y < 0)
            {
                OnDespawn();
            }
        }
        if(type == PlatformType.FallingPlatform)
        {
            if (collision.relativeVelocity.y <= 0)
            {
                TF.DOMoveY(TF.position.y - 10f, 5f);
            }
        }
    }
    public void DeleteChild()
    {
        if(GetComponentInChildren<Hat>())
        {
            Destroy(GetComponentInChildren<Hat>().gameObject);
        }
        if(GetComponentInChildren<SpringShoes>())
        {
            Destroy(GetComponentInChildren<SpringShoes>().gameObject);
        }
    }
    public void ResetPlatform()
    {
        type = 0;
        targetPosition = Vector2.zero;
    }
    void MoveToPos1()
    {
        TF.DOMove(positionA, moveDuration).OnComplete(() =>
        {
            MoveToPos2();
        });
    }

    void MoveToPos2()
    {
        TF.DOMove(positionB, moveDuration).OnComplete(() =>
        {
            MoveToPos1();
        });
    }
}
