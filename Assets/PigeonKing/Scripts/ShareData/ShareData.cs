using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace PigeonKingGames.Steps.Data
{
    public class ShareData<T> : MonoBehaviour where T:IComparable
    {
        public T Value
        {
            get {
                return _value;
            }
            set {
                OnValueSet?.Invoke(_value,value);
                if(_value.CompareTo(value) != 0)
                {
                    OnValueChange?.Invoke(_value, value);
                }
                _value = value;
            }
        }
        [SerializeField]
        T _value;
        UnityEvent<T,T> OnValueSet;
        UnityEvent<T,T> OnValueChange;

        /// <summary>
        /// action param1:old value param2:new value
        /// </summary>
        /// <param name="action"></param>
        public void AddOnValueSetListener(UnityAction<T,T> action)
        {
            if(OnValueSet == null)
            {
                OnValueSet = new UnityEvent<T, T>();
            }
            OnValueSet.AddListener(action);
        }
        /// <summary>
        /// action param1:old value param2:new value
        /// </summary>
        public void RemoveOnValueSetListener(UnityAction<T, T> action)
        {
            if(OnValueSet != null)
            OnValueSet.RemoveListener(action);
        }
        /// <summary>
        /// action param1:old value param2:new value
        /// </summary>
        public void AddOnValueChangeListener(UnityAction<T, T> action)
        {
            if(OnValueChange == null)
            {
                OnValueChange = new UnityEvent<T, T>();
            }
            OnValueChange.AddListener(action);
        }
        /// <summary>
        /// action param1:old value param2:new value
        /// </summary>
        public void RemoveOnValueChangeListener(UnityAction<T, T> action)
        {
            if(OnValueChange != null)
            OnValueChange.RemoveListener(action);
        }
    }
}
