using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    public float range;
    [SerializeField] LayerMask obstacleMask;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
       
        Collider[] targets = Physics.OverlapSphere(transform.position, range);
        for (int i = 0; i < targets.Length; i++)
        {
            IListenable listenable = targets[i].GetComponent<IListenable>();
            listenable?.Listen(transform);
        }

        Destroy(gameObject, 1f);
    }
}
