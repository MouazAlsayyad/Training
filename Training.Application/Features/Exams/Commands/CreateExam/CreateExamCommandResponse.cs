using Training.Application.Responses;

namespace Training.Application.Features.Exams.Commands.CreateExam
{
    public class CreateExamCommandResponse : BaseResponse
    {
        public CreateExamCommandResponse() : base() { }

        public Guid ExamId { get; set; }
    }
}
