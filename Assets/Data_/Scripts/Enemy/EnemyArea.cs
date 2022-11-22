using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyArea : log
{
    public Collider2D boundary;

    public void OnEnable()
    {
        currentState = EnemyState.idle;
    }
    public override void checkDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius 
            && Vector3.Distance(target.position, transform.position) > attackRadius 
            && boundary.bounds.Contains(target.transform.position))
        {
            if (currentState == EnemyState.idle 
                || currentState == EnemyState.walk 
                && currentState != EnemyState.stagger)
            {

                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);

                ChangeState(EnemyState.walk);
                anim.SetBool("wakeUp", true)
;
            }

        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius 
                || !boundary.bounds.Contains(target.transform.position))
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, homePos, moveSpeed * Time.deltaTime);

            changeAnim(temp - transform.position);
            myRigidbody.MovePosition(temp);
            ChangeState(EnemyState.walk);
            if(transform.position == homePos)
            {
                anim.SetBool("wakeUp", false);
            }
            

        }
    }
}
