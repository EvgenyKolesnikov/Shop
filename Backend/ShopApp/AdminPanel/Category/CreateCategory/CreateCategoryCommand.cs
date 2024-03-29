﻿using MediatR;
using Shop.Model;

namespace Shop.AdminPanel.Commands
{
    public class CreateCategoryCommand : IRequest<CategoryResponse>
    {
        public string? Name { get; set; }
        public int? ParentCategoryId { get; set; }

        public List<string> Features { get; set; }
    }
}
