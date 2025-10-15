// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
using RhodeIsland.Arknights.Resource;
using UnityEngine;
//using XLua;

namespace RhodeIsland.Arknights.AVG
{
	/// <summary>
	/// AVG组件
	/// </summary>
	public abstract class AVGComponent : MonoBehaviour//, IHotfixable
	{
		protected AVGController controller { get; private set; }

		protected AbstractAssetLoader assetLoader
		{
			get
			{
				return controller.assetLoader;
			}
		}

		protected ResourceRouter router
		{
			get
			{
				return controller.router;
			}
		}

		/// <summary>
		/// 抽象方法: 获得该组件所有命令执行器
		/// </summary>
		/// <returns>所有命令执行器</returns>
		public abstract IList<ICommandExecutor> GetCommandExecutors();

		/// <summary>
		/// 设置该组件归属Controller
		/// </summary>
		/// <param name="controller"></param>
		public void SetController(AVGController controller)
		{
			this.controller = controller;
		}

		//EMPTY
		public virtual void OnStoryBegin(Story story) { }
		
		//EMPTY
		public virtual void OnStoryEnd(Story story) { }
		
		//EMPTY
		public virtual void OnReset() { }
	}
}
