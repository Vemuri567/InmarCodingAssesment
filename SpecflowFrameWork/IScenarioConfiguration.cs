using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowFramework
{
    public interface IScenarioConfiguration
    {
        DataSet Data { get; }
        bool LoadData();
    }

    public abstract class BaseScenarioConfiguration : IScenarioConfiguration
    {
        public virtual DataSet Data => new DataSet();
        public virtual bool LoadData()
        {
            return true;
        }
    }
}
