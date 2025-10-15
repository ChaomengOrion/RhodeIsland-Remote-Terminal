// Created by ChaomengOrion
// Create at 2022-04-30 08:22:16
// Last modified on 2022-07-19 22:30:03

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace RhodeIsland.RemoteTerminal.UI
{
    [Icon("Assets/StaticAssets/Editor/Free Flat Gear 2 Icon.png"), GUIColor(1f, 0.75f, 0.5f)]
    public class PageManager : SingletonSerializedMonoBehaviour<PageManager>
    {
        #region SerializeField
        [SerializeField]
        private IPage _homePage;
        #endregion

        #region PublicAttribute
        public EventPool<PageEvent> EventPool
        {
            get => m_eventPool;
        }
        #endregion

        #region PrivateField
        private EventPool<PageEvent> m_eventPool = new();
        private IPage m_currentPage = null;
        private Stack<(Action, Action)> m_currents = new();
        #endregion

        #region ReferenceMethods
        protected override void OnInit()
        {
            EnterPage(_homePage);
        }
        #endregion

        #region PublicMethods
        public void OnBackBtnClick()
        {
            _OnBackBtnClick();
        }

        public void OnHomeBtnClick()
        {
            _OnHomeBtnClick();
        }

        public void EnterPage(IPage page)
        {
            if (page == null)
                return;
            m_currentPage?.OnClose();
            ClearCurrents();
            m_eventPool.Emit(PageEvent.PageChange, page);
            m_currentPage = page;
            page.OnEnter();
        }

        public void AddCurrent(Action onResumeCB, Action onCloseCB)
        {
            if (m_currents.Count > 0)
                m_currents.Peek().Item2?.Invoke();
            m_currents.Push((onResumeCB, onCloseCB));
            m_eventPool.Emit(PageEvent.CurrentChange, m_currentPage);
        }

        public void ClearCurrents()
        {
            while (m_currents.Count > 0)
            {
                m_currents.Pop().Item2?.Invoke();
            }
            m_eventPool.Emit(PageEvent.CurrentChange, m_currentPage);
        }
        #endregion

        #region PrivateMethods
        private void _OnHomeBtnClick()
        {
            m_eventPool.Emit(PageEvent.HomeButton, m_currentPage);
            ClearCurrents();
            EnterPage(_homePage);
        }

        private void _OnBackBtnClick()
        {
            m_eventPool.Emit(PageEvent.BackButton, m_currentPage);
            if (m_currents.Count > 1)
            {
                m_currents.Pop().Item2?.Invoke();
                m_currents.Peek().Item1?.Invoke();
            }
        }
        #endregion

        public enum PageEvent
        {
            HomeButton,
            BackButton,
            CurrentChange,
            PageChange
        }
    }
}