﻿using Application.Commands.Category;
using Application.Common;
using Application.Common.Models;
using MediatR;

namespace Application.Handlers.Category;

public interface ICreateCategoryCommandHandler: IRequestHandler<CreateCategoryCommand, int>
{
    
}