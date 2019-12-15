using System;
using System.Collections.Generic;

namespace AnimalCrossing.Models
{
        public interface IReviewRepository
        {
            public void Save(Review c);

            public List<Review> Get();
            public Review Get(int reviewId);
            public void Delete(int reviewId);

            public List<Review> Find(string search);
        }
}
