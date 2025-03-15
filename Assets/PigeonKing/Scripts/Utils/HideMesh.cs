using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PigeonKingGames.Steps
{
    public class HideMesh: MonoBehaviour
    {
        public List<MeshRenderer> renderers;
        bool _isShow = true;
        public void Hide()
        {
            if (!_isShow)
            {
                return;
            }
            foreach (var renderer in renderers)
            {
                renderer.enabled = false;
            }
            _isShow = false;
        }
        public void Show()
        {
            if (_isShow)
            {
                return;
            }
            foreach (var renderer in renderers)
            {
                renderer.enabled = true;
            }
            _isShow = true;
        }

        public bool isShow { get; }
    }
}