using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class DistanceFromPlayerChecker : MonoBehaviour
    {
        #region Variables

        [Header("Components")]

        private Transform playerTransform;
        
        [Header("Scripts")]

        [SerializeField] private PlayerApproacher playerApproacher;

        #endregion

        #region Methods

        public float CheckDistanceFromPlayer()
        {
            if (playerTransform != null)
            {
                return Vector3.Distance(transform.position, playerTransform.position);
            }
            else
            {
                return 0f;
            }
        }

        #endregion

        #region Unity Methods
        /// <summary>
        /// Have no idea why but the framework breaks if this is not called in Update
        /// </summary>
        private void Update()
        {
            if (playerApproacher.GetPlayerTransform() != null && playerTransform == null)
            {
                playerTransform = playerApproacher.GetPlayerTransform();
            }
        }

        #endregion
    }
}