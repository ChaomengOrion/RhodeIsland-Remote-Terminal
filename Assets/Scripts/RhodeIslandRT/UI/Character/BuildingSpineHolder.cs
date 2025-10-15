// Created by ChaomengOrion
// Create at 2022-07-28 22:01:38
// Last modified on 2022-07-31 16:51:37

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using Torappu.Building.Vault;

namespace RhodeIsland.RemoteTerminal.UI.Character
{
    public class BuildingSpineHolder
    {
        public SkeletonDataAsset skeletonData;
        public float spineScale;
        public VCharacter.Options options;
        //public Texture mainTex, alphaTex;
        //public Mesh mesh;

        public BuildingSpineHolder(VCharacter vChar)
        {
            skeletonData = vChar.Skeleton.skeletonDataAsset;
            options = vChar.OptionsData;
            spineScale = vChar.SpineScale;
            //mesh = vChar.skeleton.GetComponent<MeshFilter>().sharedMesh;
            //Material mat = vChar.skeleton.GetComponent<MeshRenderer>().sharedMaterial;
            //mainTex = mat.mainTexture;
            //alphaTex = mat.GetTexture("_AlphaTex");
        }
    }
}