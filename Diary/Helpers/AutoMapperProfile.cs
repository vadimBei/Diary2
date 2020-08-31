using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Diary.Entities.DTOs.Account;
using Diary.Entities.DTOs.Invite;
using Diary.Entities.DTOs.Record;
using Diary.Entities.DTOs.UploadedFile;
using Diary.Entities.Models;

namespace Diary.Helpers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<UserRegisterDTO, User>();
			CreateMap<UserUpdateDTO, User>();
			CreateMap<User, UserUpdateDTO>();
			CreateMap<User, UserViewDTO>();
			CreateMap<User, UserChangePasswordDTO>();
			CreateMap<User, UserNewPasswordDTO>();

			CreateMap<InviteCreateDTO, Invite>();

			CreateMap<UploadedFileCreateDTO, UploadedFile>();
			CreateMap<UploadedFile, UploadedFileViewDTO>();

			CreateMap<RecordCreateDTO, Record>();
			CreateMap<RecordUpdateDTO, Record>();
			CreateMap<Record, RecordUpdateDTO>()
				.ForMember(d=>d.UploadedFileViewDTOs, o=>o.MapFrom(s=>s.UploadedFiles));
			CreateMap<Record, RecordViewDTO>();
				//.ForMember(d => d.UploadedFileViewDTOs, o => o.MapFrom(s => s.UploadedFiles));

		}
	}
}
