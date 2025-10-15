// Created by ChaomengOrion
// Create at 2022-07-25 14:03:03
// Last modified on 2022-07-31 02:18:10

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace RhodeIsland.RemoteTerminal.UI.Character
{
    public class CircleTrans : MonoBehaviour
    {
        [SerializeField]
        private ChangeableMask _mask;
        [SerializeField]
        private Image _image;
        [SerializeField]
        private CanvasGroup _group;
        [SerializeField]
        private Ease _colorEase;
        [SerializeField]
        private Ease _moveEase;
        [SerializeField]
        private Ease _fadeEase;
        [SerializeField]
        private Button _btnBack;

        private Color m_toColor = Color.white;
        private readonly HashSet<Tweener> m_tweeners = new();
        protected Material m_material;

        protected void Awake()
        {
            _btnBack.onClick.AddListener(PageManager.instance.OnBackBtnClick);
            _mask.onChange = _OnChange;
        }

        public void DoColor(Color toColor, float t)
        {
            _image.DOKill();
            m_tweeners.Add(_image.DOColor(toColor, t).Play());
        }

        public void DoTrans(Image source, Color toColor, float t, Action cb = null)
        {
            gameObject.SetActiveIfNecessary(true);
            m_toColor = toColor;
            _image.color = source.color;
            foreach (Tweener item in m_tweeners)
            {
                item.Kill();
            }
            m_tweeners.Clear();
            _group.blocksRaycasts = true;
            _group.alpha = 0f;
            _btnBack.interactable = false;
            _btnBack.gameObject.SetActive(true);
            this.InvokeEndOfFrame(() =>
            {
                m_material = _mask.ModifiedMaterial;
                m_tweeners.Add(_image.DOColor(toColor, t).SetEase(_colorEase).Play());
                m_tweeners.Add(_group.DOFade(1f, t).SetEase(_fadeEase).Play());
                Rect size = source.GetPixelAdjustedRect();
                Rect rect = GetComponent<RectTransform>().rect;
                Vector2 pos = (Vector2)transform.worldToLocalMatrix.MultiplyPoint(source.transform.position) + rect.size / 2f;
                m_material.SetInteger("_Enable", 1);
                m_material.SetFloat("_CenterX", pos.x / rect.width);
                m_material.SetFloat("_CenterY", 1f - pos.y / rect.height);
                m_material.SetFloat("_Width", size.width / rect.width);
                m_material.SetFloat("_Height", size.height / rect.height);
                float r = 2 * Mathf.Sqrt(Mathf.Pow(rect.width, 2) + Mathf.Pow(rect.height, 2));
                m_tweeners.Add(m_material.DOFloat(r / rect.width, "_Width", t).SetEase(_moveEase).Play());
                m_tweeners.Add(m_material.DOFloat(r / rect.height, "_Height", t).SetEase(_moveEase).OnComplete(() => {
                    _btnBack.interactable = true;
                    _image.color = m_toColor;
                    m_material.SetInteger("_Enable", 0);
                    cb?.Invoke();
                }).Play());
            });
        }

        public void TransOff(float t)
        {
            foreach (Tweener item in m_tweeners)
            {
                item.Kill(true);
            }
            m_tweeners.Clear();
            _btnBack.gameObject.SetActive(false);
            m_material.SetInteger("_Enable", 0);
            _group.blocksRaycasts = false;
            m_tweeners.Add(_group.DOFade(0f, t).OnComplete(() => gameObject.SetActive(false)).Play());
        }

        private void _OnChange(Material mat)
        {
            if (mat == m_material)
                return;
            m_material = mat;
            m_material.SetInteger("_Enable", 0);
        }
    }
}