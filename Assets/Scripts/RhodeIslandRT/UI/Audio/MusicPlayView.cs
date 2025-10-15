// Created by ChaomengOrion
// Create at 2022-08-14 22:02:52
// Last modified on 2022-08-16 00:21:19

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using RhodeIsland.RemoteTerminal.Audio;
using RhodeIsland.RemoteTerminal.UI.ScrollView;
using RhodeIsland.Arknights;
using RhodeIsland.Arknights.Audio;
using AudioData = RhodeIsland.RemoteTerminal.Resources.AudioData;
using System.Linq;
using UnityEngine.EventSystems;

namespace RhodeIsland.RemoteTerminal.UI.Audio
{
    [ComponentColor(ComponentType.CONTROLLER)]
    public class MusicPlayView : UIAnimation
    {
        [SerializeField]
        private UIAnimationPlayer _animationPlayer;
        [SerializeField]
        private AutoFoucsScrollView _scrollView;
        [SerializeField]
        private ScrollViewHighlightter _scrollViewHighlightter;

        [SerializeField]
        private Slider _slider;
        [SerializeField] 
        private EventTrigger _sliderEventTrigger;

        [SerializeField]
        private Button _btnPlay, _btnLast, _btnNext, _btnFront, _btnBack;
        [SerializeField]
        private Text _timeText;
        [SerializeField]
        private Transform _rotateTrans;
        [SerializeField]
        private Image _recordImage;
        [SerializeField]
        private Transform _pinTrans;
        [SerializeField]
        private float pinRotationNoPlaying, pinRotationPlaying;

        [SerializeField]
        private RectTransform _loopFlag;

        [SerializeField]
        private MusicItem _musicItemPerfab;
        
        [Inject]
        private AudioPage m_page;
        [Inject]
        private AudioData m_audioData;
        [Inject]
        private IObjectResolver m_objectResolver;

        private Dictionary<string, SongData> m_songDatas = new();
        private AudioChannel m_channel = null;
        private SongData m_curSongData = null;

        private bool _switchToNewSong = false;
        private float _cachedTotalTime = 0f;
        private bool _draging = false;
        private bool _isPlaying = false;

        protected void Awake()
        {
            _scrollView.OnFocus += _OnSongFoucs;
            _scrollView.OnFocusChange += _OnSongDynamicFoucs;
            _btnPlay.onClick.AddListener(_OnPlay);
            var entryBeg = new EventTrigger.Entry
            {
                eventID = EventTriggerType.BeginDrag
            };
            var entryEnd = new EventTrigger.Entry
            {
                eventID = EventTriggerType.EndDrag
            };
            entryBeg.callback.AddListener(_OnSliderDragBegin);
            entryEnd.callback.AddListener(_OnSliderDragEnd);
            _sliderEventTrigger.triggers.Add(entryBeg);
            _sliderEventTrigger.triggers.Add(entryEnd);
        }

        protected void Update()
        {
            if (m_channel == null || !_isPlaying) return;

            float pos = m_channel.audioSchedule.pos;
            float total = _cachedTotalTime;

            // Convert pos and total to minutes:seconds format
            int posMinutes = Mathf.FloorToInt(pos / 60);
            int posSeconds = Mathf.FloorToInt(pos % 60);
            int totalMinutes = Mathf.FloorToInt(total / 60);
            int totalSeconds = Mathf.FloorToInt(total % 60);

            _timeText.text = $"{posMinutes}:{posSeconds:D2} <color=#A0A0A0><size=40>/ </size><size=36>{totalMinutes}:{totalSeconds:D2}</size></color>";
            if (!_draging) _slider.value = pos / total;

            _rotateTrans.transform.localEulerAngles += new Vector3(0, 0, Time.deltaTime * -30f);
        }

        /// <summary>
        /// Set the music group data and play initial animation
        /// </summary>
        /// <returns>A <see cref="UniTask"> which will finish when the TranIn animation has finished</returns>
        public async UniTask SetMusicGroup(MusicGroupData data)
        {
            _scrollViewHighlightter.SetPosGetter(null);
            m_songDatas.Clear();
            foreach (SongData songData in data.GetSongsList())
            {
                m_songDatas.Add(songData.GetId(), songData);
            }
            gameObject.SetActive(true);
            _SetSongs(m_songDatas.Values);
            _SetToSong(m_songDatas.Values.First().GetId());
            await _animationPlayer.Play("TransIn");
        }

        private void _SetSongs(IEnumerable<SongData> datas)
        {
            _scrollView.ClearItem();
            foreach (SongData data in datas)
            {
                MusicItem item = _scrollView.AddItem(_musicItemPerfab.gameObject).GetComponent<MusicItem>();
                item.Render(data.GetId(), data.GetName(), data.GetAuthor());
            }
        }

        private void _OnSongFoucs(object obj)
        {
            MusicItem item = (MusicItem)obj;
            _scrollViewHighlightter.SetPosGetter(() => _scrollView.ScrollRect.content.transform.localPosition.y + item.GetLocalPos().y);
            _SetToSong(item.GetId());
        }

        private void _OnSongDynamicFoucs(object obj)
        {
            MusicItem item = (MusicItem)obj;
            _scrollViewHighlightter.SetPosGetter(() => _scrollView.ScrollRect.content.transform.localPosition.y + item.GetLocalPos().y);
        }

        private void _SetToSong(string id)
        {
            Debug.Log("SetToSong: " + id);
            SongData data = m_songDatas[id];
            m_curSongData = data;
            _switchToNewSong = true;
        }

        private void _OnPlay()
        {
            if (_switchToNewSong)
            {
                AudioManager.StopMusic();

                if (m_audioData.TryGetBGMBank(m_curSongData.GetId(), out var bank))
                {
                    if (!string.IsNullOrEmpty(bank.intro))
                    {
                        m_channel = AudioManager.PlayMusicWithIntro(bank.intro, bank.loop);
                    }
                    else
                    {
                        m_channel = AudioManager.PlayMusic(bank.loop);
                    }
                }

                _cachedTotalTime = m_channel.audioSchedule.length;
                _switchToNewSong = false;

                if (!string.IsNullOrEmpty(bank.intro))
                {
                    float intro = m_channel.GetAudioSource(0).audioSource.clip.length;
                    _loopFlag.anchoredPosition = new Vector2(_slider.rectTransform().rect.width * (intro / _cachedTotalTime), -20f);
                    _loopFlag.gameObject.SetActive(true);
                }
                else
                {
                    _loopFlag.gameObject.SetActive(false);
                }

                _pinTrans.DOLocalRotate(new Vector3(0, 0, pinRotationPlaying), 0.2f);
                _isPlaying = true;
            }
            else if (_isPlaying)
            {
                //TODO
                _pinTrans.DOLocalRotate(new Vector3(0, 0, pinRotationNoPlaying), 0.2f);
                m_channel.audioSchedule.Pause();
                _isPlaying = false;
            }
            else
            {
                _pinTrans.DOLocalRotate(new Vector3(0, 0, pinRotationPlaying), 0.2f);
                m_channel.audioSchedule.Resume();
                _isPlaying = true;
            }
        }

        private void _OnSliderDragBegin(BaseEventData _)
        {
            _draging = true;
        }

        private void _OnSliderDragEnd(BaseEventData _)
        {
            _draging = false;
            m_channel.audioSchedule.SetPos(_cachedTotalTime * _slider.value);
        }
    }
}