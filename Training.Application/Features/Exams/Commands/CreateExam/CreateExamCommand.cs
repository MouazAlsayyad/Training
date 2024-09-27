using MediatR;

namespace Training.Application.Features.Exams.Commands.CreateExam
{
    public class CreateExamCommand : IRequest<CreateExamCommandResponse>
    {
        public TimeSpan Duration { get; set; }
        public DateTime TimeOfExam { get; set; }
        public Guid CourseId { get; set; }
    }
}
