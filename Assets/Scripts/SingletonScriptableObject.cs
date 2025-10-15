// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-07-26 12:11:35

using System;
using UnityEngine;

namespace RhodeIsland
{
	public class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
	{
		/*public static T instance
		{
			get
			{
				if (!s_instance)
                {
					T[] _instances = Resources.FindObjectsOfTypeAll<T>();
					if (_instances.Length == 1)
					{
						s_instance = _instances[0];
					}
					else
					{
						Debug.Log("Wrong Singleton because find " + _instances.Length);
						return null;
					}
				}
				return s_instance;
			}
		}*/
		public static T instance
		{
			get
			{
				if (!s_instance)
				{
					s_instance = Resources.Load<T>(typeof(T).Name);
				}
				return s_instance;
			}
		}

		public static T GetInstanceSafe()
		{
			return null;
		}

		protected virtual void OnEnable()
		{
			if (s_instance != null && s_instance != this)
            {
				Debug.LogError(name + " is a singleton but exists twice!");
            }
			else
            {
				s_instance = null;
			}
		}

		protected virtual void OnDisable()
		{
			if (s_instance == this)
            {
				s_instance = null;
            }
		}

		private static T s_instance;
	}
}