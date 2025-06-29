using System.Collections;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class AttackStateMeleeManager : AttackStateManager
    {
        #region Variables

        [Header("Data (Melee)")]

        private Coroutine attackCoroutine;

        [Header("Components (Melee)")]

        [SerializeField] private AnimationClip meleeAttackClip;

        [SerializeField] private Animator animationController;

        [Header("Scripts (Melee)")]

        [SerializeField] private DistanceFromPlayerChecker distanceFromPlayerChecker;

        #endregion

        #region Methods
        public override void Attack()
        {
            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(AttackCoroutine());
            }
        }

        private IEnumerator AttackCoroutine()
        {
            Debug.Log("Attack called in AttackStateMeleeManager");

            while (distanceFromPlayerChecker.CheckDistanceFromPlayer() < distanceToAttackFrom)
            {
                if (!animationController.GetBool(0))
                {
                    // By doing getparameter with an index, we ensure that we dont need to dive back into script to change string value for parameter if changing its name
                    animationController.SetBool(animationController.GetParameter(0).name, true);
                }

                yield return new WaitForSeconds(meleeAttackClip.length);

                animationController.SetBool(animationController.GetParameter(0).name, false);

                yield return new WaitForSeconds(attackCooldown);
            }

            Debug.Log("No longer in attack state");
        }

        #endregion

        #region Unity Methods
        private void Start()
        {
            animationController.SetBool(animationController.GetParameter(0).name, false);
        }

        #endregion
    }
}