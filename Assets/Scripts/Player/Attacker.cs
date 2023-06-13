using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attacker : MonoBehaviour
{

    [SerializeField] Weapon weapon;
    [SerializeField] bool debug;

    [SerializeField] float range;
    [SerializeField, Range(0, 360)] float angle;
    [SerializeField] int damage;

    private Animator anim;
    private Mover mover;

    private float cosResult;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        mover = GetComponent<Mover>();

        //캐싱
        cosResult = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);

    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    private void OnAttack(InputValue value)
    {
        Attack();
    }

    public void StartAttack()
    {
        if (debug) 
        {
            Debug.Log("공격시작");
        }
        mover.enabled = false;
        weapon.GetComponent<Collider>().enabled = false;
    }

    public void EndAttack()
    {
        if (debug)
        {
            Debug.Log("공격끝");
        }
        mover.enabled = true;
        weapon.GetComponent<Collider>().enabled = true;
    }

    public void AttackTiming()
    {
      
        //범위체크
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
          
            Vector3 dirToTarget = (collider.transform.position - transform.position).normalized;
            // transform.forward 보고있는 방향벡터
            // dirToTarget 캐릭터위치에서 타겟의 방향벡터
            // 두개를 내적 (Dot함수)
            // 그결과가 Cos세타 이고 cos세타는 90도 면 0 , 90 보다 작으면 - , 90보다 크면 + 이다.

            // 공격각도 angle
            // 보고있는 벡터에서 타겟의 벡터의 각도는 반으로 봐야한다. 두개의 벡터를 생각하면 당연하다. 1/2 를 곱해준다.
            // 삼각함수는 호도법이기때문에 * Mathf.Deg2Rad 해준다.

            // 정해진 범위에 앞에있는지
            if (Vector3.Dot(transform.forward, dirToTarget) < cosResult)
                continue;

            IHittable hittable = collider.GetComponent<IHittable>();
            hittable?.TakeHit(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!debug)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
        Debug.DrawRay(transform.position, rightDir * range, Color.blue);
        Debug.DrawRay(transform.position, leftDir * range, Color.blue);
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}
