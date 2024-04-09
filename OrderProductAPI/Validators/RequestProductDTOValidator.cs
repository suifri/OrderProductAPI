using FluentValidation;
using OrderProductAPI.DTO.Request;

namespace OrderProductAPI.Validators
{
    public class RequestProductDTOValidator : AbstractValidator<RequestProductDTO>
    {
        public RequestProductDTOValidator()
        {
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price should be greater than 0");
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().Matches(@"^\D+$").WithMessage("Name should contain only letters");
        }
    }
}
