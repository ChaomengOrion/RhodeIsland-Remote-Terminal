// Created by ChaomengOrion
// Create at 2022-06-02 18:54:00
// Last modified on 2022-08-01 19:07:50

using System;
using UnityEngine;
using UnityEngine.UI;

namespace RhodeIsland.Arknights.UI
{
	public class UICullMaskController : MonoBehaviour
	{
		private void Start()
		{
			_UpdateLayout();
		}

		private void OnEnable()
		{
			_UpdateLayout();
		}

		private Vector2 _CalcUniformScreenSize()
		{
			UICanvasScalerHelper.UpdateScalerFitMode(_canvasScaler);
			Vector2 size = _panelCenter.sizeDelta, res = new();
			float width = Screen.width, height = Screen.height;
			if (width != 0 && height != 0)
            {
				//宽高比小于等于16:9
				if (width / height <= 1.7777778f)
				{
					res.x = size.x;
					res.y = size.x * height / width;
				}
				else
                {
					res.y = size.y;
					res.x = size.y * width / height;
				}
            }
			return res;
		}

		private void _UpdateLayout()
		{
			Vector2 size = _CalcUniformScreenSize(),
				center = _panelCenter.sizeDelta,
				res = size - center;
			float resY = res.y;
			float resX = 2f;
			bool vertical = false, horizontal = false;
			if (res.x >= 2f)
            {
				float X = res.x * 0.5f;
				float orgY = _panelRight.sizeDelta.y;
				_panelLeft.sizeDelta = new(X, orgY);
				_panelRight.sizeDelta = new(X, orgY);
				horizontal = true;
				resY = 2f;
				resX = res.x;
			}
			if (resX <= resY)
            {
				float Y = res.y * 0.5f;
				float orgX = _panelTop.sizeDelta.x;
				_panelTop.sizeDelta = new(orgX, Y);
				_panelBottom.sizeDelta = new(orgX, Y);
				vertical = true;
			}
			_panelTop.gameObject.SetActiveIfNecessary(vertical);
			_panelBottom.gameObject.SetActiveIfNecessary(vertical);
			_panelLeft.gameObject.SetActiveIfNecessary(horizontal);
			_panelRight.gameObject.SetActiveIfNecessary(horizontal);
		}

		[SerializeField]
		private RectTransform _panelLeft;

		[SerializeField]
		private RectTransform _panelRight;

		[SerializeField]
		private RectTransform _panelTop;

		[SerializeField]
		private RectTransform _panelBottom;

		[SerializeField]
		private RectTransform _panelCenter;

		[SerializeField]
		private CanvasScaler _canvasScaler;
	}
}