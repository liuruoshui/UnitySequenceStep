using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PigeonKingGames.Steps
{
    /// <summary>
    /// TODO, 将所有的step树形在manager中统一管理，可以制作图形工具进行调试和编辑。
    /// </summary>
    public class StepManager : MonoBehaviour
    {
        public static StepManager _instance;

        public static StepManager Instance {  get { return _instance; } }

        void Awake()
        {
            _instance = this;
        }

        public List<StepBase> allCurrentSteps = new List<StepBase>();
    }
}
