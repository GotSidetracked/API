using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KTU_API.Data.Dtos.Comments;
using KTU_API.Data.Dtos.Needed_Gears;
using KTU_API.Data.Dtos.Paths;
using KTU_API.Data.Dtos.Users;
using KTU_API.Data.Entities;

namespace KTU_API.Data
{
    public class RestProfile: Profile
    {
        public RestProfile()
        {
            CreateMap<Path, PathDto>();
            CreateMap<CreatePathDto, Path>();
            CreateMap<UpdatePathDto, Path>();

            CreateMap<Needed_Gear, NeededGearDto>();
            CreateMap<CreateNeededGearDto, Needed_Gear>();
            CreateMap<UpdateNeededGearDto, Needed_Gear>();

            CreateMap<Comment, CommentDto>();
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<UpdateCommentDto, Comment>();

            CreateMap<User, UserDto>();
            CreateMap<CreateUsersDto, User>();
            CreateMap<UpdateUserDto, User>();
        }
    }
}