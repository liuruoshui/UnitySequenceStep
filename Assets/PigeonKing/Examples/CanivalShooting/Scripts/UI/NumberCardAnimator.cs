using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PigeonKingGames.Steps;
using System;
using Unity.Mathematics;

namespace PigeonKingGames.CanivalShooting
{
    // ��������һ����9�ſ�Ƭ��������ʾװ�ò��Ŷ���
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
            //��������numberCardPrefabs����ת�Ƕ�Ϊ1�ȣ��Ҷ�����ʾ
            for (var i = 0; i < numberCardPrefabs.Count; i++)
            {
                turningTransforms[i].localEulerAngles = new Vector3(showRotate, 0, 0);
                numberCardPrefabs[i].Hide();
            }
            //���õ�һ��numberCardPrefabs����ת�Ƕ�Ϊ1�ȣ�����ʾ
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

            // ����ת�������岻�ɼ�����ת����һ��ת������
            numberCardPrefabs[lastNumber].Hide();
            //���õ�ǰ����Ϊ��ʾ
            numberCardPrefabs[currentNumber].Show();

            // �������¿�ʼ����
            currentTime = 0;
            turning = true;


            // ���õ�ǰ���ֵ���תΪshowRotate��
            turningTransforms[currentNumber].transform.localEulerAngles = new Vector3(showRotate, 0, 0);


            if (currentNumber < nextNumber)
            {
                // �������ֱ仯����
                turnBigger = true;

                // ������һ�����ֵ���תΪshowRotate��
                turningTransforms[nextNumber].transform.localEulerAngles = new Vector3(showRotate, 0, 0);
            }
            else
            {
                // �������ֱ仯����
                turnBigger = false;

                // ������һ�����ֵ���תΪhideRotate��
                turningTransforms[nextNumber].transform.localEulerAngles = new Vector3(hideRotate, 0, 0);
                //������һ������Ϊ��ʾ
                numberCardPrefabs[nextNumber].Show();
            }
            turning = true;
            lastNumber = currentNumber;
            currentNumber = nextNumber;
        }
    }
}