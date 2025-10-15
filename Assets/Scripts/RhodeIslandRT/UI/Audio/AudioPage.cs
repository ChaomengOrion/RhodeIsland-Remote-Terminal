// Created by ChaomengOrion
// Create at 2022-08-02 20:27:42
// Last modified on 2022-08-15 00:21:12

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;
using Sirenix.OdinInspector;
using RhodeIsland.RemoteTerminal.Audio;

namespace RhodeIsland.RemoteTerminal.UI.Audio
{
    [ComponentColor(ComponentType.CONTROLLER)]
    public class AudioPage : SingletonMonoBehaviour<AudioPage>, IPage, IPageScopeHandle
    {
        #region SerializeField
        [Title("Component", TitleAlignment = TitleAlignments.Right)]
        [Title("FirstMenu", HorizontalLine = false)]
        [SerializeField, Indent(2)]
        private GameObject _firstMenu;
        [SerializeField, Indent(2)]
        private UIAnimationPlayer _firstMenuAnim;

        [Title("SecondMenu", HorizontalLine = false)]
        [SerializeField, Indent(2)]
        private GameObject _secondMenu;
        [SerializeField, Indent(2)]
        private UIAnimationPlayer _secondMenuAnim;
        [SerializeField, Indent(2)]
        private Transform _secondMenuContent;

        [Title("PlayView", HorizontalLine = false)]
        [SerializeField]
        private MusicPlayView _musicPlayView;

        [Title("OtherUI", HorizontalLine = false)]
        [SerializeField, Indent(2)]
        private Button btnHome;

        [Title("Perfabs", TitleAlignment = TitleAlignments.Right)]
        [SerializeField]
        private MusicGroupItem _musicGroupItemPerfab;
        #endregion

        #region PublicAttribute
        #endregion

        #region PrivateField
        [Inject]
        private AudioDataHolder m_audioData;
        [Inject]
        private IObjectResolver m_objectResolver;
        #endregion

        #region ReferenceMethods
        protected override void OnInit()
        {
            base.OnInit();
            btnHome.onClick.AddListener(PageManager.instance.OnHomeBtnClick);
        }
        #endregion

        #region PublicMethods
        public void OnClose()
        {
            _ResetSecondMenu();
            gameObject.SetActive(false);
        }

        public void OnEnter()
        {
            gameObject.SetActive(true);
            _SetToFirstMenu().Forget();
        }

        public void OnConfigure(IContainerBuilder builder)
        {
            builder.Register<AudioDataHolder>(Lifetime.Scoped);
        }

        public void SetToGroup(MusicGroupType type)
        {
            _SetToGroup(type).Forget();
        }

        public void SetToPlayView(MusicGroupData data)
        {
            _SetToPlayView(data).Forget();
        }
        #endregion

        #region PrivateMethods
        /// <summary>
        /// Switch to the enter menu
        /// </summary>
        private async UniTaskVoid _SetToFirstMenu()
        {
            _firstMenu.SetActiveIfNecessary(true);
            btnHome.gameObject.SetActive(true);
            btnHome.interactable = false;
            await _firstMenuAnim.Play("TransIn", 0.06f);
            PageManager.instance.AddCurrent(() => _SetToFirstMenu().Forget(), null);
            btnHome.interactable = true;
        }

        /// <summary>
        /// Switch To the second menu
        /// </summary>
        /// <param name="type">The type of music group</param>
        private async UniTaskVoid _SetToGroup(MusicGroupType type)
        {
            if (m_audioData.TryGetGroupData(type, out var musicGroupDatas))
            {
                bool firstFinish = false;
                _firstMenuAnim.Play("TransOut").ContinueWith(() =>
                {
                    _firstMenu.SetActive(false);
                    firstFinish = true;
                }).Forget();
                _secondMenuContent.ClearAllChildren();
                _secondMenu.SetActive(true);
                bool side = false;
                _secondMenuAnim.ClearAnimations();
                foreach (var data in musicGroupDatas)
                {
                    side = !side;
                    MusicGroupItem item = m_objectResolver.Instantiate(_musicGroupItemPerfab, _secondMenuContent);
                    item.Render(data, side);
                    _secondMenuAnim.Animations.Add(item);
                }
                _secondMenuAnim.ResetAnimations();
                await UniTask.WhenAll(_secondMenuAnim.Play("TransIn"), UniTask.WaitUntil(() => firstFinish));
            }
            else
            {
                Debug.LogWarning(string.Format("Failed to get groupdata for {0}", type));
            }
        }

        /// <summary>
        /// Clear all items in the second menu and set its active to false
        /// </summary>
        private void _ResetSecondMenu()
        {
            _secondMenuContent.ClearAllChildren();
            _secondMenu.SetActive(false);
        }

        /// <summary>
        /// Switch to the music play view
        /// </summary>
        private async UniTaskVoid _SetToPlayView(MusicGroupData data)
        {
            bool firstFinish = false;
            _secondMenuAnim.Play("TransOut").ContinueWith(() =>
            {
                _secondMenu.SetActive(false);
                firstFinish = true;
            }).Forget();
            await UniTask.WhenAll(_musicPlayView.SetMusicGroup(data), UniTask.WaitUntil(() => firstFinish));
        }
        #endregion

        //public 
    }
}