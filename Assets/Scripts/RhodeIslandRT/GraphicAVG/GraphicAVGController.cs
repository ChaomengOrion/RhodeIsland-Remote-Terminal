// Created by ChaomengOrion
// Create at 2022-05-14 17:59:25
// Last modified on 2022-08-01 19:07:51

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using RhodeIsland.Arknights.AVG;

namespace RhodeIsland.RemoteTerminal.GraphicAVG
{
    [RequireComponent(typeof(CanvasGroup))]
    public class GraphicAVGController : SingletonMonoBehaviour<GraphicAVGController>
    {
        [SerializeField]
        private Transform _baseContext;
        [SerializeField]
        private Button _backBtn;
        [SerializeField, ValidateInput("IsDialogLinePerfab", "预制体需有组件继承自IDialogLine接口")]
        private GameObject _dialogLinePerfab;
        [SerializeField, ValidateInput("IsImageLinePerfab", "预制体需有组件继承自IImageLine接口")]
        private GameObject _imageLinePerfab, _backgrounndLinePerfab;

        private Action m_onEndCallBack = null;
        private CanvasGroup m_canvasGroup;
        private List<GameObject> m_lines = new();

#if UNITY_EDITOR
        private bool IsDialogLinePerfab(GameObject obj)
        {
            if (obj == null)
                return true;
            return obj.GetComponent<IDialogLine>() != null;
        }
        private bool IsImageLinePerfab(GameObject obj)
        {
            if (obj == null)
                return true;
            return obj.GetComponent<IImageLine>() != null;
        }
#endif

        protected override void OnInit()
        {
            m_canvasGroup = GetComponent<CanvasGroup>();
            m_canvasGroup.alpha = 0f;
            _backBtn.onClick.AddListener(_OnBackButtonClick);
        }

        public void RunStory(string storyId, Action onEndCallBack)
        {
            m_onEndCallBack = onEndCallBack;
            if (!Arknights.AVG.AVG.instance.TryGetStory(storyId, out Story story))
            {
                EndStory();
                return;
            }
            m_canvasGroup.DOKill();
            gameObject.SetActive(true);
            foreach (GameObject obj in m_lines)
            {
                Destroy(obj);
            }
            int lineCount = 0;
            foreach (Command command in story.commands)
            {
                _TryAddCommand(command, ref lineCount);
            }
            m_canvasGroup.DOFade(1f, 0.3f);
        }

        public void EndStory()
        {
            m_canvasGroup.DOKill();
            m_canvasGroup.DOFade(0f, 0.2f).OnComplete(() =>
            {
                foreach (GameObject obj in m_lines)
                {
                    Destroy(obj);
                }
                gameObject.SetActive(false);
            });
            m_onEndCallBack?.Invoke();
            m_onEndCallBack = null;
        }
        
        private void _OnBackButtonClick()
        {
            EndStory();
        }

        private void _TryAddCommand(Command command, ref int lineCount)
        {
            switch (command.command.ToLower())
            {
                case "dialog":
                    _AddDialog(command, ref lineCount);
                    break;
                case "image":
                case "background":
                    _AddImage(command, ref lineCount);
                    break;
                default:
                    return;
            }
        }

        private void _AddDialog(Command command, ref int lineCount)
        {
            if (string.IsNullOrEmpty(command.content))
                return;
            GameObject obj = Instantiate(_dialogLinePerfab, _baseContext);
            m_lines.Add(obj);
            obj.GetComponent<IDialogLine>().Init(command, lineCount);
            lineCount++;
        }

        private void _AddImage(Command command, ref int lineCount)
        {
            if (command.TryGetParam("image", out string key) && !string.IsNullOrEmpty(key))
            {
                string path = command.command.ToLower() == "background" ? ResourceRouter.GetBackgroundPath(key) : ResourceRouter.GetImagePath(key);
                Sprite sprite = AVGController.instance.assetLoader.Load<Sprite>(path);
                if (sprite != null)
                {
                    GameObject obj = Instantiate(command.command.ToLower() == "background" ? _backgrounndLinePerfab : _imageLinePerfab, _baseContext);
                    m_lines.Add(obj);
                    obj.GetComponent<IImageLine>().Init(command, sprite, lineCount);
                    lineCount++;
                }
            }
        }
    }
}