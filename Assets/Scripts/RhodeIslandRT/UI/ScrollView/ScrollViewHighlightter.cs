// Created by ChaomengOrion
// Create at 2022-08-15 00:43:41
// Last modified on 2022-08-15 12:57:50

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RhodeIsland.RemoteTerminal.UI.ScrollView
{
    [ComponentColor(ComponentType.ELEMENT)]
    public class ScrollViewHighlightter : MonoBehaviour
    {
        [SerializeField]
        private float _followSpeed = 10f;

        private Func<float> m_getter = null;
        private float m_lastY = -1f;
        private float m_targetY = -1f;

        private void OnEnable()
        {
            GetComponent<Image>().enabled = false;
            this.InvokeEndOfFrame(() => GetComponent<Image>().enabled = true);
        }

        public void SetPosGetter(Func<float> getter)
        {
            if (m_getter == null && getter != null)
            {
                _SetPosY(getter.Invoke());
            }
            m_getter = getter;
        }

        private void _SetPosY(float y)
        {
            Vector3 pos = transform.localPosition;
            pos.y = y;
            transform.localPosition = pos;
        }

        private void LateUpdate()
        {
            if (m_getter != null)
            {
                m_targetY = m_getter.Invoke();
                float x = Mathf.Abs(m_lastY - m_targetY);
                if (x > 0.01f)
                {
                    m_lastY += m_lastY - m_targetY > 0f ? -_CalDelta(x) : _CalDelta(x);
                    _SetPosY(m_lastY);
                }
            }
        }

        private float _CalDelta(float x)
        {
             return Time.deltaTime * _followSpeed * x;
        }
    }
}
