// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-04-23 09:49:00

using System;
using UnityEngine;
//using XLua;

namespace RhodeIsland
{
	public class PersistentSingleton<T> : SingletonMonoBehaviour<T> where T : MonoBehaviour
	{
		public void DestroyMe(bool immediately = false)
		{
			GameObject obj = transform.root.gameObject;
			if (immediately)
            {
				DestroyImmediate(obj);
            }
			else
            {
				Destroy(obj);
            }
		}

		protected override void OnInit()
		{
			base.OnInit();
			DontDestroyOnLoad(transform.root.gameObject);
		}

		protected override void OnDuplicated()
		{
			Destroy(transform.root.gameObject);
		}
	}
}
