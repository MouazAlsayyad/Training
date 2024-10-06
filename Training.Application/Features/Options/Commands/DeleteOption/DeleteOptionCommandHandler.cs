using AutoMapper;
using MediatR;
using Training.Application.Contracts.Persistence;
using Training.Application.Exceptions;
using Training.Application.Helper.Validators;
using Training.Application.Responses;
using Training.Domain.Entities;

namespace Training.Application.Features.Options.Commands.DeleteOption
{
    public class DeleteOptionCommandHandler(
        IOptionRepository optionRepository
       ) : IRequestHandler<DeleteOptionCommand, BaseResponse<object>>
    {
        private readonly IOptionRepository _optionRepository = optionRepository ?? throw new ArgumentNullException(nameof(optionRepository));
        

        public async Task<BaseResponse<object>> Handle(DeleteOptionCommand request, CancellationToken cancellationToken)
        {

            // Retrieve the option to be deleted
            var option = await _optionRepository.GetByIdAsync(request.OptionId);
           
            if (option == null)
            {
                return new BaseResponse<object>("Option Not Found", false, 404);
            }

            await _optionRepository.DeleteAsync(option, cancellationToken);


            return new BaseResponse<object>("Option has been Deleted", true, 200);
        }
    }
}
