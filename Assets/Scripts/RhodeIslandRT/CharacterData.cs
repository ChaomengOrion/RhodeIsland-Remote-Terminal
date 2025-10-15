// Created by ChaomengOrion
// Create at 2022-05-01 10:58:39
// Last modified on 2022-07-27 00:04:45

namespace RhodeIsland.RemoteTerminal
{
	public class CharacterData
	{
		[UnityEngine.Scripting.Preserve]
		public CharacterData() { }

		public string name;

		public string description;

		public bool canUseGeneralPotentialItem;

		public string potentialItemId;

		public string nationId;

		public string groupId;

		public string teamId;

		public string displayNumber;

		public string tokenKey;

		public string appellation;

		//public BuildableType position;

		public string[] tagList;

		public string itemUsage;

		public string itemDesc;

		public string itemObtainApproach;

		public bool isNotObtainable;

		public bool isSpChar;

		public int maxPotentialLevel;

		public RarityRank rarity;

		public ProfessionCategory profession;

		public string subProfessionId;

		//public TraitDataBundle trait;

		//public CharacterData.PhaseData[] phases;

		//public MainSkill[] skills;

		//public TalentDataBundle[] talents;

		//public PotentialRank[] potentialRanks;

		//public AttributesDeltaKeyFrame favorKeyFrames;

		//public SkillLevelCost[] allSkillLvlup;

	}
}