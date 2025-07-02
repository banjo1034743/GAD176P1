using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class EnemyMeleeAnimationManager : MonoBehaviour
    {
        private int canPlayAttackAnim;

        [SerializeField] private AnimationClip meleeAttackClip;

        [SerializeField] private Animator animationController;

        private void Start()
        {
            canPlayAttackAnim = Animator.StringToHash("canPlayAttackAnimation");
        }

        public void SetCanPlayAttackAnimationBool(bool value)
        {
            animationController.SetBool(canPlayAttackAnim, value);
        }

        public bool GetCanPlayAttackAnimationValue()
        {
            return animationController.GetBool(canPlayAttackAnim);
        }

        public float GetAttackAnimationLength()
        {
            return meleeAttackClip.length;
        }

        public void StopAnimation()
        {
            animationController.enabled = false;
        }

        public void StartAnimation()
        {
            animationController.enabled = true;
        }
    }
}