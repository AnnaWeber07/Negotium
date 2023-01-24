using System;
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
    public partial class HomeComponent : ComponentBase
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
        protected RadzenGrid<Opportunity> grid0;
        protected RadzenGrid<InDrivoHRM.Models.Crm.Task> grid1;

        InDrivoHRM.Pages.Stats _monthlyStats;
        protected InDrivoHRM.Pages.Stats monthlyStats
        {
            get
            {
                return _monthlyStats;
            }
            set
            {
                if (!object.Equals(_monthlyStats, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "monthlyStats", NewValue = value, OldValue = _monthlyStats };
                    _monthlyStats = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<InDrivoHRM.Pages.RevenueByCompany> _revenueByCompany;
        protected IEnumerable<InDrivoHRM.Pages.RevenueByCompany> revenueByCompany
        {
            get
            {
                return _revenueByCompany;
            }
            set
            {
                if (!object.Equals(_revenueByCompany, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "revenueByCompany", NewValue = value, OldValue = _revenueByCompany };
                    _revenueByCompany = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<InDrivoHRM.Pages.RevenueByEmployee> _revenueByEmployee;
        protected IEnumerable<InDrivoHRM.Pages.RevenueByEmployee> revenueByEmployee
        {
            get
            {
                return _revenueByEmployee;
            }
            set
            {
                if (!object.Equals(_revenueByEmployee, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "revenueByEmployee", NewValue = value, OldValue = _revenueByEmployee };
                    _revenueByEmployee = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<InDrivoHRM.Pages.RevenueByMonth> _revenueByMonth;
        protected IEnumerable<InDrivoHRM.Pages.RevenueByMonth> revenueByMonth
        {
            get
            {
                return _revenueByMonth;
            }
            set
            {
                if (!object.Equals(_revenueByMonth, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "revenueByMonth", NewValue = value, OldValue = _revenueByMonth };
                    _revenueByMonth = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<InDrivoHRM.Models.Crm.Opportunity> _getOpportunitiesResult;
        protected IEnumerable<InDrivoHRM.Models.Crm.Opportunity> getOpportunitiesResult
        {
            get
            {
                return _getOpportunitiesResult;
            }
            set
            {
                if (!object.Equals(_getOpportunitiesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getOpportunitiesResult", NewValue = value, OldValue = _getOpportunitiesResult };
                    _getOpportunitiesResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<InDrivoHRM.Models.Crm.Task> _getTasksResult;
        protected IEnumerable<InDrivoHRM.Models.Crm.Task> getTasksResult
        {
            get
            {
                return _getTasksResult;
            }
            set
            {
                if (!object.Equals(_getTasksResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getTasksResult", NewValue = value, OldValue = _getTasksResult };
                    _getTasksResult = value;
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
            var monthlyStatsResult = MonthlyStats();
            monthlyStats = monthlyStatsResult;

            var revenueByCompanyResult = RevenueByCompany();
            revenueByCompany = revenueByCompanyResult;

            var revenueByEmployeeResult = RevenueByEmployee();
            revenueByEmployee = revenueByEmployeeResult;

            var revenueByMonthResult = RevenueByMonth();
            revenueByMonth = revenueByMonthResult;

            var crmGetOpportunitiesResult = await Crm.GetOpportunities(new Query() { OrderBy = $"CloseDate desc" });
            getOpportunitiesResult = crmGetOpportunitiesResult;

            var crmGetTasksResult = await Crm.GetTasks(new Query() { OrderBy = $"DueDate desc" });
            getTasksResult = crmGetTasksResult;
        }
    }
}
