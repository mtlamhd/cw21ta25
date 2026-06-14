namespace CW21Ta23.Domain.Dto;

public class AuthorWithBookDto
{
    
        public string AuthorName { get; set; }

        public string Country { get; set; }

        public List<string> Books { get; set; }
        
      // public bool HasAnyBooks { get; set; }
    
}