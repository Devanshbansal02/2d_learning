using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;

public class Walking : MonoBehaviour
{
    public float speed = 10f;
    private bool isMoving;
    private Vector2 control;
    private Animator animator;
    public LayerMask solidObjectsLayer;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    private void Update()
    {
        if(!isMoving){
        control.x=Input.GetAxisRaw("Horizontal");
        control.y=Input.GetAxisRaw("Vertical");
        if(control.x!=0) control.y=0;

        if(control!=Vector2.zero){
            animator.SetFloat("moveX", control.x);
            animator.SetFloat("moveY", control.y);
            var targetPos = transform.position;
            targetPos.x += control.x;
            targetPos.y += control.y;
            if(isWalkable(targetPos))
            StartCoroutine(Move(targetPos));

        }
        }
        animator.SetBool("IsWalking", isMoving);
    }
    IEnumerator Move(Vector3 targetPos){
        isMoving = true;
        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    private bool isWalkable(Vector3 targetPos){
        if(Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null)
        {
            return false;
        }
        return true;
    }
}