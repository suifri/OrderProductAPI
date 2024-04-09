using FluentValidation;
using OrderProductAPI.DTO.Request;

namespace OrderProductAPI.Validators
{
    public class RequestOrderProductDTOValidator : AbstractValidator<RequestOrderProductDTO>
    {
        public RequestOrderProductDTOValidator() 
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
        }
    }
}
