using WebAPI.Models;

namespace WebAPI.Services
{
    public class ApplicationService
    {
        private readonly AppDbContext _context;

        public ApplicationService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _context.Movies;
        }

        public Movie GetMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.Id == id);
            if (movie == null) return movie;

            return movie;
        }

        public void SaveMovie(MovieDTO dto)
        {
            Movie movie = new Movie
            {
                Title = dto.Title,
                Description = dto.Description,
                Rating = dto.Rating,
                Image = dto.Image,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };

            _context.Movies.Add(movie);
            _context.SaveChanges();
        }

        public bool DeleteMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.Id == id);
            if (movie == null) return false;

            _context.Movies.Remove(movie);
            _context.SaveChangesAsync();
            return true;
        }

        public async Task<Movie> EditMovie(int id, MovieDTO movie)
        {
            var data = _context.Movies.FirstOrDefault(x => x.Id == id);
            if (data == null) return data;

            data.Title = movie.Title;
            data.Description = movie.Description;
            data.Rating = movie.Rating;
            data.Image = movie.Image;
            data.Updated_at = DateTime.Now;

            await _context.SaveChangesAsync();
            return data;
        }
    }
}
