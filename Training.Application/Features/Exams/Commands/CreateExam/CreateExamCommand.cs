﻿using MediatR;

namespace Training.Application.Features.Exams.Commands.CreateExam
{
    public class CreateExamCommand : IRequest<Guid>
    {
        public TimeSpan Duration { get; set; }
        public DateTime TimeOfExam { get; set; }
        public Guid MaterialId { get; set; }
    }
}
