using System.Collections.Generic;
using DM.Application.Contracts.Colleague.Models;
using Framework.Domain;

namespace DM.Domain.ColleagueAgg
{
    public interface IColleagueDiscRepository : IRepository<long,ColleagueDisc>
    {
        EditColleagueDisc GetDiscDetails(long id);
        List<ColleagueDiscViewModel> Search(ColleagueDiscSearchModel searchModel);
    }
}
