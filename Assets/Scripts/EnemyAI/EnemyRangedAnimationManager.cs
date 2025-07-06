using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class EnemyRangedAnimationManager : EnemyAnimationManager
    {
        #region Methods

        public override void StartAnimation()
        {
            base.StartAnimation();
        }

        public override void StopAnimation()
        {
            base.StopAnimation();
        }

        #endregion

        #region Unity Methods

        protected override void Start()
        {
            canPlayAttackAnimationParameterCode = Animator.StringToHash("canPlayRangedAttackAnimation");
            StopAnimation();
        }

        protected override void Update()
        {
            base.Update();
        }

        #endregion
    }
}