using System;
using UnityEngine;

namespace MonoBehaviours
{


    public class AnimatorOverrider : MonoBehaviour
    {
        Animator animator;


        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void SetAnimations(AnimatorOverrideController overrideController)
        {
            animator.runtimeAnimatorController = overrideController;
        }
    }
}
