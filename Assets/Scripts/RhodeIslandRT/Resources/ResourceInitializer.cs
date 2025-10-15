// Created by ChaomengOrion
// Create at 2022-05-07 23:35:29
// Last modified on 2023-02-12 16:09:53

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RhodeIsland.Arknights.Resource;

namespace RhodeIsland.RemoteTerminal.Resources
{
    public class ResourceInitializer : MonoBehaviour
    {
        [SerializeField]
        private bool isDev;

        #region ReferenceMethods
        protected void Awake()
        {
            StartCoroutine(_DoInit());
        }
        #endregion

        private IEnumerator _DoInit()
        {
            Debuger.EnableOnText(true);
            if (Application.platform == RuntimePlatform.Android)
            {
                if (_TryFindManifestFile(packageNames, out string name, out string directory))
                {
                    Debug.Log($"[ResourceInitializer] Find Arknights with package name \"{name}\" in: {directory} on Android");
                    BundleRouter.arknightsPersistentResPath = directory;
                    BundleRouter.arknightsStreamResPath = Android2Unity.GetPackageStreamDataPath(name);
                    yield return BundleRouter.InitStreamIndex();
                }
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                BundleRouter.arknightsPersistentResPath = "F:/ArkAssets";
            }
            foreach (string asset in _GetKeyAssetsWhichNeedDownload())
            {
                Debug.Log("Downloaded: " + asset); //TODO
            }
            ResourceManager.InitIfNot();
            yield return TableManager.instance.Init();
            if (!isDev)
                SceneManager.LoadScene("Main");
            else
                VContainer.Unity.LifetimeScope.Find<RootScope>().Build();
        }

        private HashSet<string> _GetKeyAssetsWhichNeedDownload()
        {
            HashSet<string> assets = new();
            foreach (string asset in keyAssets)
            {
                if (BundleRouter.CheckABExistState(asset) == BundleRouter.ExistState.NotExist)
                {
                    assets.Add(asset);
                }
            }
            return assets;
        }

        private bool _TryFindManifestFile(string[] parkageNames, out string name, out string directory)
        {
            name = null;
            directory = null;
            int place;
            foreach (string parkageName in parkageNames)
            {
                place = 0;
                do
                {
                    FileInfo manifestFile = new(string.Format("/storage/emulated/{0}/Android/data/{1}/files/AB/Android/torappu.ab", place, parkageName));
                    if (manifestFile.Exists)
                    {
                        name = parkageName;
                        directory = manifestFile.DirectoryName;
                        return true;
                    }
                    place = 1;
                } while (place != 1);
            }
            return false;
        }

        private readonly string[] packageNames = new string[2]
        {
            "com.hypergryph.arknights",
            "com.hypergryph.arknights.bilibili"
        };

        private readonly string[] keyAssets = new string[2]
        {
            "torappu.ab",
            "torappu_index.ab"
        };

        private AndroidJavaObject android2Unity;
    }
}