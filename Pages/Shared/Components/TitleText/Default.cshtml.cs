using Microsoft.AspNetCore.Mvc;

namespace www.TrooperCruit.com.ViewComponents
{
    public class TitleTextViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
