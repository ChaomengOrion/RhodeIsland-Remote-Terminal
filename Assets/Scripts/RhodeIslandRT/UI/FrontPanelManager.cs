// Created by ChaomengOrion
// Create at 2022-06-05 14:45:23
// Last modified on 2022-06-05 17:00:56

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace RhodeIsland.RemoteTerminal.UI
{
    [Icon("Assets/StaticAssets/Editor/Free Flat Gear 2 Icon.png"), GUIColor(1f, 0.75f, 0.5f), RequireComponent(typeof(CanvasGroup))]
    public class FrontPanelManager : PersistentSingleton<FrontPanelManager>
    {
        #region SerializeField
        [SerializeField]
        private Transform _rootPanel;
        [SerializeField]
        private Image _blurImage;
        [Title("弹窗预制体")]
        [SerializeField, ValidateInput("IsIBinaryPanelPerfab", "预制体需有组件继承自IBinaryPanel和IPopUpPanel接口")]
        private GameObject _binaryPanelPerfab;
        [Title("参数设置")]
        [SerializeField]
        private float _blurIntensity;
        [SerializeField]
        private float _fadeInTime;
        [SerializeField]
        private float _fadeOutTime;

#if UNITY_EDITOR
        private bool IsIPopUpPanelPerfab(GameObject obj)
        {
            return obj.GetComponent<IPopUpPanel>() != null;
        }

        private bool IsIBinaryPanelPerfab(GameObject obj)
        {
            if (obj == null)
                return true;
            return obj.GetComponent<IBinaryPanel>() != null && IsIPopUpPanelPerfab(obj);
        }
#endif
        #endregion

        #region PublicAttribute
        public bool Active
        {
            get => m_active;
        }

        public Queue<IPopUpPanel> PopUpPanels
        {
            get => m_popUpPanels;
        }
        #endregion

        #region PrivateField
        private Queue<IPopUpPanel> m_popUpPanels = new();
        private CanvasGroup m_canvasGroup;
        private Material m_blurMaterial;
        private bool m_active = false;
        #endregion

        #region ReferenceMethods
        protected override void OnInit()
        {
            m_canvasGroup = GetComponent<CanvasGroup>();
            m_blurMaterial = _blurImage.material;
            m_canvasGroup.blocksRaycasts = false;
        }
        #endregion

        #region PublicMethods
        public void PopUpBinaryPanel(string message, Action<bool> onClick)
        {
            TryStartTransit();
            TryFoldUppest();
            GameObject panel = Instantiate(_binaryPanelPerfab, _rootPanel);
            IPopUpPanel popUpPanel = panel.GetComponent<IPopUpPanel>();
            m_popUpPanels.Enqueue(popUpPanel);
            IBinaryPanel binaryPanel = panel.GetComponent<IBinaryPanel>();
            binaryPanel.Init(message, onClick);
            popUpPanel.Expand();
        }

        public void FoldCloseUppest()
        {
            IPopUpPanel panel = m_popUpPanels.Dequeue();
            GameObjectUtil.InvokeAsync(this, () =>
            {
                panel.CloseMe();
            }, panel.Fold(), true);
            TryEndTransit();
        }

        public void TryFoldUppest()
        {
            if (m_popUpPanels.Count > 0)
            {
                m_popUpPanels.Peek().Fold();
            }
        }
        #endregion

        #region PrivateMethods
        protected void TryStartTransit()
        {
            if (m_active)
            {
                return;
            }
            m_canvasGroup.blocksRaycasts = true;
            m_blurMaterial.DOKill();
            m_blurMaterial.DOFloat(_blurIntensity, "_Size", _fadeInTime).SetUpdate(true).Play();
            m_canvasGroup.DOKill();
            m_canvasGroup.DOFade(1f, _fadeInTime).SetUpdate(true).Play();
        }

        protected void TryEndTransit()
        {
            if (m_popUpPanels.Count > 0)
            {
                return;
            }
            m_blurMaterial.DOKill();
            m_blurMaterial.DOFloat(0f, "_Size", _fadeOutTime).SetUpdate(true).Play();
            m_canvasGroup.DOKill();
            m_canvasGroup.DOFade(0f, _fadeOutTime).SetUpdate(true).OnComplete(() => m_canvasGroup.blocksRaycasts = false).Play();
        }
        #endregion
    }
}