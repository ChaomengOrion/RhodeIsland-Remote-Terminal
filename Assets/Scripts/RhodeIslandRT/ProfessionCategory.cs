// Created by ChaomengOrion
// Create at 2022-07-23 12:51:16
// Last modified on 2022-07-23 20:42:18

using System;
using System.Collections.Generic;

namespace RhodeIsland.RemoteTerminal
{
	[Flags]
	public enum ProfessionCategory
	{
		NONE = 0,
		/// <summary>
		/// 近卫
		/// </summary>
		WARRIOR = 1,
		/// <summary>
		/// 狙击
		/// </summary>
		SNIPER = 2,
		/// <summary>
		/// 重装
		/// </summary>
		TANK = 4,
		/// <summary>
		/// 医疗
		/// </summary>
		MEDIC = 8,
		/// <summary>
		/// 辅助
		/// </summary>
		SUPPORT = 16,
		/// <summary>
		/// 术士
		/// </summary>
		CASTER = 32,
		/// <summary>
		/// 特种
		/// </summary>
		SPECIAL = 64,
		/// <summary>
		/// 召唤物
		/// </summary>
		TOKEN = 128,
		/// <summary>
		/// 装置
		/// </summary>
		TRAP = 256,
		/// <summary>
		/// 先锋
		/// </summary>
		PIONEER = 512
	}

	public static class Professions
    {
		private static readonly Dictionary<ProfessionCategory, string> m_dict = new()
		{
			[ProfessionCategory.NONE] = "无",
			[ProfessionCategory.WARRIOR] = "近卫",
			[ProfessionCategory.SNIPER] = "狙击",
			[ProfessionCategory.TANK] = "重装",
			[ProfessionCategory.MEDIC] = "医疗",
			[ProfessionCategory.CASTER] = "术士",
			[ProfessionCategory.SPECIAL] = "特种",
			[ProfessionCategory.TOKEN] = "召唤物",
			[ProfessionCategory.TRAP] = "装置",
			[ProfessionCategory.PIONEER] = "先锋",
		};

		public static string GetProfessionsName(ProfessionCategory profession)
        {
			return m_dict[profession];
        }
    }
}