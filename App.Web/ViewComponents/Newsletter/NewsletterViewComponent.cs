using Microsoft.AspNetCore.Mvc;

public class NewsletterViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(); 
    }
}
