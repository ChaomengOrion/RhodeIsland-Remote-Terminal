// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-12 23:40:52

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

namespace RhodeIsland
{
	public static class GameObjectUtil
	{
		public static T[] FindAllObjectsInScene<T>() where T : Component
		{
			return null;
		}

		public static Component[] FindComponentsInScene(Type type)
		{
			return null;
		}

		public static void FindAllComponentsRecursively<T>(GameObject target, ref List<T> output) where T : Component
		{
		}

		public static void DestroyAllChildren(Transform transform)
		{
		}

		public static void Destroy(UnityEngine.Object obj)
		{
            UnityEngine.Object.Destroy(obj);
		}

		public static Coroutine InvokeAsync<T>(this T mono, Action<T> cb, float delay, bool ignoreTimeScale = false) where T : MonoBehaviour
		{
			return mono.StartCoroutine(_InvokeAsync(mono, cb, delay, ignoreTimeScale));
		}

		public static Coroutine InvokeAsync<T>(this T mono, Action cb, float delay, bool ignoreTimeScale = false) where T : MonoBehaviour
		{
			return mono.StartCoroutine(_InvokeAsync(mono, cb, delay, ignoreTimeScale));
		}

		[DebuggerHidden]
		private static IEnumerator _InvokeAsync<T>(T mono, Action<T> cb, float delay, bool ignoreTimeScale) where T : MonoBehaviour
		{
			if (ignoreTimeScale)
			{
				yield return new WaitForSecondsRealtime(delay);
			}
			else
			{
				yield return new WaitForSeconds(delay);
			}
			cb.Invoke(mono);
		}

		[DebuggerHidden]
		private static IEnumerator _InvokeAsync<T>(T mono, Action cb, float delay, bool ignoreTimeScale) where T : MonoBehaviour
		{
			if (ignoreTimeScale)
            {
				yield return new WaitForSecondsRealtime(delay);
            }
			else
            {
				yield return new WaitForSeconds(delay);
            }
			cb.Invoke();
		}

		public static Coroutine InvokeEndOfFrame<T>(this T mono, Action<T> cb) where T : MonoBehaviour
		{
			return mono.StartCoroutine(_InvokeEndOfFrame(mono, cb));
		}

		public static Coroutine InvokeEndOfFrame<T>(this T mono, Action cb) where T : MonoBehaviour
		{
			return mono.StartCoroutine(_InvokeEndOfFrame(mono, cb));
		}

		[DebuggerHidden]
		private static IEnumerator _InvokeEndOfFrame<T>(this T mono, Action<T> cb) where T : MonoBehaviour
		{
			yield return new WaitForEndOfFrame();
			cb.Invoke(mono);
		}

		[DebuggerHidden]
		private static IEnumerator _InvokeEndOfFrame<T>(this T mono, Action cb) where T : MonoBehaviour
		{
			yield return new WaitForEndOfFrame();
			cb.Invoke();
		}
		public static Coroutine InvokeNextFrame<T>(this T mono, Action<T> cb) where T : MonoBehaviour
		{
			return mono.StartCoroutine(_InvokeNextFrame(mono, cb));
		}
		public static Coroutine InvokeNextFrame<T>(this T mono, Action cb) where T : MonoBehaviour
		{
			return mono.StartCoroutine(_InvokeNextFrame(mono, cb));
		}
		[DebuggerHidden]
		private static IEnumerator _InvokeNextFrame<T>(this T mono, Action<T> cb) where T : MonoBehaviour
		{
			yield return null;
			cb.Invoke(mono);
		}
		[DebuggerHidden]
		private static IEnumerator _InvokeNextFrame<T>(this T mono, Action cb) where T : MonoBehaviour
		{
			yield return null;
			cb.Invoke();
		}
		public static T InstantiateLocal<T>(this T prototype, Vector3 position, Quaternion rotation, Transform parent) where T : Component
		{
			return null;
		}
		public static T InstantiateEnsureOnAwake<T>(this T prototype, Transform parent) where T : Component
		{
			return null;
		}
		public static void SetActiveIfNecessary(this GameObject gameObject, bool isActive)
		{
			if (gameObject)
			{
				if (isActive ^ gameObject.activeSelf)
				{
					gameObject.SetActive(isActive);
				}
			}
		}
		public static void SetEnabledIfNecessary(this Behaviour behaviour, bool isEnabled)
		{
			if (behaviour)
            {
				if (isEnabled ^ behaviour.enabled)
                {
					behaviour.enabled = isEnabled;
                }
            }
		}
		public static T AddComponent<T>(this GameObject go, T proto) where T : Component
		{
			return null;
		}
		public static T EnsureComponent<T>(this GameObject go) where T : Component
		{
			return null;
		}
		public static void ClearAllComponent<T>(this GameObject go, [Optional] Func<T, bool> validator, bool allowDestroyingAsset = false) where T : Component
		{
		}

		public static void ClearAllChildren(this Transform transform, [Optional] Func<Transform, bool> validator)
		{
			//MODIFY
			while (transform.childCount > 0)
			{
				Transform trans = transform.GetChild(0);
				trans.SetParent(null);
				Destroy(trans.gameObject);
			}
		}

		public static void ClearAllComponentsInChildren<T>(this GameObject go, bool includeInactive = false, Func<T, bool> validator = null) where T : Component
		{
		}
		public static void DisableComponentsInChildren<T>(this GameObject go, bool includeInactive = false, Func<T, bool> validator = null) where T : Behaviour
		{
		}
		public static Transform EnsureSubGameObject(this Transform transform, string name)
		{
			return null;
		}
		public static T EnsureSubGameObject<T>(this Transform transform, string name) where T : Component
		{
			return null;
		}
		public static Transform DuplicateAdditionalLayer(string name, Transform transform, bool includeRotationAndScale = false)
		{
			return null;
		}
		public static Rect TransformRect(this Transform transform, Rect rect)
		{
			return default(Rect);
		}
		public static Rect InverseTransformRect(this Transform transform, Rect rect)
		{
			return default(Rect);
		}
		public static string SafeName(this GameObject obj)
		{
			return null;
		}
		public static string SafeName(this Component component)
		{
			return null;
		}
		public static Transform FindDeepChildContainSubstring(this Transform transform, string substr, bool capitalSensitive = true)
		{
			return null;
		}
		public static Transform[] FindDeepChildrenContainSubstring(this Transform transform, string substr, bool capitalSensitive = true)
		{
			return null;
		}
		public static Transform FindDeepChild(this Transform transform, string name)
		{
			return null;
		}
		public static void ForeachComponentWithInterface<TClass, TInterface>(this Component comp, Action<TClass, TInterface> cb) where TClass : Component where TInterface : class
		{
		}
		public static void ForeachComponentInChildrenWithInterface<TClass, TInterface>(this Component comp, Action<TClass, TInterface> cb, bool includeInactive = false) where TClass : Component where TInterface : class
		{
		}
		public static RectTransform CreateUIObject(string name, RectTransform parent)
		{
			return null;
		}
		public static void SetLayerRecursively(GameObject go, int layer)
		{
		}
		public static void SetSortingLayerIDRecursively(GameObject go, int sortingLayerID)
		{
		}

		public static void AddSortingLayerOrderRecursively(GameObject go, int delta, bool includeInactive)
		{
            foreach (Renderer item in go.GetComponentsInChildren<Renderer>(includeInactive))
            {
				item.sortingOrder += delta;
			}
		}

		public static void TranverseAllGameObjects(GameObject root, Action<GameObject> callback)
		{
		}
		public static bool CheckIfAncestor(Transform ancestor, Transform descendant)
		{
			return default(bool);
		}
	}
}