using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class RangedFleeStateManager : FleeStateManager
    {
        /// <summary>
        /// The content we've overriden the base with is the same as it, but as we want to call the Flee() coroutine with unique functionality here rather than the one in
        /// the base class, we keep the same functionality without using the base keyword instead.
        /// </summary>
        public override void CallFleeCycleCoroutine()
        {
            if (currentFleeCycle == null)
            {
                Debug.Log("AI is in the flee state!");
                enemyAIMovePointIndex = 0;
                currentFleeCycle = StartCoroutine(Flee());
            }
        }

        protected override IEnumerator Flee()
        {
            yield return null;
        }

        #region Unity Methods

        protected override void Start()
        {
            base.Start();
        }

        #endregion
    }
}