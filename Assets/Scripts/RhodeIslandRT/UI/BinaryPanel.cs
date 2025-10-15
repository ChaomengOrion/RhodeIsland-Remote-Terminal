// Created by ChaomengOrion
// Create at 2022-06-05 15:27:01
// Last modified on 2022-06-05 16:47:07

using System;
using UnityEngine;
using UnityEngine.UI;

namespace RhodeIsland.RemoteTerminal.UI
{
    public class BinaryPanel : MonoBehaviour, IPopUpPanel, IBinaryPanel
    {
        #region SerializeField
        [SerializeField]
        private Animation _animation;
        [SerializeField]
        private Button _btn_Yes;
        [SerializeField]
        private Button _btn_No;
        [SerializeField]
        private Text _message;
        #endregion

        #region PrivateField
        private Action<bool> m_onClick = null;
        #endregion

        #region ReferenceMethods
        protected void Start()
        {
            _btn_Yes.onClick.AddListener(_OnYesClick);
            _btn_No.onClick.AddListener(_OnNoClick);
        }

        public void Init(string message, Action<bool> onClick)
        {
            _message.text = message;
            m_onClick = onClick;
        }

        public float Expand()
        {
            _animation.Play("Expansion");
            return _animation.GetClip("Expansion").length;
        }

        public float Fold()
        {
            _animation.Play("Fold");
            return _animation.GetClip("Fold").length;
        }

        public void CloseMe()
        {
            Destroy(gameObject);
        }
        #endregion

        #region PrivateMethods
        private void _OnYesClick()
        {
            m_onClick?.Invoke(true);
            FrontPanelManager.instance.FoldCloseUppest();
        }

        private void _OnNoClick()
        {
            m_onClick?.Invoke(false);
            FrontPanelManager.instance.FoldCloseUppest();
        }
        #endregion
    }
}