// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-04-23 09:48:54

using System;
//using XLua;

namespace RhodeIsland
{
	public class Singleton<T> /*: IHotfixable*/ where T : class
	{
		public static T instance
		{
			get
			{
				if (s_instance == null)
                {
					_CreateInstance();
                }
				return s_instance;
			}
		}

		public static void Reset()
		{
            if (s_instance is IDisposable instance)
            {
                instance.Dispose();
            }
            s_instance = null;
		}

		private static void _CreateInstance()
		{
			/*if (typeof(T).GetConstructors().Length > 0)
            {
				throw new InvalidOperationException(string.Format("{0} has at least one accesible ctor making it impossible to enforce singleton behaviour", typeof(T).Name));
            }*/
			object ins = Activator.CreateInstance(typeof(T), true);
			if (ins != null)
            {
				s_instance = (T)ins;
            }
			else
            {
				s_instance = null;
			}
		}

		private static T s_instance;
	}
}
