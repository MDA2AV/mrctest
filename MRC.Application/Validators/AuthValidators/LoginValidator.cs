using FluentValidation;
using MRC.Application.RequestHandlers;

namespace MRC.Application.Validators.AuthValidators{
    public class LoginValidator : AbstractValidator<LoginQuery>
    {
        public LoginValidator()
        {
            RuleFor(x => x.adaptedRequest.customerNumber).Length(3, 50);
            RuleFor(x => x.adaptedRequest.password).Length(3, 50);
        }
    }
}
