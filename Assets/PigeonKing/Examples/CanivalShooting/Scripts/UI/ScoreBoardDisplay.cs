using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PigeonKingGames.Steps;
using System;
using Unity.Mathematics;
using PigeonKingGames.Steps.Data;
using PigeonKingGames.Utils;

namespace PigeonKingGames.CanivalShooting
{
    public class ScoreBoardDisplay : BindingData<int>
    {
        [SerializeField]
        int length = 3;

        [SerializeField]
        List<NumberCardAnimator> animators;

        protected override void Start()
        {
            base.Start();
            OnValueChange(0, 0);
            if (value != null)
            {
                OnValueChange(value.Value, value.Value);
            }
        }
        protected override void OnValueChange(int oldValue, int newValue)
        {
            var digitals = Digital.GetDigitals(newValue, length);
            for (var i = 0; i < length; i++)
            {
                animators[i].SetNumber(digitals[i]);
            }
        }
    }
}