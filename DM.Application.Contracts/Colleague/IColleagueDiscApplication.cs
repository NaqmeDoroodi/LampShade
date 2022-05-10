using System.Collections.Generic;
using DM.Application.Contracts.Colleague.Models;
using Framework.Application;

namespace DM.Application.Contracts.Colleague
{
    public interface IColleagueDiscApplication
    {
        OperationResult Create(CreateColleagueDisc disc);
        OperationResult Edit(EditColleagueDisc disc);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        EditColleagueDisc GetDiscDetails(long id);
        List<ColleagueDiscViewModel> Search(ColleagueDiscSearchModel searchModel);
    }
}
