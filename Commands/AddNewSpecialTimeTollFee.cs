using congestion.calculator.Contracts.Models;
using congestion.calculator.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace congestion.calculator.Commands
{
    public class AddNewSpecialTimeTollFee
    {
        private readonly IAddNewSpecialTimeTollFeeService addNewSpecialTimeTollFeeService;
        private readonly IGetSpecialTimesTollFee getSpecialTimesTollFee;

        public SpecialTimeTollFee SpecialTimeTollFee { get; private set; }


        public AddNewSpecialTimeTollFee(SpecialTimeTollFee specialTimeTollFee,
                                        IAddNewSpecialTimeTollFeeService addNewSpecialTimeTollFeeService,
                                        IGetSpecialTimesTollFee getSpecialTimesTollFee)
        {
            this.addNewSpecialTimeTollFeeService = addNewSpecialTimeTollFeeService;
            this.getSpecialTimesTollFee = getSpecialTimesTollFee;
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
