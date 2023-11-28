using congestion.calculator.Queries;
using System;

namespace congestion.calculator.Services
{
    public class SpecialTimesTollFee : ISpecialTimesTollFee
    {
        private readonly IRepository _repository;

        public SpecialTimesTollFee(IRepository repository)
        {
            _repository = repository;
        }
        public int GetFee(TimeSpan timeOfDate)
        {

            return _repository.GetSpecialTimeTollFee(timeOfDate);
        }
    }
}