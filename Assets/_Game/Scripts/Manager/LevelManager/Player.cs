using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GameUnit
{
    [SerializeField] private GameObject left, up, right;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    private GameObject limitLine;
    private Vector2 lastPosition = Vector2.zero;
    private int score = 0;
    private float horizontal;
    private bool isFlying;
    private bool isAlive;

    public void OnInit(Vector2 pos, GameObject limitLine)
    {
        isAlive = true;
        this.limitLine = limitLine;
        // huong khi moi bat dau
        Left();

        score = 0;

        TF.position = pos;

        isFlying = false;

        tag = "Untagged";

        lastPosition = pos;
    }
    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * speed;
        OnLimit();
        if(transform.position.y > lastPosition.y)
        {
            lastPosition = transform.position;
            score++;
        }
        if (isFlying)
        {
            TF.Translate(Vector2.up * 0.08f);
        }

    }
    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = horizontal * 0.5f;
        if(horizontal > 0) { Right(); }
        else if(horizontal < 0) { Left(); }
        rb.velocity = velocity;
    }
    public void OnLimit()
    {
        if(TF.position.x > 3f)
        {
            TF.position = new Vector2(-3f, TF.position.y);
        }
        else if (TF.position.x < -3f)
        {
            TF.position = new Vector2(3f, TF.position.y);
        }

        if(TF.position.y < limitLine.transform.position.y)
        {
            isAlive = false;
            GameManager.Ins.ChangeState(GameState.EndGame);
        }
    }
    public void Left()
    {
        SetOffAllDirection();
        left.gameObject.SetActive(true);
    }
    public void Up()
    {
        SetOffAllDirection();
        up.gameObject.SetActive(true);
    }
    public void Right()
    {
        SetOffAllDirection();
        right.gameObject.SetActive(true);
    }
    public void SetOffAllDirection()
    {
        left.gameObject.SetActive(false);
        up.gameObject.SetActive(false);
        right.gameObject.SetActive(false);
    }
    public bool SetCamera() => transform.position.y > 0 && rb.velocity.y >= 0;
    public int GetScore() => score;
    public void DestroyHat()
    {
        if(GetComponentInChildren<Hat>())
        {
            Hat[] hat = GetComponentsInChildren<Hat>();
            /*if(hat.Length > 1)
            {
                for(int i = 1; i < hat.Length; i++)
                {
                    Destroy(hat[i].gameObject);
                }
            }
            StopAllCoroutines();
            if (hat.Length == 1)
            {
                StartCoroutine(hat[0].Release(0.5f));
            }*/
            StartCoroutine(hat[0].Release(0.5f));
        }
    }
    public void SetFlying(bool isFlying)
    {
        this.isFlying = isFlying;
        tag = "Untagged";
        //GetComponent<CapsuleCollider2D>().isTrigger = true;
        rb.gravityScale = 0f;
        StartCoroutine(SetOffFlying(2f));
    }
    public IEnumerator SetOffFlying(float time)
    {
        yield return new WaitForSeconds(time);
        isFlying = false;
        tag = "Player";
        //GetComponent<CapsuleCollider2D>().isTrigger = false;
        rb.gravityScale = 1f;
        DestroyHat();
    }
    public bool IsAlive() => isAlive;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag(Cache.keyGoal))
        {
            GameManager.Ins.ChangeState(GameState.EndGame);
        }
    }
    public void SetActive()
    {
        tag = "Player";
    }
    public void SetIsFlying(bool isFlying)
    {
        this.isFlying = isFlying;
        if(GetComponentInChildren<Hat>())
        {
            GetComponentInChildren<Hat>().SetIsFly(isFlying);
        }
    }
}
