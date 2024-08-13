using Microsoft.AspNetCore.Mvc;

namespace MYChamp.Pages.Shared.Components.TitleText
{
    public class TitleTextViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
