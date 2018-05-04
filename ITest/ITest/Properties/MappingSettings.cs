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
using ITest.Models.ResultsViewModels;
using ITest.Models;
using ITest.Data.Models.Enums;

namespace ITest.Properties
{

    public class MappingSettings : Profile
    {
        public MappingSettings()
        {
            //to create tests
            this.CreateMap<CreateTestViewModel, TestDTO>(MemberList.Source);
            this.CreateMap<CreateCategoryViewModel, CategoryDTO>();
            this.CreateMap<CategoryDTO, Category>();
            this.CreateMap<Category, CategoryDTO>(MemberList.Source);
            this.CreateMap<CreateQuestionViewModel, QuestionDTO>(MemberList.Source);
            this.CreateMap<CreateAnswerViewModel, AnswerDTO>(MemberList.Source);
            //to solve tests
            this.CreateMap<Test, TestDTO>(MemberList.Source);


            this.CreateMap<TestDTO, Test>(MemberList.Source);

            this.CreateMap<TestDTO, ShowTestViewModel>();

            //to Visualize and Edit Test
            this.CreateMap<TestDTO, TestViewModel>(MemberList.Source);
            this.CreateMap<TestViewModel, TestDTO>(MemberList.Source);
            
            this.CreateMap<QuestionDTO, QuestionEditModel>(MemberList.Source);
            this.CreateMap<AnswerDTO, AnswerEditModel>(MemberList.Source);
            this.CreateMap<QuestionEditModel, QuestionDTO>(MemberList.Source);
            this.CreateMap<AnswerEditModel, AnswerDTO>(MemberList.Source);
           
            this.CreateMap<Test, TestEditDTO>(MemberList.Destination)
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            this.CreateMap<TestEditDTO, Test>(MemberList.Source);

            this.CreateMap<Question, QuestionEditDTO>(MemberList.Destination);              
            this.CreateMap<QuestionEditDTO, Question>(MemberList.Source);
            this.CreateMap<Answer, AnswerEditDTO>(MemberList.Destination);           
            this.CreateMap<AnswerEditDTO, Answer>(MemberList.Source);


            this.CreateMap<TestEditDTO, TestViewModel>(MemberList.Source);
            this.CreateMap<TestViewModel, TestEditDTO>(MemberList.Source);
            //this.CreateMap<>


            this.CreateMap<TestDTO, SolveTestDTO>(MemberList.Source);
            this.CreateMap<SolveTestDTO, SolveTestViewModel>(MemberList.Source).ReverseMap();
            this.CreateMap<Question, QuestionDTO>(MemberList.Source);
            this.CreateMap<QuestionDTO, ShowQuestionViewModel>(MemberList.Source);
            this.CreateMap<Answer, AnswerDTO>(MemberList.Source);
            this.CreateMap<AnswerDTO, ShowAnswerViewModel>(MemberList.Source);

            
            

            this.CreateMap<QuestionDTO, Question>(MemberList.Source);
            this.CreateMap<AnswerDTO, Answer>(MemberList.Source);

            this.CreateMap<CategoryDTO, CategoryViewModel>(MemberList.Source).
                ForMember(x => x.Category, opt => opt.MapFrom(src => src.Name));//here
            this.CreateMap<SolveTestViewModel, UserTestsDTO>(MemberList.Source);

            this.CreateMap<SolveTestDTO, UserTestsDTO>(MemberList.Source);
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
            this.CreateMap<UserTestsDTO, ResultsViewModel>(MemberList.Source);

            this.CreateMap<UserTestsDTO, CategoryViewModel>(MemberList.Source);

            this.CreateMap<UserTestsDTO, DetailedTestViewModel>(MemberList.Destination);


        }
    }
}
