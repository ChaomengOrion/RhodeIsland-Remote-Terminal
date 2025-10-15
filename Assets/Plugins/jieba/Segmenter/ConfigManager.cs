using System.Collections;
using UnityEngine;

namespace JiebaNet
{
    [CreateAssetMenu(fileName = "JiebaConfigManager", menuName = "Jieba/JiebaConfigManager")]
    public class ConfigManager : ScriptableObject
    {
        public static void Init(ConfigManager manager)
        {
            IdfLoader = manager.idfLoader.text;
            StopWords = manager.stopWords.text;
            MainDictFile = manager.mainDictFile.text;
            ProbTransFile = manager.probTransFile.text;
            ProbEmitFile = manager.probEmitFile.text;
            PosProbStartFile = manager.posProbStartFile.text;
            PosProbTransFile = manager.posProbTransFile.text;
            PosProbEmitFile = manager.posProbEmitFile.text;
            CharStateTabFile = manager.charStateTabFile.text;
        }

        public static void Unload()
        {
            IdfLoader = null;
            StopWords = null;
            MainDictFile = null;
            ProbTransFile = null;
            ProbEmitFile = null;
            PosProbStartFile = null;
            PosProbTransFile = null;
            PosProbEmitFile = null;
            CharStateTabFile = null;
        }

        public static string IdfLoader { get; set; }

        public static string StopWords { get; set; }

        public static string MainDictFile { get; set; }

        public static string ProbTransFile { get; set; }

        public static string ProbEmitFile { get; set; }

        public static string PosProbStartFile { get; set; }

        public static string PosProbTransFile { get; set; }

        public static string PosProbEmitFile { get; set; }

        public static string CharStateTabFile { get; set; }

        [SerializeField]
        private TextAsset idfLoader;
        [SerializeField]
        private TextAsset stopWords;
        [SerializeField]
        private TextAsset mainDictFile;
        [SerializeField]
        private TextAsset probTransFile;
        [SerializeField]
        private TextAsset probEmitFile;
        [SerializeField]
        private TextAsset posProbStartFile;
        [SerializeField]
        private TextAsset posProbTransFile;
        [SerializeField]
        private TextAsset posProbEmitFile;
        [SerializeField]
        private TextAsset charStateTabFile;
    }
}