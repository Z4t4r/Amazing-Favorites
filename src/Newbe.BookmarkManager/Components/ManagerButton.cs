﻿using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Newbe.BookmarkManager.Services;
using Newbe.BookmarkManager.Services.Configuration;
using Newbe.BookmarkManager.Services.EventHubs;
using Newbe.BookmarkManager.Services.RPC;
using System;
using System.Threading.Tasks;
using Newbe.BookmarkManager.Services.RPC.Handlers;
using WebExtensions.Net.Tabs;

namespace Newbe.BookmarkManager.Components
{
    public partial class ManagerButton
    {
        [Inject] public ITabsApi Tabs { get; set; }
        [Inject] public IOptions<StaticUrlOptions> StaticUrlOptions { get; set; }
        [Inject] public IBkManager BkManager { get; set; }
        [Inject] public IAfEventHub AfEventHub { get; set; }

        [Inject] public IMediator Mediator { get; set; }
        private bool _controlPanelVisible;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                var result = await Mediator.Send<SampleResponse>(new SampleRequest
                {
                    Name = "SampleRequest",
                    Count = 1
                });
                Console.WriteLine("result:");
                Console.WriteLine(result.Count);
                Console.WriteLine(result.Name);
                AfEventHub.RegisterHandler<TriggerOpenControlPanelEvent>(HandleTriggerOpenControlPanelEvent);
                await AfEventHub.EnsureStartAsync();
            }
        }

        private async Task HandleTriggerOpenControlPanelEvent(TriggerOpenControlPanelEvent arg)
        {
            await InvokeAsync(() =>
            {
                OpenControlPanel();
                StateHasChanged();
            });
        }

        private async Task OpenHelp()
        {
            await Tabs.OpenAsync(StaticUrlOptions.Value.Docs);
        }

        private async Task OpenWhatsNew()
        {
            await Tabs.OpenAsync(StaticUrlOptions.Value.WhatsNew);
        }

        private async Task OpenWelcome()
        {
            await Tabs.OpenAsync(StaticUrlOptions.Value.Welcome);
        }

        private void OpenControlPanel()
        {
            _controlPanelVisible = true;
        }

        private async Task OnClickResumeFactorySetting()
        {
            await BkManager.RestoreAsync();
            _controlPanelVisible = false;
        }

        private bool _visible = false;

        private void OnClickLike()
        {
            _visible = true;
        }
    }
}