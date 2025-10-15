// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-07-22 23:48:36

using System;
using UnityEngine;
using Sirenix.OdinInspector;
//using XLua;

namespace RhodeIsland
{
	public class SingletonMonoBehaviour<T> : MonoBehaviour/*, IHotfixable*/ where T : MonoBehaviour
	{
		public static T instance
		{
			get
			{
				if (!s_instance)
                {
					T[] objs = FindObjectsOfType<T>(true);
					int count = objs.Length;
					if (count > 2)
					{
						Debug.LogError(string.Format("There should be never more than one singleton {0} in this scene", typeof(T).Name));
					}
					if (count == 1)
                    {
						s_instance = objs[0];
					}
					else
                    {
						if (!typeof(ISingletonNotAutoCreate).IsAssignableFrom(typeof(T)))
                        {
							s_instance = new GameObject("~" + typeof(T).Name).AddComponent<T>();
                        }
                    }
				}
				return s_instance;
			}
		}

		public static T instanceOrNull
		{
			get
			{
				return s_instance;
			}
		}

		public static bool hasInstance
		{
			get
			{
				return s_instance;
			}
		}

		protected virtual void OnInit() { }

		protected virtual void OnDuplicated()
		{
			GameObject self = gameObject;
			Destroy(self);
			Debug.LogError(string.Concat(self.name, " is a singleton, delete duplicated instance."));
		}

		protected virtual void Awake()
		{
			if (instance != this)
            {
				OnDuplicated();
            }
			else
            {
				OnInit();
			}
		}

		protected virtual void OnDestroy()
		{
			if (s_instance == this)
			{
				s_instance = null;
			}
		}

		protected static T s_instance;
	}

	public class SingletonSerializedMonoBehaviour<T> : SerializedMonoBehaviour/*, IHotfixable*/ where T : MonoBehaviour
	{
		public static T instance
		{
			get
			{
				if (!s_instance)
				{
					T[] objs = FindObjectsOfType<T>(true);
					int count = objs.Length;
					if (count > 2)
					{
						Debug.LogError(string.Format("There should be never more than one singleton {0} in this scene", typeof(T).Name));
					}
					if (count == 1)
					{
						s_instance = objs[0];
					}
					else
					{
						if (!typeof(ISingletonNotAutoCreate).IsAssignableFrom(typeof(T)))
						{
							s_instance = new GameObject("~" + typeof(T).Name).AddComponent<T>();
						}
					}
				}
				return s_instance;
			}
		}

		public static T instanceOrNull
		{
			get
			{
				return s_instance;
			}
		}

		public static bool hasInstance
		{
			get
			{
				return s_instance;
			}
		}

		protected virtual void OnInit() { }

		protected virtual void OnDuplicated()
		{
			GameObject self = gameObject;
			Destroy(self);
			Debug.LogError(string.Concat(self.name, " is a singleton, delete duplicated instance."));
		}

		protected virtual void Awake()
		{
			if (instance != this)
			{
				OnDuplicated();
			}
			else
			{
				OnInit();
			}
		}

		protected virtual void OnDestroy()
		{
			if (s_instance == this)
			{
				s_instance = null;
			}
		}

		protected static T s_instance;
	}
}