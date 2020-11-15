using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SocialNetworkBlazor.Client.Store.User;
using SocialNetworkBlazor.Client.Store.User.Actions;
using SocialNetworkBlazor.Shared.Models;
using System;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Pages
{
    public partial class EditProfile
    {
        [Inject]
        public IDispatcher Dispatcher { get; set; }

        public ClientUserUpdate ClientUserUpdated { get; set; } = new ClientUserUpdate();

        public string ImageDataUrl { get; set; }

        public void HandleValidSubmit()
        {
            Dispatcher.Dispatch(new UpdateUserAction(ClientUserUpdated));
        }

        public async Task OnInputFileChanged(InputFileChangeEventArgs e)
        {
            var format = "image/png";

            var resizedImageFile = await e.File.RequestImageFileAsync(format, 100, 100);

            var buffer = new byte[resizedImageFile.Size];
            await resizedImageFile.OpenReadStream().ReadAsync(buffer);
            ImageDataUrl = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
            StateHasChanged();
            ClientUserUpdated.ProfileImageTitle = e.File.Name;
            ClientUserUpdated.ProfileImage = buffer;
        }
    }
}
