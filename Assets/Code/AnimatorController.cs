using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;

namespace Animations
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField]
        protected Animator animator;
        public string currentAnimationState {get; protected set;}

        public string[] animationsList {get; protected set;}

        protected void Start()
        {
            animator = GetComponent<Animator>() as Animator;
            animationsList = animator.runtimeAnimatorController.animationClips.ToList().Select(x => x.name).ToArray();
        }

        public void ChangeAnimationState(string animationState, bool imediate = false)
        {
            if (currentAnimationState == animationState && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) return;

            if (!imediate && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) return;

            animator.Play(animationState, 0);

            currentAnimationState = animationState;
        }

    }
}
