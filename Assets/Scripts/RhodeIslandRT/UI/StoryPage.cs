// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using RhodeIsland.RemoteTerminal.AVG;
using RhodeIsland.RemoteTerminal.GraphicAVG;
using RhodeIsland.RemoteTerminal.UI.ScrollView;
using RhodeIsland.Arknights.Resource;
using RhodeIsland.Arknights;
using Torappu.UI;
using Cysharp.Threading.Tasks;

namespace RhodeIsland.RemoteTerminal.UI
{
    [Icon("Assets/StaticAssets/Editor/Free Flat Gear 2 Icon.png"), GUIColor(1f, 0.75f, 0.5f)]
    public class StoryPage : SingletonMonoBehaviour<StoryPage>, IPage
    {
        [SerializeField]
        private VariableAngleScrollView _scrollView;
        [SerializeField]
        private GameObject _mainLineChapterInfoPerfab, _activityChapterInfoPerfab;
        [SerializeField]
        private GameObject _storyInfoPerfab;


        [SerializeField]
        private Button _btnEnterChapter;
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private float _fadeTime;
        [SerializeField]
        private ParticleSystem _screenParticleEffect;
        [SerializeField]
        private ParticleSystem _screenFogEffect;

        [SerializeField]
        private float _angle, _angleDuration;
        [SerializeField, LabelText("背景图片")]
        private Image _backgroundImage;
        [SerializeField, LabelText("章节图片")]
        private Image _chapterImage;
        [SerializeField]
        private CanvasGroup _chapterInfoPanel, _storyInfoPanel;
        [SerializeField]
        private Text _chapterInfo, _storyInfo, _storyCharacterInfo;
        [SerializeField]
        private CanvasGroup _briefPanel, _typePanel;
        [SerializeField]
        private Text _briefText;
        [SerializeField]
        private Text _chapterName;

        [SerializeField]
        private Button _btnHome, _btnBack;
        [SerializeField]
        private Button _btnAVG, _btnGraphicAVG;

        [Title("参数")]
        [SerializeField]
        private Vector2 _chapterScrollViewSizeDelta;
        [SerializeField]
        private Vector2 _storyScrollViewSizeDelta;
        [SerializeField]
        private float _fadeDuration;

        // Private
        private CanvasGroup _canvasGroup;
        private Dictionary<string, ZoneData> m_zoneDatas;
        private Dictionary<string, MainlineZoneData> m_mainlineZoneDatas;
        private Dictionary<string, string> m_activityBG;

        private int m_chapterIndex = -1;
        private string m_currentChapterId = null, m_currentStoryTxt = null;
        private StoryReviewType m_type = StoryReviewType.NONE;
        private State m_state = State.Chapter;
        private CancellationTokenSource m_loadInfoToken = null;

#if UNITY_EDITOR
        private bool IsStoryGroupPanel(GameObject obj)
        {
            if (obj == null)
                return true;
            return obj.GetComponent<IStoryGroupPanel>() != null;
        }
        private bool IsCharacterStoryGroupPanel(GameObject obj)
        {
            if (obj == null)
                return true;
            return obj.GetComponent<ICharacterStoryGroupPanel>() != null;
        }
        private bool IsStoryInfoPanel(GameObject obj)
        {
            if (obj == null)
                return true;
            return obj.GetComponent<IStoryInfoPanel>() != null;
        }
        private bool IsCharacterStoryInfoPanel(GameObject obj)
        {
            if (obj == null)
                return true;
            return obj.GetComponent<ICharacterStoryInfoPanel>() != null;
        }
#endif

        [Title("Runtime Debug")]
        [ShowInInspector, GUIColor(1f, 1f, 1f)]
        private AutoPackSpriteHub m_activityStorySpritePack;
        [ShowInInspector, GUIColor(1f, 1f, 1f)]
        private AutoPackSpriteHub m_miniStorySpritePack;
        [ShowInInspector, GUIColor(1f, 1f, 1f)]
        private readonly List<GameObject> m_panels = new();

        protected override void OnInit()
        {
            base.OnInit();
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;
            m_activityStorySpritePack = ResourceManager.Load<AutoPackSpriteHub>(ResourceUrls.GetStoryReviewActivityImageHubPath());
            m_miniStorySpritePack = ResourceManager.Load<AutoPackSpriteHub>(ResourceUrls.GetStoryReviewMiniActivityImageHubPath());
            m_zoneDatas = TableManager.instance.ZoneTable.zones;
            m_activityBG = TableManager.instance.Config.activityBG;
            m_mainlineZoneDatas = TableManager.instance.ZoneTable.mainlineAdditionInfo;
            _scrollView.OnFocusChange += _OnFocusChange;
            _scrollView.OnFocus += _OnFocus;
            _scrollView.OnDisFocus = _OnDisFocus;
            _btnEnterChapter.onClick.AddListener(_OnChatperBtnClick);
            _btnHome.onClick.AddListener(PageManager.instance.OnHomeBtnClick);
            _btnBack.onClick.AddListener(PageManager.instance.OnBackBtnClick);
            _btnAVG.onClick.AddListener(_OnAVGBtnClick);
            _btnGraphicAVG.onClick.AddListener(_OnGrapicAVGBtnClick);
        }

        public void OnEnter()
        {
            PageManager.instance.ClearCurrents();
            gameObject.SetActive(true);
            _scrollView.ClearItem();
            _scrollView.SetAngle(0);
            _scrollView.ScrollRect.rectTransform().sizeDelta = _chapterScrollViewSizeDelta;
            _SetEffects(true);
            _canvasGroup.DOKill();
            _canvasGroup.DOFade(1f, _fadeTime);
            _SetUpChapterGroup(StoryReviewType.MAIN_STORY);
            _backgroundImage.color = Color.black;
            _backgroundImage.DOColor(new(1f, 1f, 1f, 1.6f), _fadeTime);
        }

        public void OnClose()
        {
            gameObject.SetActive(false);
            _SetEffects(false);
        }

        public Sprite LoadStoryReviewEntryImage(string storyEntryPicId, StoryReviewType reviewType)
        {
            Sprite result = null;
            if (reviewType == StoryReviewType.ACTIVITY_STORY)
            {
                m_activityStorySpritePack.TryLoadSprite(storyEntryPicId, out result);
            }
            if (reviewType == StoryReviewType.MINI_STORY)
            {
                m_miniStorySpritePack.TryLoadSprite(storyEntryPicId, out result);
            }
            return result;
        }

        public bool TryLoadActivityImage(string id, out Sprite sprite)
        {
            //TODO
            return ResourceManager.TryLoadAsset("Arts/UI/Storyreview/Hubs/Activity/" + id, out sprite);
        }

        public bool TryLoadMINIStoryImage(string id, out Sprite sprite)
        {
            //TODO
            return ResourceManager.TryLoadAsset("Arts/UI/Storyreview/Hubs/Mini/Storyentrypic/" + id, out sprite);
        }

        public bool TryLoadMainLineImage(string id, out Sprite sprite)
        {
           //TODO
           return ResourceManager.TryLoadAsset("Arts/UI/Stage/[UC]StageZoneMainlineItemBg/zone_page_" + id, out sprite);
        }

        public bool TryLoadMainLineBackGroundImage(string id, out Sprite sprite)
        {
            //TODO
            return ResourceManager.TryLoadAsset("Arts/UI/Stage/[UC]StageZoneMainlineBg/" + id, out sprite);
        }

        public bool TryLoadStoryInfo(string id, out string info)
        {
            //TODO
            bool res = ResourceManager.TryLoadAsset("Gamedata/Story/[UC]" + id, out TextAsset text);
            if (res)
            {
                info = text.text;
                return true;
            }
            info = null;
            return false;
        }

        public void RunStory(string id)
        {
            StartCoroutine(DoRunStory(id));
        }

        public void RunGraphicStory(string id)
        {
            StartCoroutine(DoRunGraphicStory(id));
        }

        public void SetChapterGroup(StoryReviewType type)
        {
            m_type = type;
            _scrollView.ClearItem();
            IEnumerable<StoryReviewGroupClientData> groupDatas =
               from groupData in TableManager.instance.StoryReviewGroupDatas.Values
               where groupData.actType == type
               select groupData;
            float vH = _scrollView.ScrollRect.viewport.rect.height;
            if (type == StoryReviewType.MAIN_STORY)
            {
                StoryReviewGroupClientData[] datas = groupDatas.ToArray();
                List<GameObject> objs = _scrollView.AddItems(_mainLineChapterInfoPerfab, datas.Length);
                for (int i = 0; i < objs.Count; i++)
                {
                    StoryReviewGroupClientData data = datas[i];
                    ZoneData zoneData = m_zoneDatas[data.id];
                    MainLineChapterInfoItem info = objs[i].GetComponentInChildren<MainLineChapterInfoItem>();
                    info.Render(zoneData.zoneID, zoneData.zoneNameSecond, zoneData.zoneNameFirst, zoneData.zoneNameThird);
                    info.OnEnter(0f, vH, 0.5f + i / 10f);
                }
            }
            else if (type == StoryReviewType.ACTIVITY_STORY || type == StoryReviewType.MINI_STORY)
            {
                StoryReviewGroupClientData[] datas = groupDatas.ToArray();
                List<GameObject> objs = _scrollView.AddItems(_activityChapterInfoPerfab, datas.Length);
                for (int i = 0; i < objs.Count; i++)
                {
                    StoryReviewGroupClientData data = datas[i];
                    ActivityChapterInfoItem info = objs[i].GetComponentInChildren<ActivityChapterInfoItem>();
                    info.Render(data.id, data.name, type == StoryReviewType.ACTIVITY_STORY ? "别传" : "故事集");
                    info.OnEnter(0f, vH, 0.5f + i / 10f);
                }
            }
        }

        #region PrivateMethods
        private void _SetEffects(bool isOn)
        {
            _screenParticleEffect.gameObject.SetActive(isOn);
            _screenFogEffect.gameObject.SetActive(isOn);
        }

        private void _OnAVGBtnClick()
        {
            RunStory(m_currentStoryTxt);
        }

        private void _OnGrapicAVGBtnClick()
        {
            RunGraphicStory(m_currentStoryTxt);
        }

        private void _OnChatperBtnClick()
        {
            _SwitchFromChapterToStoryInfo(m_currentChapterId);
        }

        private void _OnFocusChange(object obj)
        {
            switch (m_state)
            {
                case State.Chapter:
                    string id = (string)obj;
                    m_loadInfoToken?.Cancel();
                    m_loadInfoToken = new();
                    _SetChapterInfoAsync(id, m_loadInfoToken.Token).Forget();
                    if (m_type == StoryReviewType.MAIN_STORY)
                    {
                        if (TryLoadMainLineImage(id, out Sprite sprite))
                        {
                            _chapterImage.sprite = sprite;
                        }
                        if (TryLoadMainLineBackGroundImage(m_mainlineZoneDatas[id].mainlneBgName, out Sprite bg))
                        {
                            _backgroundImage.sprite = bg;
                        }
                    }
                    else if (m_type == StoryReviewType.ACTIVITY_STORY)
                    {
                        if (TryLoadActivityImage(TableManager.instance.StoryReviewGroupDatas[id].storyEntryPicId, out Sprite sprite))
                        {
                            _chapterImage.sprite = sprite;
                        }
                    }
                    else if (m_type == StoryReviewType.MINI_STORY)
                    {
                        if (TryLoadMINIStoryImage(TableManager.instance.StoryReviewGroupDatas[id].storyEntryPicId, out Sprite sprite))
                        {
                            _chapterImage.sprite = sprite;
                        }
                    }
                    if (m_type == StoryReviewType.ACTIVITY_STORY || m_type == StoryReviewType.MINI_STORY)
                    {
                        if (!m_activityBG.TryGetValue(id, out string path) || string.IsNullOrEmpty(path))
                        {
                            if (ResourceManager.TryLoadAsset("Avg/Images/avg_5_7_shining", out Sprite bg))
                                _backgroundImage.sprite = bg;
                        }
                        else if (ResourceManager.TryLoadAsset(path, out Sprite bg))
                        {
                            _backgroundImage.sprite = bg;
                        }
                    }
                    break;
                case State.StoryInfo:
                    (string, string) pair = ((string, string))obj;
                    if (TryLoadStoryInfo(pair.Item2, out string info))
                        _briefText.text = info;
                    m_loadInfoToken?.Cancel();
                    m_loadInfoToken = new();
                    _SetStoryInfoAsync(pair.Item1, m_loadInfoToken.Token).Forget();
                    break;
                default:
                    break;
            }
        }

        private void _OnFocus(object obj)
        {
            switch (m_state)
            {
                case State.Chapter:
                    string id = (string)obj;
                    m_currentChapterId = (string)obj;
                    m_loadInfoToken?.Cancel();
                    m_loadInfoToken = new();
                    _SetChapterInfoAsync(m_currentChapterId, m_loadInfoToken.Token).Forget();
                    _btnEnterChapter.gameObject.SetActive(true);
                    if (m_type == StoryReviewType.MAIN_STORY)
                    {
                        if (TryLoadMainLineImage(id, out Sprite sprite))
                        {
                            _chapterImage.sprite = sprite;
                        }
                        if (TryLoadMainLineBackGroundImage(m_mainlineZoneDatas[id].mainlneBgName, out Sprite bg))
                        {
                            _backgroundImage.sprite = bg;
                        }
                    }
                    else if (m_type == StoryReviewType.ACTIVITY_STORY)
                    {
                        if (TryLoadActivityImage(TableManager.instance.StoryReviewGroupDatas[id].storyEntryPicId, out Sprite sprite))
                        {
                            _chapterImage.sprite = sprite;
                        }
                    }
                    else if (m_type == StoryReviewType.MINI_STORY)
                    {
                        if (TryLoadMINIStoryImage(TableManager.instance.StoryReviewGroupDatas[id].storyEntryPicId, out Sprite sprite))
                        {
                            _chapterImage.sprite = sprite;
                        }
                    }
                    if (m_type == StoryReviewType.ACTIVITY_STORY || m_type == StoryReviewType.MINI_STORY)
                    {
                        if (!m_activityBG.TryGetValue(id, out string path) || string.IsNullOrEmpty(path))
                        {
                            if (ResourceManager.TryLoadAsset("Avg/Images/avg_5_7_shining", out Sprite bg))
                                _backgroundImage.sprite = bg;
                        }
                        else if (ResourceManager.TryLoadAsset(path, out Sprite bg))
                        {
                            _backgroundImage.sprite = bg;
                        }
                    }
                    break;
                case State.StoryInfo:
                    (string, string) pair = ((string, string))obj;
                    m_currentStoryTxt = pair.Item1;
                    if (TryLoadStoryInfo(pair.Item2, out string info))
                        _briefText.text = info;
                    m_loadInfoToken?.Cancel();
                    m_loadInfoToken = new();
                    _SetStoryInfoAsync(m_currentStoryTxt, m_loadInfoToken.Token).Forget();
                    break;
                default:
                    break;
            }
        }

        private void _OnDisFocus()
        {
            switch (m_state)
            {
                case State.Chapter:
                    m_currentChapterId = null;
                    _btnEnterChapter.gameObject.SetActive(false);
                    break;
                case State.StoryInfo:
                    m_currentStoryTxt = null;
                    break;
                default:
                    break;
            }
        }

        private async UniTask _SetChapterInfoAsync(string id, CancellationToken token)
        {
            int chapterCount = TableManager.instance.StoryReviewGroupDatas[id].infoUnlockDatas.Count;
            long openTime = TableManager.instance.StoryReviewGroupDatas[id].startShowTime;
            if (!StorysManager.instance.IsChapterDone(id))
            {
                _chapterInfo.text = $"剧情数量：<color=#66ccff>{chapterCount}</color><color=#A0A0A0>节</color>\n" +
                    $"剧情总字数：<color=#66ccff>正在加载...</color>\n" +
                    $"CG数量：<color=#66ccff>正在加载...</color>\n" +
                    $"开放时间：<color=#66ccff>{TimeUtil.GetDateTime(openTime):yyyy<color=#A0A0A0>年</color>M<color=#A0A0A0>月</color>d<color=#A0A0A0>日</color>}</color>";
            }
            Chapter chapter = await StorysManager.instance.LoadChapterAsync(id);
            if (chapter.isDone && !token.IsCancellationRequested)
            {
                _chapterInfo.text = $"剧情数量：<color=#66ccff>{chapterCount}</color><color=#A0A0A0>节</color>\n" +
                    $"剧情总字数：<color=#66ccff>{chapter.fontCount}</color><color=#A0A0A0>字</color>\n" +
                    $"CG数量：<color=#66ccff>{chapter.cgCount}</color><color=#A0A0A0>张</color>\n" +
                    $"开放时间：<color=#66ccff>{TimeUtil.GetDateTime(openTime):yyyy<color=#A0A0A0>年</color>M<color=#A0A0A0>月</color>d<color=#A0A0A0>日</color>}</color>";
            }
        }

        private async UniTask _SetStoryInfoAsync(string id, CancellationToken token)
        {
            if (!StorysManager.instance.IsChapterDone(id))
            {
                _storyInfo.text = $"本节剧情字数：<color=#66CCFF>正在加载...</color>\n" +
                    $"预计阅读时间：<color=#66CCFF>正在加载...</color>";
                _storyCharacterInfo.text = "主要出场人物：正在加载...";
            }
            Chapter chapter = await StorysManager.instance.LoadChapterAsync(m_currentChapterId);
            if (chapter.isDone && !token.IsCancellationRequested)
            {
                StoryInfo info = chapter.storyInfos[chapter.storyPaths.IndexOf(id)];
                _storyInfo.text = $"本节剧情字数：<color=#66CCFF>{info.fontCount}</color><color=#606060>字</color>\n" +
                    $"预计阅读时间：<color=#66CCFF>{info.predictReadTime / 60f:F2}</color><color=#606060>分钟</color>";
                _storyCharacterInfo.text = "主要出场人物：" + string.Join("、", info.mainCharacters);
            }
        }

        private IEnumerator DoRunStory(string id)
        {
            _SetEffects(false);
            _canvasGroup.DOFade(0f, 0.5f).Play();
            yield return new WaitForSeconds(0.5f);
            _camera.enabled = false;
            bool end = false;
            Arknights.AVG.AVG.instance.StartStory(id, story => end = true);
            yield return new WaitUntil(() => end);
            foreach (var channel in AudioManager.instance.channels.Values)
            {
                channel.Stop(0.2f);
            }
            this.InvokeAsync(() =>
            {
                _canvasGroup.DOFade(1f, 0.3f).Play();
                _camera.enabled = true;
                _SetEffects(true);
            }, 0.3f);
        }

        private IEnumerator DoRunGraphicStory(string id)
        {
            _SetEffects(false);
            _canvasGroup.DOFade(0f, 0.2f).Play();
            yield return new WaitForSeconds(0.2f);
            bool end = false;
            GraphicAVGController.instance.RunStory(id, () => end = true);
            yield return new WaitUntil(() => end);
            _canvasGroup.DOFade(1f, 0.3f).Play();
            _SetEffects(true);
        }

        private void _SetUpChapterGroup(StoryReviewType type)
        {
            PageManager.instance.AddCurrent(_SwitchFromStoryInfoToChapter, null);
            m_state = State.Chapter;
            SetChapterGroup(type);

            _typePanel.alpha = 0f;
            _typePanel.DOFade(1f, _fadeTime);
            _typePanel.gameObject.SetActive(true);

            _chapterName.gameObject.SetActive(false);

            if (_storyInfoPanel.gameObject.activeSelf)
            {
                _storyInfoPanel.DOKill();
                _storyInfoPanel.interactable = false;
                _storyInfoPanel.DOFade(0f, _fadeDuration).OnComplete(() =>
                {
                    _storyInfoPanel.gameObject.SetActive(false);
                });
            }

            if (_briefPanel.gameObject.activeSelf)
            {
                _briefPanel.DOKill();
                _briefPanel.DOFade(0f, _fadeDuration).OnComplete(() =>
                {
                    _storyInfoPanel.gameObject.SetActive(false);
                });
            }

            _chapterImage.DOKill();
            _chapterImage.color = new(1f, 1f, 1f, 0f);
            _chapterImage.gameObject.SetActive(true);
            _chapterImage.DOFade(1f, _fadeDuration);

            _chapterInfoPanel.DOKill();
            _chapterInfoPanel.interactable = true;
            _chapterInfoPanel.alpha = 0f;
            _chapterInfoPanel.gameObject.SetActive(true);
            _chapterInfoPanel.DOFade(1f, _fadeDuration);

            _scrollView.SetFocusForce(0);
        }

        private void _SwitchFromChapterToStoryInfo(string chapterId)
        {
            _btnEnterChapter.gameObject.SetActive(false);
            m_chapterIndex = _scrollView.FoucsIndex;
            PageManager.instance.AddCurrent(null, null);
            m_state = State.Trans;
            _scrollView.rectTransform().DOSizeDelta(_storyScrollViewSizeDelta, _angleDuration).SetEase(Ease.InOutCirc).SetUpdate(true).Play();
            foreach (GameObject obj in _scrollView.Items)
            {
                ChapterInfoItem item = obj.GetComponentInChildren<ChapterInfoItem>();
                if (item)
                {
                    item.OnExit(_angleDuration);
                }
            }

            _chapterName.text = "《" + TableManager.instance.StoryReviewGroupDatas[m_currentChapterId].name  + "》";
            _chapterName.gameObject.SetActive(true);

            _typePanel.interactable = false;
            _typePanel.DOFade(0f, _fadeDuration).Play();

            _chapterInfoPanel.DOKill();
            _chapterInfoPanel.interactable = false;
            _chapterInfoPanel.DOFade(0f, _fadeDuration).Play();

            _chapterImage.DOKill();
            _chapterImage.DOFade(0f, _fadeDuration).OnComplete(() =>
            {
                _typePanel.gameObject.SetActive(false);
                _chapterInfoPanel.gameObject.SetActive(false);
                _chapterImage.gameObject.SetActive(false);
            }).Play();

            _storyInfoPanel.DOKill();
            _storyInfoPanel.interactable = true;
            _storyInfoPanel.alpha = 0f;
            _storyInfoPanel.gameObject.SetActive(true);
            _storyInfoPanel.DOFade(1f, _fadeDuration).Play();

            _scrollView.DOAngle(_angle, _angleDuration).OnComplete(() =>
            {
                _btnBack.gameObject.SetActive(true);

                _scrollView.ClearItem();
                _SetStoryInfoGroup(chapterId);

                _briefPanel.DOKill();
                _briefPanel.alpha = 0f;
                _briefPanel.gameObject.SetActive(true);
                _briefPanel.DOFade(1f, _fadeDuration);
                m_state = State.StoryInfo;
            }).Play();
        }

        private void _SwitchFromStoryInfoToChapter()
        {
            m_state = State.Trans;
            _scrollView.rectTransform().DOSizeDelta(_chapterScrollViewSizeDelta, _angleDuration).SetEase(Ease.InOutCirc).SetUpdate(true).Play();
            foreach (GameObject obj in _scrollView.Items)
            {
                StoryInfoItem item = obj.GetComponentInChildren<StoryInfoItem>();
                if (item)
                {
                    item.OnExit(_angleDuration);
                }
            }

            _chapterName.gameObject.SetActive(false);

            _btnBack.gameObject.SetActive(false);
            _storyInfoPanel.DOKill();
            _storyInfoPanel.interactable = false;
            _storyInfoPanel.DOFade(0f, _fadeDuration).Play();

            _briefPanel.DOKill();
            _briefPanel.DOFade(0f, _fadeDuration).OnComplete(() =>
            {
                _storyInfoPanel.gameObject.SetActive(false);
                _briefPanel.gameObject.SetActive(false);
            }).Play();

            _typePanel.DOKill();
            _typePanel.gameObject.SetActive(true);
            _typePanel.interactable = true;
            _typePanel.DOFade(1f, _fadeDuration).Play();

            _chapterInfoPanel.DOKill();
            _chapterInfoPanel.interactable = true;
            _chapterInfoPanel.alpha = 0f;
            _chapterInfoPanel.gameObject.SetActive(true);
            _chapterInfoPanel.DOFade(1f, _fadeDuration).Play();

            _scrollView.DOAngle(0f, _angleDuration).OnComplete(() =>
            {
                _scrollView.ClearItem();
                SetChapterGroup(m_type);
                _scrollView.SetFocusForce(m_chapterIndex);
                _chapterImage.DOKill();
                _chapterImage.color = new(1f, 1f, 1f, 0f);
                _chapterImage.gameObject.SetActive(true);
                _chapterImage.DOFade(1f, _fadeDuration).Play();
                m_state = State.Chapter;
                _btnEnterChapter.gameObject.SetActive(true);
            }).Play();
        }

        private void _SetStoryInfoGroup(string chapterId)
        {
            List<StoryReviewInfoClientData> datas = TableManager.instance.StoryReviewGroupDatas[chapterId].infoUnlockDatas;
            List<GameObject> objs = _scrollView.AddItems(_storyInfoPerfab, datas.Count);
            float vH = _scrollView.ScrollRect.viewport.rect.height;
            for (int i = 0; i < objs.Count; i++)
            {
                StoryReviewInfoClientData info = datas[i];
                StoryInfoItem item = objs[i].GetComponentInChildren<StoryInfoItem>();
                item.Render(info.storyTxt, info.storyInfo, info.storyCode, info.storyName, info.avgTag);
                item.OnEnter(0f, vH, 0.5f + i / 20f);
            }
            _scrollView.ScrollRect.vertical = false;
            this.InvokeAsync(() => _scrollView.ScrollRect.vertical = true, 0.8f);
        }
        #endregion

        public enum State
        {
            Chapter,
            StoryInfo,
            Trans
        }
    }
}
