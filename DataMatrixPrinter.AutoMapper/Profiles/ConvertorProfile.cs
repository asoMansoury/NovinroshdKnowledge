using AutoMapper;

namespace DataMatrixPrinter.AutoMapper.Profiles
{
    public class ConvertorProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<string, string>().ConvertUsing(
                str => !string.IsNullOrEmpty(str) ? str.Trim() : null
            );
        }
    }
}
