namespace CW21Ta23.Domain.Dto;

public class CreateAuthorDto
{
    public string FullName { get; set; }

    public DateTime? BirthDate { get; set; }

    public string Country { get; set; }
}