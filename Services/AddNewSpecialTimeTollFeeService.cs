using congestion.calculator.Contracts.Models;
using congestion.calculator.Queries;
using System;

namespace congestion.calculator.Services
{
    public class AddNewSpecialTimeTollFeeService : IAddNewSpecialTimeTollFeeService
    {
        private readonly IRepository _repository;

        public AddNewSpecialTimeTollFeeService(IRepository repository)
        {
            _repository = repository;
        }
        public void AddNewSpecialTimeTollFee(SpecialTimeTollFee specialTimeTollFee)
        {
            _repository.AddNewSpecialTimeTollFee(specialTimeTollFee);
        }
    }
}