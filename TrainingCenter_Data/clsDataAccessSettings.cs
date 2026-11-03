using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TrainingCenter_Data
{
    internal class clsDataAccessSettings
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["TrainingCenterDB"].ConnectionString;
    }
}
