using FluentValidation;
using OrderProductAPI.DTO.Request;

namespace OrderProductAPI.Validators
{
    public class RequestOrderDTOValidator : AbstractValidator<RequestOrderDTO>
    {
        public RequestOrderDTOValidator() 
        {
            RuleFor(x => x.CustomerFullName).NotEmpty().Matches(@"^(?![\s.]+$)[a-zA-Z\s.]*$");
            RuleFor(x => x.CustomerPhone).NotEmpty().Matches(@"^\d{10}$");
            RuleForEach(x => x.orderProducts).SetValidator(new RequestOrderProductDTOValidator());
        }
    }
}
