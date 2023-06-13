using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MonsterMover : MonoBehaviour
{

    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float moveTime;

    private Vector3? target = null;
    private Vector3 moveDir;
    private Vector3[] randDir;
    private float curSpeed;
    private bool walk = false;
    private CharacterController controller;
    Coroutine moveRoutine;

    private float curTime = 0f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();


        randDir = new Vector3[4];

        randDir[0] = transform.forward;
        randDir[1] = transform.forward * -1;
        randDir[2] = transform.right;
        randDir[3] = transform.right * -1;
    }
    private void OnEnable()
    {
        //moveRoutine = StartCoroutine(MoveRoutine());
    }

    private void OnDisable()
    {
        StopCoroutine(moveRoutine);
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DetectPlayer(Vector3 _target)
    {
        target = _target;
        walk = false;

        moveDir = (_target - transform.forward).normalized;
    }
 
    //private IEnumerator MoveRoutine()
    //{
    //    while (true)
    //    {
    //        curTime += Time.deltaTime;

    //        if (curTime > moveTime)
    //        {
    //            curTime = 0f;

    //            if (target == null)
    //            {
    //                int rand = Random.Range(0, 3);
    //                moveDir = randDir[rand];
    //            }


    //            if (moveDir.sqrMagnitude <= 0)
    //            {
    //                curSpeed = Mathf.Lerp(curSpeed, 0, 0.1f);

    //                yield return null;
    //                continue;
    //            }


    //            if (walk)
    //            {
    //                curSpeed = Mathf.Lerp(curSpeed, walkSpeed, 0.1f);
    //            }
    //            else
    //            {
    //                curSpeed = Mathf.Lerp(curSpeed, runSpeed, 0.1f);
    //            }


    //            controller.Move(transform.forward * moveDir.z * curSpeed * Time.deltaTime);
    //            controller.Move(transform.right * moveDir.x * curSpeed * Time.deltaTime);


    //            Quaternion lookRotation = Quaternion.LookRotation(transform.forward * moveDir.z + transform.right * moveDir.x);
    //            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.2f);


    //            yield return null;

    //        }


    //        yield return null;
    //    }
    //}
}
