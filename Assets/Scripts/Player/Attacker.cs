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

        //ĳ��
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
            Debug.Log("���ݽ���");
        }
        mover.enabled = false;
        weapon.GetComponent<Collider>().enabled = false;
    }

    public void EndAttack()
    {
        if (debug)
        {
            Debug.Log("���ݳ�");
        }
        mover.enabled = true;
        weapon.GetComponent<Collider>().enabled = true;
    }

    public void AttackTiming()
    {
      
        //����üũ
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
          
            Vector3 dirToTarget = (collider.transform.position - transform.position).normalized;
            // transform.forward �����ִ� ���⺤��
            // dirToTarget ĳ������ġ���� Ÿ���� ���⺤��
            // �ΰ��� ���� (Dot�Լ�)
            // �װ���� Cos��Ÿ �̰� cos��Ÿ�� 90�� �� 0 , 90 ���� ������ - , 90���� ũ�� + �̴�.

            // ���ݰ��� angle
            // �����ִ� ���Ϳ��� Ÿ���� ������ ������ ������ �����Ѵ�. �ΰ��� ���͸� �����ϸ� �翬�ϴ�. 1/2 �� �����ش�.
            // �ﰢ�Լ��� ȣ�����̱⶧���� * Mathf.Deg2Rad ���ش�.

            // ������ ������ �տ��ִ���
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
