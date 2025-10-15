// Created by ChaomengOrion
// Create at 2022-07-28 23:02:10
// Last modified on 2022-07-31 16:51:26

using System;
using Spine.Unity;
using UnityEngine;

namespace Torappu.Building.Vault
{
	public class VCharacter : MonoBehaviour// : VRoom.Object, IMovable
	{
		public SkeletonAnimation Skeleton => _skeleton;
		public Options OptionsData => _options;
		public float SpineScale => _spineScale;

		/*[UnityEngine.Serialization.FormerlySerializedAs("_centerTransform")]
		[SerializeField]
		private Transform centerTransform;

		[UnityEngine.Serialization.FormerlySerializedAs("_shadowTransform")]
		[SerializeField]
		private Transform shadowTransform;*/

		//[UnityEngine.Serialization.FormerlySerializedAs("_skeleton")]
		[SerializeField]
		private SkeletonAnimation _skeleton;

		//[UnityEngine.Serialization.FormerlySerializedAs("_options")]
		[SerializeField]
		private Options _options;

		//[UnityEngine.Serialization.FormerlySerializedAs("_spineScale")]
		[SerializeField]
		private float _spineScale;

		[Serializable]
		public class Options
		{
			public Vector2 idleTimeRange;

			public float moveSpeed;

			public float moveAnimScale;

			public float probToInteractFurniture;

			//public VFurnitureBridge.InteractSlot interactSlot;

			public float probToPlaySpecialAnim;
		}
	}
}
