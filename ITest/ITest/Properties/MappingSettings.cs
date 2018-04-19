using AutoMapper;
using ITest.Data.Models;
using ITest.DTO;
using ITest.Models.CategoryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITest.Properties
{

    public class MappingSettings : Profile
    {
        public MappingSettings()
        {



            this.CreateMap<CreateCategoryViewModel, CategoryDTO>();
            this.CreateMap<CategoryDTO, Category>();

            //this.CreateMap<CommentDto, CommentViewModel>()
            //       .ForMember(x => x.Author, options => options.MapFrom(x => x.Author.Email));

            //this.CreateMap<PostViewModel, PostDto>(MemberList.Source);
            //this.CreateMap<PostDto, Post>(MemberList.Source);
            //this.CreateMap<CommentDto, Comment>(MemberList.Source);
        }
    }

}
