using AutoMapper;
using MediatR;
using Training.Application.Contracts.Persistence;
using Training.Application.Exceptions;
using Training.Application.Responses;
using Training.Domain.Entities;

namespace Training.Application.Features.Exams.Commands.CreateExam
{
    public class CreateExamCommandHandler(
        IExamRepository examRepository,
        ICourseRepository courseRepository,
        IMapper mapper) : IRequestHandler<CreateExamCommand, BaseResponse<Guid>>
    {

        private readonly ICourseRepository _courselRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
        private readonly IExamRepository _examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<BaseResponse<Guid>> Handle(CreateExamCommand request, CancellationToken cancellationToken)
        {
         
            var validator = new CreateExamCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Count() > 0)
            {
                return new BaseResponse<Guid>("There is Validation Errors", false, 400, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }
            else
            {
                var course = await _courselRepository.GetByIdAsync(request.CourseId);
                
                if(course == null)
                {
                    return new BaseResponse<Guid>("course not found", false, 400);
                }
               
                var @exam = _mapper.Map<Exam>(request);
                var newExam = await _examRepository.AddAsync(@exam, cancellationToken);

                return new BaseResponse<Guid>("", true, 200, newExam.ExamId);
            }
        }
    }
}
