using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public interface IDal
{
    ITask Task { get; }
    IEngineer Engineer { get; }
    IDependence Dependence { get; }
    DateTime? StartDateToProject { get; init; }
    DateTime? EndDateToProject { get; init; }

}
