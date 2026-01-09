using BTKECommerce_Core.DTOs.Category;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTKECommerce_Core.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryDTO>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.CategoryName)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.").MinimumLength(3).WithMessage("Category name must be higher 3 characters.");
            RuleFor(c => c.Description)
                .MaximumLength(500).WithMessage("Category description must not exceed 500 characters.").MinimumLength(3).WithMessage("Category description must be higher 3 characters.");
        }
    }
}
