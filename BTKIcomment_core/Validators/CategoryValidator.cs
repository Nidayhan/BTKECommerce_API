using BTKECommerce_core.DTOs.Category;
using FluentValidation;

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