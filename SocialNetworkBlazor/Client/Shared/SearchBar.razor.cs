using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using SocialNetworkBlazor.Client.Store.User;
using SocialNetworkBlazor.Client.Store.User.Actions;
using SocialNetworkBlazor.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Shared
{
    public partial class SearchBar
    {
        [CascadingParameter]
        protected Task<AuthenticationState> AuthenticationState { get; set; }
        [Inject]
        private IState<UserState> UserState { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Inject]
        public IDispatcher Dispatcher { get; set; }
        IEnumerable<ClientUser> customCustomersData;
        private string _currentValue { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var state = await AuthenticationState;
            if (!state.User.Identity.IsAuthenticated)
                return;
        }

        private void OnLoadData(LoadDataArgs args)
        {
            Dispatcher.Dispatch(new GetFilteredUsersAction(args.Filter));

            customCustomersData = UserState.Value.ClientUsers.Where(c => c.FullName.Contains(args.Filter, StringComparison.OrdinalIgnoreCase));

            InvokeAsync(StateHasChanged);
        }

        private void OnChange(object value)
        {
            var selectedUser = UserState.Value.ClientUsers.Where(x => x.FullName == value.ToString()).FirstOrDefault();

            if (selectedUser != null)
            {
                _navigationManager.NavigateTo(_navigationManager.BaseUri + $"/Profile/{selectedUser.ContactId}");
                selectedUser = null;
            }

        }
    }
}
