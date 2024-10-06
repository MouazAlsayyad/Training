using MediatR;
using Training.Application.Responses;

namespace Training.Application.Features.Exams.Commands.CreateExam
{
    public class CreateExamCommand : IRequest<BaseResponse<Guid>>
    {
        public TimeSpan Duration { get; set; }
        public DateTime TimeOfExam { get; set; }
        public Guid CourseId { get; set; }
    }
}
