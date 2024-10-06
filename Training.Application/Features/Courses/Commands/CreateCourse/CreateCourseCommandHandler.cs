using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Application.Contracts.Persistence;
using Training.Application.Responses;
using Training.Domain.Entities;

namespace Training.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandHandler
        : IRequestHandler<CreateCourseCommand, BaseResponse<Guid>>
    {
        private readonly IAsyncRepository<Course> _courseRepository;
        private readonly IMapper _mapper;

        public CreateCourseCommandHandler(IAsyncRepository<Course> courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = _mapper.Map<Course>(request);

            var data = await _courseRepository.AddAsync(course);

            return new BaseResponse<Guid>("", true, 200,data.CourseId);
        }
    }
}
