using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public abstract class EnemyAnimationManager : MonoBehaviour
    {
        #region Variables

        [Header("Data")]

        protected int canPlayAttackAnimationParameterCode;

        [Header("Components")]

        [SerializeField] protected AnimationClip attackAnimationClip;

        [SerializeField] protected Animator animationController;

        [Header("Scripts")]

        [SerializeField] protected EnemyAI enemyAI;

        #endregion

        #region Methods

        public virtual void StartAnimation()
        {
            animationController.enabled = true;
        }

        public virtual void StopAnimation()
        {
            SetCanPlayAttackAnimationBool(false);
            animationController.enabled = false;
        }

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
            return attackAnimationClip.length;
        }

        #endregion

        #region Unity Methods

        protected virtual void Start()
        {
            canPlayAttackAnimationParameterCode = Animator.StringToHash("canPlayAttackAnimation");
        }

        protected virtual void Update()
        {
            if (enemyAI.GetFleeStateBool() && GetCanPlayAttackAnimationValue())
            {
                StopAnimation();
            }
        }

        #endregion
    }
}