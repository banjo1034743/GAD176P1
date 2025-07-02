using System.Collections;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class AttackStateMeleeManager : AttackStateManager
    {
        #region Variables

        [Header("Data (Melee)")]

        private Coroutine attackCoroutine;

        [Header("Scripts (Melee)")]

        [SerializeField] private DistanceFromPlayerChecker distanceFromPlayerChecker;

        [SerializeField] private PlayerApproacher playerApproacher;

        [SerializeField] private EnemyMeleeAnimationManager enemyMeleeAnimationManager;

        #endregion

        #region Methods
        public override void Attack()
        {
            Debug.Log("Attack() in AttackStateMeleeManager has been called");

            if (attackCoroutine == null)
            {
                Debug.Log("attackCoroutine wa null");
                attackCoroutine = StartCoroutine(AttackCoroutine());
            }
        }

        private IEnumerator AttackCoroutine()
        {
            Debug.Log("Attack called in AttackStateMeleeManager");

            enemyMeleeAnimationManager.StartAnimation();

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
            playerApproacher.SetIsInAttackDistanceValue(false);
            enemyMeleeAnimationManager.StopAnimation();
            EndAttackCoroutine(attackCoroutine);
        }

        #endregion

        #region Unity Methods
        private void Start()
        {
            //enemyMeleeAnimationManager.SetCanPlayAttackAnimationBool(false);
            //enemyMeleeAnimationManager.StopAnimation();
        }

        #endregion
    }
}