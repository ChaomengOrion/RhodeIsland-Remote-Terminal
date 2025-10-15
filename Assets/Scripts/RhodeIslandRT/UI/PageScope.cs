// Created by ChaomengOrion
// Create at 2022-08-13 22:55:42
// Last modified on 2022-08-14 14:47:16

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Sirenix.OdinInspector;

namespace RhodeIsland.RemoteTerminal.UI
{
    [Icon("Assets/StaticAssets/Editor/Free Flat Gear 2 Icon.png"), ComponentColor(ComponentType.MANAGER)]
    public class PageScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            if (TryGetComponent(out IPage page))
            {
                builder.RegisterComponent(page).AsSelf();
            }
            if (TryGetComponent(out IPageScopeHandle handle))
            {
                handle.OnConfigure(builder);
            }
        }
    }

    public interface IPageScopeHandle
    {
        public void OnConfigure(IContainerBuilder builder);
    }
}