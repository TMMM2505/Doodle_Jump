using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float speed = 20;

    void Start()
    {
        target = FindObjectOfType<Player>().transform;
    }

    void LateUpdate()
    {
        if(target.position.y > transform.position.y)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,target.position.y + offset.y, -1), speed);
        }
    }
    public void OnInit(float y)
    {
        transform.position = new Vector3(0, y, -10);
    }
}
