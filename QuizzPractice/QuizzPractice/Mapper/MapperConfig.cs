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

            CreateMap<Question, CreateQuestionRequest>().ReverseMap();
            CreateMap<Question, UpdateQuestionRequest>().ReverseMap();
              

            CreateMap<Option, OptionRequest>().ReverseMap();
            CreateMap<Option, OptionResponse>().ReverseMap();

            CreateMap<Option, UpdateOptionRequest>().ReverseMap();

            CreateMap<Question, GetQuestionResponse>().ReverseMap();

            CreateMap<Result, AddResultRequest>().ReverseMap();
            CreateMap<Result, UpdateResultRequest>().ReverseMap();
            CreateMap<Answer, AnswerRequest>().ReverseMap();
            CreateMap<Quiz, QuizResponse>().ReverseMap();
            CreateMap<Answer, AnswerResponse>().ReverseMap();
            CreateMap<Result, GetResultsResponse>().ReverseMap();

            CreateMap<User, GetUserResponse>().ReverseMap();
            CreateMap<User, RegisterRequest>().ReverseMap();
            CreateMap<User, UpdateUserRequest>().ReverseMap();


        }
    }
}
