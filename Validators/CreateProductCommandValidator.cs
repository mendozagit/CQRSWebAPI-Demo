using CQRSWebAPI_Demo.Features.Products.Command;
using FluentValidation;

namespace CQRSWebAPI_Demo.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Barcode)
                .NotEmpty()
                .MaximumLength(11);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(4);

        }
    }
}