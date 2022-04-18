using System.ComponentModel.DataAnnotations;

namespace FluentValidationWebApi.Dtos;
public class StudentAddDto
{
    public int Age { get; set; }
    //[MaxLength(100)]
    public string? Name { get; set; }
    public bool Gender { get; set; }
}
