using System;
using System.Collections.Generic;
using System.Linq;
using AnimalCrossing.Data;
using AnimalCrossing.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalCrossing.Models
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AnimalCrossingContext _context;
        public ReviewRepository(AnimalCrossingContext _context)
        {
            this._context = _context;
        }

        public void Delete(int reviewId)
        {
            _context.Review.Remove(this.Get(reviewId));
            _context.SaveChanges();
        }
        //public int Rating()
        //{
        //    List <Review> review =_context.Review.ToList();
        //    for(int i=0; i<review.Count; i++)
        //    {
        //        review
        //    }
            
        //}

        public List<Review> Find(string search)
        {
            var reviews = from m in _context.Review.Include(x => x.ReviewingCat)
                       select m;

            if (!String.IsNullOrEmpty(search))
            {
                reviews = reviews.Where(r => r.ReviewingCat.Name.Contains(search));
                
            }
            return reviews.ToList();
        }

        public List<Review> Get()
        {
            return _context.Review.Include(x => x.ReviewingCat).ToList();
        }

        public Review Get(int reviewId)
        {
            return _context.Review.Find(reviewId);
        }

        public void Save(Review review)
        {
            if (review.ReviewId == 0)
            {
                _context.Review.Add(review);
            }
            else
            {
                _context.Review.Update(review);
            }
            _context.SaveChanges();
        }
    }
}
