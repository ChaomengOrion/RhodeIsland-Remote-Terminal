// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-04-16 10:25:26

using System;
using UnityEngine.UI;

public class NonDrawingGraphic : Graphic
{
	public override void SetMaterialDirty() { }

	public override void SetVerticesDirty() { }

	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
	}
}