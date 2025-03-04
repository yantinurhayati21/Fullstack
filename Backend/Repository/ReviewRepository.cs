using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Dtos;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ReviewDisplayDto> GetAll()
{
    return _context.Review
        .Include(r => r.Film)
        .Select(r => new ReviewDisplayDto
        {
            Id = r.Id,
            ReviewerName = r.ReviewerName,
            Comment = r.Comment,
            Rating = r.Rating,
            FilmId = r.FilmId,
            FilmTitle = r.Film.Title  // Map film title
        })
        .ToList();
}

        public Review GetById(int id)
        {
            return _context.Review.Find(id);
        }

        public Review Create(Review review)
        {
            _context.Review.Add(review);
            _context.SaveChanges();
            return review;
        }

        public void Update(Review review)
        {
            _context.Review.Update(review);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var review = _context.Review.Find(id);
            if (review != null)
            {
                _context.Review.Remove(review);
                _context.SaveChanges();
            }
        }
    }
}