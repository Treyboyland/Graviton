using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticlePiece : MonoBehaviour
{
    [SerializeField]
    ReticleController reticle;

    [SerializeField]
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        reticle.OnFlicker.AddListener(StartFlicker);
        reticle.OnStopFlickering.AddListener(StopFlicker);
    }

    void StartFlicker()
    {
        StopAllCoroutines();
        animator.CrossFade("Base Layer.Fade", 0);
    }

    void StopFlicker()
    {
        StopAllCoroutines();
        animator.CrossFade("Base Layer.Opaque", 0);   
    }


}
