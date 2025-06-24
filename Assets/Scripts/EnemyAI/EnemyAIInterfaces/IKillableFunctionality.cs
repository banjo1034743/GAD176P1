using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public interface IKillableFunctionality
    {
        public void DestroySelf();
    }
}