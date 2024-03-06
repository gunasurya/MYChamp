using Microsoft.AspNetCore.Mvc;

namespace www.TrooperCruit.com.ViewComponents
{
    public class ToastMessageBoxViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
