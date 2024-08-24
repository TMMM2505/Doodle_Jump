using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public SpriteRenderer spRender;
    public Sprite[] listSprite;
    private Sprite curBg;

    public void SetBG(int index)
    {
        curBg = listSprite[index];
        spRender.sprite = curBg;
    }
}
