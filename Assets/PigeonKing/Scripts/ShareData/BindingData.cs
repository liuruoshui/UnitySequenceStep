using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace PigeonKingGames.Steps.Data
{
    public class BindingData<T>: MonoBehaviour where T:IComparable
    {
        ShareData<T> _value;
        public ShareData<T> value;
        protected virtual void Start()
        {
            if(value != null)
            {
                Bind(value);
            }
        }

        public void Bind(ShareData<T> shareData)
        {
            UnBind();
            _value = shareData;
            _value.AddOnValueSetListener(OnValueSet);
            _value.AddOnValueChangeListener(OnValueChange);
        }

        public void UnBind()
        {
            if(_value == null)
            {
                return;
            }
            _value.RemoveOnValueSetListener(OnValueSet);
            _value.RemoveOnValueChangeListener(OnValueChange);
            _value = null;
        }

        protected virtual void OnValueSet(T oldValue, T newValue)
        {
        }

        protected virtual void OnValueChange(T oldValue, T newValue)
        {
        }
    }
}
