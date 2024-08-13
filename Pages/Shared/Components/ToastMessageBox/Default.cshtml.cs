using Microsoft.AspNetCore.Mvc;

namespace MYChamp.Pages.Shared.Components.ToastMessageBox
{
    public class ToastMessageBoxViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
