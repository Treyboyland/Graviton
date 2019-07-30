using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnAnimationState : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    string baseLayerName;

    [SerializeField]
    string stateName;

    int stateHash;

    void OnEnable()
    {

    }

    IEnumerator DisableOnceStateReached()
    {
        while(animator.GetCurrentAnimatorStateInfo(0).fullPathHash != stateHash)
        {
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
