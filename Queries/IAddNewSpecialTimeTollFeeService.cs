using congestion.calculator.Contracts.Models;

namespace congestion.calculator.Queries
{
    public interface IAddNewSpecialTimeTollFeeService
    {
        void AddNewSpecialTimeTollFee(SpecialTimeTollFee specialTimeTollFee);
    }
}