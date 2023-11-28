using congestion.calculator.Contracts.Models;

namespace congestion.calculator.Queries.Services
{
    public interface IAddNewSpecialTimeTollFeeService
    {
        void AddNewSpecialTimeTollFee(SpecialTimeTollFee specialTimeTollFee);
    }
}