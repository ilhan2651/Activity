﻿using App.Dto.CommentDto;
using Microsoft.AspNetCore.Mvc;

public class CreateCommentViewComponent : ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync(int eventId)
    {
        var dto = new CreateCommentDto { EventId = eventId }; 
        return Task.FromResult<IViewComponentResult>(View(dto));
    }
}
