using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD_IconDamage : MonoBehaviour
{
    private Animator _animator;
    

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        Debug.Log(_animator);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Animate()
    {
        _animator.Play("AD_IconDamage");
    }
}
