using BTKECommerce_core.DTOs.Category;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTKECommerce_core.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryDTO>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        }
    }
}
