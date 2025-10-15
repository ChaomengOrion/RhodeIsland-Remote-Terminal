// Created by ChaomengOrion
// Create at 2022-07-23 18:34:08
// Last modified on 2022-07-23 20:01:46

namespace RhodeIsland.RemoteTerminal.Character
{
    [System.Serializable]
    public struct CharacterInfo
    {
        public string id;
        public string nameCN;
        public string nameFL;
        public RarityRank rarity;
        public ProfessionCategory profession;
        public string nationId;
        public string groupId;
        public string teamId;
    }
}