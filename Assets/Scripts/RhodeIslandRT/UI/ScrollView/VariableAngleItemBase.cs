// Created by ChaomengOrion
// Create at 2022-07-05 00:22:14
// Last modified on 2022-07-19 13:29:06

using UnityEngine;

namespace RhodeIsland.RemoteTerminal.UI.ScrollView
{

    public class VariableAngleItemBase : VariableAngleBase
    {
        [SerializeField]
        protected RectTransform _focus;

        protected RectTransform m_rectTransform;

        protected override void Awake()
        {
            base.Awake();
            m_rectTransform = GetComponent<RectTransform>();
        }

        public virtual void UpdatePadding(float contentY, float viewHeight)
        {
            (Vector2, Vector2) pos = _CalculateAnchoredAndSizeDelta(contentY, viewHeight);
            m_rectTransform.anchoredPosition = pos.Item1; // 锚点偏移
            m_rectTransform.sizeDelta = pos.Item2; // 尺寸偏移
        }

        public virtual void ResetPadding()
        {
            m_rectTransform.anchoredPosition = m_rectTransform.sizeDelta = Vector2.zero;
        }

        protected (Vector2, Vector2) _CalculateAnchoredAndSizeDelta(float contentY, float viewHeight, float angle = -1)
        {
            if (angle == -1)
                angle = m_angel;
            float k = Mathf.Tan(angle * Mathf.Deg2Rad);
            return (new Vector2(k * (contentY + viewHeight / 2f + _focus.localPosition.y), 0f), new Vector2(-k * viewHeight + k * _focus.rect.height, 0f));
        }
    }
}
