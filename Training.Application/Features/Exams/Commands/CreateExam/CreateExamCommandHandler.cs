using AutoMapper;
using MediatR;
using Training.Application.Contracts.Persistence;
using Training.Application.Exceptions;
using Training.Domain.Entities;

namespace Training.Application.Features.Exams.Commands.CreateExam
{
    public class CreateExamCommandHandler(
        IExamRepository examRepository,
        ICourseRepository courseRepository,
        IMapper mapper) : IRequestHandler<CreateExamCommand, CreateExamCommandResponse>
    {

        private readonly ICourseRepository _courselRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
        private readonly IExamRepository _examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<CreateExamCommandResponse> Handle(CreateExamCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateExamCommandResponse();


            var validator = new CreateExamCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var course = await _courselRepository.GetByIdAsync(request.CourseId) ??
                throw new NotFoundException(
                    nameof(Course), request.CourseId,
                    $"Course with ID {request.CourseId} was not found.");

            var @exam = _mapper.Map<Exam>(request);
            var newExam = await _examRepository.AddAsync(@exam, cancellationToken);

            response.ExamId = newExam.ExamId;
            response.Success = true;
            response.Message = "Exam created successfully.";

            return response;
        }
    }
}
