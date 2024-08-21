using AutoMapper;
using Training.Application.Features.Options.Commands.CreateOption;
using Training.Application.Features.Options.Commands.UpdateOption;
using Training.Application.Features.Questions.Commands.CreateQuestion;
using Training.Application.Features.Questions.Commands.DeleteQuestion;
using Training.Application.Features.Questions.Commands.UpdateQuestion;
using Training.Application.Features.Questions.Queries.GetQuestionDetail;
using Training.Application.Features.Questions.Queries.GetQuestionsList;
using Training.Domain.Entities;

namespace Training.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //---------------------------------- Question ----------------------------------
            CreateMap<Question, GetQuestionsListQuery>().ReverseMap();
            CreateMap<Question, GetQuestionDetailQuery>().ReverseMap();
            CreateMap<Question, DeleteQuestionCommand>().ReverseMap();
            CreateMap<Question, QuestionDetailVm>().ReverseMap();
            CreateMap<Question, QuestionListVm>().ReverseMap();

            CreateMap<CreateQuestionCommand, Question>()
                .ForMember(dest => dest.QuestionId, opt => opt.Ignore());

            CreateMap<Question, CreateQuestionDto>().ReverseMap();

            CreateMap<UpdateQuestionCommand, Question>()
                .ForMember(dest => dest.QuestionId, opt => opt.Ignore()) 
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) 
                .ForMember(dest => dest.LastModifiedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<Question, UpdateQuestionDto>().ReverseMap();

            //---------------------------------- Option ----------------------------------
            CreateMap<CreateOptionCommand, Option>()
                .ForMember(dest => dest.OptionId, opt => opt.Ignore())
                .ForMember(dest => dest.IsCorrect, opt => opt.MapFrom(src => src.IsCorrect ?? false));

            CreateMap<Option, CreateOptionDto>();

            CreateMap<Option, OptionDto>().ReverseMap();

            CreateMap<UpdateOptionCommand, Option>()
            .ForMember(dest => dest.OptionId, opt => opt.Ignore()) 
            .ForMember(dest => dest.IsCorrect, opt => opt.Condition(src => src.IsCorrect.HasValue)); 

            CreateMap<Option, UpdateOptionDto>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.IsCorrect, opt => opt.MapFrom(src => src.IsCorrect))
                .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.QuestionId));
        }
    }
    
}
