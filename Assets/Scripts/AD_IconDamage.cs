using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD_IconDamage : MonoBehaviour
{
    private Animator _animator;
    

    // Start is called before the first frame update
    void Start()
    {
        //We get the animator component
        _animator = GetComponent<Animator>();
       
    }

    
    //Function that we will later  use to animate the life icon
    public void Animate()
    {
        _animator.Play("AD_IconDamage");
    }
}
