using congestion.calculator.Contracts.Models;
using congestion.calculator.Queries.Services;
using System;

namespace congestion.calculator.Commands
{
    public class AddNewSpecialTimeTollFee
    {
        private readonly IAddNewSpecialTimeTollFeeService _addNewSpecialTimeTollFeeService;
        private readonly IGetSpecialTimesTollFee _getSpecialTimesTollFee;

        public SpecialTimeTollFee SpecialTimeTollFee { get; private set; }


        public AddNewSpecialTimeTollFee(SpecialTimeTollFee specialTimeTollFee,
                                        IAddNewSpecialTimeTollFeeService addNewSpecialTimeTollFeeService,
                                        IGetSpecialTimesTollFee getSpecialTimesTollFee)
        {
            _addNewSpecialTimeTollFeeService = addNewSpecialTimeTollFeeService;
            _getSpecialTimesTollFee = getSpecialTimesTollFee;
            SetSpecialTollFeeTime(specialTimeTollFee, getSpecialTimesTollFee);

            addNewSpecialTimeTollFeeService.AddNewSpecialTimeTollFee(specialTimeTollFee);
        }

        private void SetSpecialTollFeeTime(SpecialTimeTollFee specialTimeTollFee, IGetSpecialTimesTollFee getSpecialTimesTollFee)
        {
            if (specialTimeTollFee.FromTime >= specialTimeTollFee.ToTime)
            {
                throw new Exception("Invalid fromtime and toTime");
            }
            if (getSpecialTimesTollFee.GetFee(specialTimeTollFee.FromTime) != 0)
            {
                throw new Exception("This Time has toll fee");
            }
            SpecialTimeTollFee = specialTimeTollFee;
        }

    }
}
