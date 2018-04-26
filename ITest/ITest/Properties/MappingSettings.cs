using AutoMapper;
using ITest.Models.AnswerViewModels;
using ITest.Models.QuestionViewModel;
using ITest.Data.Models;
using ITest.DTO;
using ITest.Models.CategoryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITest.Models.TestViewModels;

namespace ITest.Properties
{

    public class MappingSettings : Profile
    {
        public MappingSettings()
        {
            this.CreateMap<CreateCategoryViewModel, CategoryDTO>();
            this.CreateMap<CategoryDTO, Category>();
            //to solve tests
            this.CreateMap<Test, TestDTO>(MemberList.Source);
            this.CreateMap<TestDTO, SolveTestViewModel>(MemberList.Source);

            this.CreateMap<Question, QuestionDTO>(MemberList.Source);
            this.CreateMap<QuestionDTO, ShowQuestionViewModel>(MemberList.Source);

            this.CreateMap<Answer, AnswerDTO>(MemberList.Source);
            this.CreateMap<AnswerDTO, ShowAnswerViewModel>(MemberList.Source);
            //this.CreateMap<CommentDto, CommentViewModel>()
            //       .ForMember(x => x.Author, options => options.MapFrom(x => x.Author.Email));


            this.CreateMap<CreateQuestionViewModel, QuestionDTO>(MemberList.Source);
            this.CreateMap<CreateAnswerViewModel, AnswerDTO>(MemberList.Source);


            this.CreateMap<QuestionDTO, Question>(MemberList.Source);
            this.CreateMap<AnswerDTO, Answer>(MemberList.Source);


            this.CreateMap<Category, CategoryDTO>(MemberList.Source);
            this.CreateMap<CategoryDTO, CategoryViewModel>(MemberList.Source);
            this.CreateMap<SolveTestViewModel, UserTestsDTO>(MemberList.Source);
            this.CreateMap<UserTestsDTO, UserTests>(MemberList.Source).
                ForMember(x => x.SerializedAnswers, opt => opt.MapFrom(src => string.Join(";", src.StorageOfAnswers)));

            this.CreateMap<UserTests, UserTestsDTO>(MemberList.Source).
                ForMember
                (
                    x => x.StorageOfAnswers, opt => opt.MapFrom
                        (
                        src => src.SerializedAnswers.Split(new char[] { ';' }).ToList()
                        )
                );
        }
    }
}
