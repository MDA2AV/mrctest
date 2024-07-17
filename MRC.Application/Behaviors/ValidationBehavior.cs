using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MRC.Application.Behaviors
{
    //Generic Validation behaviour which implements MediatR IPipelineBehaviour
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
                                   where TRequest : IRequest<TResponse>{
        private readonly IValidator<TRequest>? _validator;
        public ValidationBehavior(IValidator<TRequest>? validator = null){
            _validator = validator;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken){
            if (_validator is null){ return await next(); }

            //Get any validation errors
            ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid){ return await next(); }

            ModelStateDictionary modelStateDictionary = new ModelStateDictionary();
            foreach (ValidationFailure failure in validationResult.Errors){
                modelStateDictionary.AddModelError(
                    failure.PropertyName,
                    failure.ErrorMessage);
            }
            ValidationProblemDetails problemDetails = new ValidationProblemDetails(modelStateDictionary);
            return (dynamic)problemDetails;
        }
    }
}
