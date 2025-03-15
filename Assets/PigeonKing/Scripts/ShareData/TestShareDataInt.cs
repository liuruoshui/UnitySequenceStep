using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PigeonKingGames.Steps.Data
{
    public class TestShareDataInt : MonoBehaviour
    {
        public ShareData<int> shareData;

        private void Start()
        {
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                shareData.Value++;
            }
        }
    }
}
