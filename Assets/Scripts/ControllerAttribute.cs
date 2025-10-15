// Created by ChaomengOrion
// Create at 2022-08-14 13:24:08
// Last modified on 2022-08-14 13:54:37

using System;
using System.Diagnostics;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

namespace RhodeIsland
{

	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class ComponentColorAttribute : Attribute
	{
		public ComponentType Type { get; set; }

		public ComponentColorAttribute(ComponentType type = ComponentType.NONE)
		{
			Type = type;
		}
	}

	public enum ComponentType
	{
		CONTROLLER,
		MANAGER,
		ELEMENT,
		DEBUG,
		TEST,
		NONE
	}

	[DrawerPriority(0.5, 0.0, 0.0)]
	public sealed class ComponentColorAttributeDrawer : OdinAttributeDrawer<ComponentColorAttribute>
	{
		protected override void Initialize()
		{
			m_color = Attribute.Type switch
            {
				ComponentType.CONTROLLER => new(0.6f, 1f, 0.85f),
				ComponentType.MANAGER => new(0.55f, 0.85f, 1f),
				ComponentType.ELEMENT => new(1f, 0.85f, 0.6f),
				ComponentType.DEBUG => new(0.9f, 0.95f, 0.65f),
				ComponentType.TEST => new(0.95f, 0.65f, 0.65f),
				_ => Color.white
			};
		}

		/// <summary>
		/// Draws the property.
		/// </summary>
		protected override void DrawPropertyLayout(GUIContent label)
		{
			GUIHelper.PushColor(m_color, false);
			CallNextDrawer(label);
			GUIHelper.PopColor();
		}


		private Color m_color;
	}
}