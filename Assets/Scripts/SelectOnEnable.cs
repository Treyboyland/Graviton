using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SelectOnEnable : MonoBehaviour
{
    [SerializeField]
    Button button;

    [SerializeField]
    EventSystem es;

    [SerializeField]
    Animator animator;

    void OnEnable()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Normal"))
        {
            animator.ResetTrigger("Normal");
            animator.SetTrigger("Highlighted");
        }
        button.Select();
    }

}
