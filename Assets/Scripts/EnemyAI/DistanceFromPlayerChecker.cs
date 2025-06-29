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
            return Vector3.Distance(transform.position, playerTransform.position);
        }

        #endregion

        #region Unity Methods

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