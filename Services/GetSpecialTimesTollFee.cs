using congestion.calculator.Queries;
using System;

namespace congestion.calculator.Services
{
    public class GetSpecialTimesTollFee : IGetSpecialTimesTollFee
    {
        private readonly IRepository _repository;

        public GetSpecialTimesTollFee(IRepository repository)
        {
            _repository = repository;
        }
        public int GetFee(TimeSpan timeOfDate)
        {

            return _repository.GetSpecialTimeTollFee(timeOfDate);
        }
    }
}