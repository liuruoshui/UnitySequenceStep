using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PigeonKingGames.Steps;
using System;
using Unity.Mathematics;

namespace PigeonKingGames.CanivalShooting
{
    // 用来设置一个有9张卡片的数字显示装置播放动画
    public class NumberCardAnimator : MonoBehaviour
    {

        public List<HideMesh> numberCardPrefabs;
        public List<Transform> turningTransforms;
        int currentNumber = 0;
        [SerializeField]
        float turningTime = 0.5f;
        [SerializeField]
        float appearTime = 0.02f;

        [SerializeField]
        float showRotate = 21;
        [SerializeField]
        float hideRotate = 338;

        float currentTime = 0;
        bool turnBigger = true;
        bool turning = false;
        int lastNumber;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //设置所有numberCardPrefabs的旋转角度为1度，且都不显示
            for (var i = 0; i < numberCardPrefabs.Count; i++)
            {
                turningTransforms[i].localEulerAngles = new Vector3(showRotate, 0, 0);
                numberCardPrefabs[i].Hide();
            }
            //设置第一个numberCardPrefabs的旋转角度为1度，且显示
            turningTransforms[0].localEulerAngles = new Vector3(showRotate, 0, 0);
            numberCardPrefabs[0].Show();
        }


        // Update is called once per frame
        void Update()
        {
            if(turning)
            {
                currentTime += Time.deltaTime;
                if (turnBigger)
                {
                    if(currentTime > appearTime)
                    {
                        numberCardPrefabs[currentNumber].Show();
                    }
                    if (currentTime < turningTime)
                    {
                        turningTransforms[lastNumber].transform.localEulerAngles = new Vector3(math.lerp(showRotate, hideRotate, currentTime / turningTime), 0, 0);
                    }
                    else
                    {
                        numberCardPrefabs[lastNumber].Hide();
                        turning = false;
                    }
                }
                else
                {
                    if(currentTime > turningTime - appearTime)
                    {
                        numberCardPrefabs[lastNumber].Hide();
                    }
                    if(currentTime < turningTime)
                    {
                        turningTransforms[currentNumber].transform.localEulerAngles = new Vector3(math.lerp(hideRotate, showRotate, currentTime / turningTime), 0, 0);
                    }
                    else
                    {
                        turningTransforms[currentNumber].transform.localEulerAngles = new Vector3(showRotate, 0, 0);
                        turning = false;
                    }
                }
            }
        }

        public void SetNumber(int nextNumber)
        {

            if (currentNumber == nextNumber)
            {
                return;
            }

            // 正在转动的物体不可见，即转向下一个转动动画
            numberCardPrefabs[lastNumber].Hide();
            //设置当前数字为显示
            numberCardPrefabs[currentNumber].Show();

            // 设置重新开始动画
            currentTime = 0;
            turning = true;


            // 设置当前数字的旋转为showRotate度
            turningTransforms[currentNumber].transform.localEulerAngles = new Vector3(showRotate, 0, 0);


            if (currentNumber < nextNumber)
            {
                // 设置数字变化方向
                turnBigger = true;

                // 设置下一个数字的旋转为showRotate度
                turningTransforms[nextNumber].transform.localEulerAngles = new Vector3(showRotate, 0, 0);
            }
            else
            {
                // 设置数字变化方向
                turnBigger = false;

                // 设置下一个数字的旋转为hideRotate度
                turningTransforms[nextNumber].transform.localEulerAngles = new Vector3(hideRotate, 0, 0);
                //设置下一个数字为显示
                numberCardPrefabs[nextNumber].Show();
            }
            turning = true;
            lastNumber = currentNumber;
            currentNumber = nextNumber;
        }
    }
}