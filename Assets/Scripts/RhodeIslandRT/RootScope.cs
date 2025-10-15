// Created by ChaomengOrion
// Create at 2022-08-14 14:30:12
// Last modified on 2022-08-15 23:22:34

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using RhodeIsland.RemoteTerminal.Resources;
using RhodeIsland.RemoteTerminal.Audio;

namespace RhodeIsland.RemoteTerminal
{
    [ComponentColor(ComponentType.MANAGER)]
    public class RootScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(TableManager.instance.Config.audioData);
            builder.RegisterInstance(TableManager.instance.AudioTable);
        }
    }
}