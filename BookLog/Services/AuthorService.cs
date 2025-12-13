using BookLog.Dtos;
using BookLog.Models;

namespace BookLog.Services {
    public class AuthorService {
        private ApplicationDbContext _dbContext;

        public AuthorService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IEnumerable<AuthorDto> GetAll() {
            var allAuthors = _dbContext.Authors;
            var authorDtos = new List<AuthorDto>();
            foreach (var author in allAuthors) {
                authorDtos.Add(ModelToDto(author));
            }
            return authorDtos;
        }

        public async Task CreateAsync(AuthorDto newAuthor) {
            await _dbContext.Authors.AddAsync(DtoToModel(newAuthor));
            await _dbContext.SaveChangesAsync();
        }

        private AuthorDto ModelToDto(Author author) {
            return new AuthorDto() {
                Id = author.Id,
                FirstName = author.FirstName,
                MiddleName = author.MiddleName,
                LastName = author.LastName,
                Nationality = author.Nationality,
                YearOfBirth = author.YearOfBirth,
                YearOfDeath = author.YearOfDeath,
                DatabazeKnihUrl = author.DatabazeKnihUrl,
            };
        }

        private Author DtoToModel(AuthorDto authorDto) {
            return new Author() {
                FirstName = authorDto.FirstName,
                MiddleName = authorDto.MiddleName,
                LastName = authorDto.LastName,
                Nationality = authorDto.Nationality,
                YearOfBirth= authorDto.YearOfBirth,
                YearOfDeath= authorDto.YearOfDeath,
                DatabazeKnihUrl = authorDto.DatabazeKnihUrl,
            };
        }
    }
}
