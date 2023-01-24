﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using InDrivoHRM.Models.Crm;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using InDrivoHRM.Models;

namespace InDrivoHRM.Pages
{
    public partial class EditOpportunityStatusComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        protected CrmService Crm { get; set; }

        [Parameter]
        public dynamic Id { get; set; }

        InDrivoHRM.Models.Crm.OpportunityStatus _opportunitystatus;
        protected InDrivoHRM.Models.Crm.OpportunityStatus opportunitystatus
        {
            get
            {
                return _opportunitystatus;
            }
            set
            {
                if (!object.Equals(_opportunitystatus, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "opportunitystatus", NewValue = value, OldValue = _opportunitystatus };
                    _opportunitystatus = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Security.InitializeAsync(AuthenticationStateProvider);
            if (!Security.IsAuthenticated())
            {
                UriHelper.NavigateTo("Login", true);
            }
            else
            {
                await Load();
            }
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var crmGetOpportunityStatusByIdResult = await Crm.GetOpportunityStatusById(Id);
            opportunitystatus = crmGetOpportunityStatusByIdResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(InDrivoHRM.Models.Crm.OpportunityStatus args)
        {
            try
            {
                var crmUpdateOpportunityStatusResult = await Crm.UpdateOpportunityStatus(Id, opportunitystatus);
                DialogService.Close(opportunitystatus);
            }
            catch (System.Exception crmUpdateOpportunityStatusException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update OpportunityStatus" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
