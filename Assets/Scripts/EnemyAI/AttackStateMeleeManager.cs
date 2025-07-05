using System.Collections;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class AttackStateMeleeManager : AttackStateManager
    {
        #region Variables

        [Header("Scripts (Melee)")]

        [SerializeField] private EnemyMeleeAnimationManager enemyMeleeAnimationManager;

        #endregion

        #region Methods
        public override void Attack()
        {
            //Debug.Log("Attack() in AttackStateMeleeManager has been called");

            if (attackCoroutine == null)
            {
                //Debug.Log("attackCoroutine wa null");

                enemyMeleeAnimationManager.StartAnimation();
                attackCoroutine = StartCoroutine(MeleeAttackCoroutine());
            }
        }

        private IEnumerator MeleeAttackCoroutine()
        {
            //Debug.Log("Attack called in AttackStateMeleeManager");

            while (distanceFromPlayerChecker.CheckDistanceFromPlayer() < distanceToAttackFrom)
            {
                if (!enemyMeleeAnimationManager.GetCanPlayAttackAnimationValue())
                {
                    enemyMeleeAnimationManager.SetCanPlayAttackAnimationBool(true);
                }

                yield return new WaitForSeconds(enemyMeleeAnimationManager.GetAttackAnimationLength());

                enemyMeleeAnimationManager.SetCanPlayAttackAnimationBool(false);

                yield return new WaitForSeconds(attackCooldown);
            }

            //Debug.Log("No longer in attack state");
            enemyMeleeAnimationManager.StopAnimation();
            playerApproacher.SetIsInAttackDistanceValue(false);
            EndAttackCoroutine(attackCoroutine);
        }

        public void ClearCoroutine()
        {
            attackCoroutine = null;
        }

        #endregion

        #region Unity Methods
        private void Start()
        {
            enemyMeleeAnimationManager.SetCanPlayAttackAnimationBool(false);
            enemyMeleeAnimationManager.StopAnimation();
        }

        #endregion
    }
}