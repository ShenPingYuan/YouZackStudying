using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace FluentValidationWebApi.Dtos
{
    public class StudentAddDtoValidator : AbstractValidator<StudentAddDto>
    {
        public StudentAddDtoValidator(IMemoryCache memoryCache)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("名字不能为空")
                .Length(2, 10).WithMessage("名字长度只能是2-10之间")
                .Must(name => name.StartsWith("沈")).WithMessage("名字只能以‘沈’开头");
            var names = memoryCache.Get<List<string>>("names");
            if(names == null)
            {
                names = new List<string>();
            }
            RuleFor(x => x.Name).Must(n =>
            {
                bool nExists=names.Contains(n!);
                if (!nExists)
                {
                    names.Add(n!);
                    memoryCache.Set("names", names);
                }
                return !nExists;
            }).WithMessage(x=>x.Name+"已存在");
        }
    }
}
