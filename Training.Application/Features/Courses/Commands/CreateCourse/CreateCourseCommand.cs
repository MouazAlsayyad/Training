using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Application.Responses;

namespace Training.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommand : IRequest<BaseResponse<Guid>>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(256)]
        public string? Description { get; set; }
    }
}
