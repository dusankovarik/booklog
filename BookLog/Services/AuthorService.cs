using BookLog.Dtos;
using BookLog.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<AuthorDto?> GetByIdAsync(int id) {
            var author = await _dbContext.Authors.FindAsync(id);
            return author != null ? ModelToDto(author) : null;
        }

        public async Task UpdateAsync(AuthorDto authorDto) {
            if (authorDto.Id == null) {
                throw new ArgumentNullException("Author Id is required for update");
            }
            var authorToEdit = await _dbContext.Authors.FindAsync(authorDto.Id.Value);
            if (authorToEdit == null) {
                throw new ArgumentNullException("Author not found");
            }
            authorToEdit.FirstName = authorDto.FirstName;
            authorToEdit.MiddleName = authorDto.MiddleName;
            authorToEdit.LastName = authorDto.LastName;
            authorToEdit.Nationality = authorDto.Nationality;
            authorToEdit.YearOfBirth = authorDto.YearOfBirth;
            authorToEdit.YearOfDeath = authorDto.YearOfDeath;
            authorToEdit.DatabazeKnihUrl = authorDto.DatabazeKnihUrl;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id) {
            var studentToDelete = await _dbContext.Authors.FindAsync(id);
            if (studentToDelete == null) {
                return false;
            }
            _dbContext.Authors.Remove(studentToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
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
