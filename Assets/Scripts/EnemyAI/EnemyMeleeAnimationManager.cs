using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class EnemyMeleeAnimationManager : MonoBehaviour
    {
        #region Variables

        [Header("Data")]

        private int canPlayAttackAnimationParameterCode;

        [Header("Components")]

        [SerializeField] private AnimationClip meleeAttackAnimationClip;

        [SerializeField] private Animator animationController;

        [Header("Scripts")]

        [SerializeField] private EnemyAI enemyAI;

        #endregion

        #region Methods

        public void SetCanPlayAttackAnimationBool(bool value)
        {
            animationController.SetBool(canPlayAttackAnimationParameterCode, value);
        }

        public bool GetCanPlayAttackAnimationValue()
        {
            return animationController.GetBool(canPlayAttackAnimationParameterCode);
        }

        public float GetAttackAnimationLength()
        {
            return meleeAttackAnimationClip.length;
        }

        public void StopAnimation()
        {
            SetCanPlayAttackAnimationBool(false);
            animationController.enabled = false;
        }

        public void StartAnimation()
        {
            animationController.enabled = true;
        }

        #endregion

        #region Unity Methods

        private void Start()
        {
            canPlayAttackAnimationParameterCode = Animator.StringToHash("canPlayAttackAnimation");
        }

        private void Update()
        {
            if (enemyAI.GetFleeStateBool() && GetCanPlayAttackAnimationValue())
            {
                StopAnimation();
            }
        }

        #endregion
    }
}