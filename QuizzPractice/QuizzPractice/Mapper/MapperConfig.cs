using AutoMapper;
using QuizzPractice.Db.Models;
using QuizzPractice.DTOs.Request;
using QuizzPractice.DTOs.Response;

namespace QuizzPractice.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<User, UserResponse>().ReverseMap();
            CreateMap<Subject, SubjectResponse>().ReverseMap();

            CreateMap<Question, QuestionResponse>().ReverseMap();
            CreateMap<Quiz, GetQuizResponse>().ReverseMap();

            CreateMap<Quiz, CreateQuizRequest>().ReverseMap();


            CreateMap<Quiz, UpdateQuizRequest>().ReverseMap();

            CreateMap<Subject, CreateSubjectRequest>().ReverseMap();
            CreateMap<Subject, UpdateSubjectRequest>().ReverseMap();
            CreateMap<Subject, GetSubjectResponse>().ReverseMap();

        }
    }
}
