using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTClientApplication.ViewModel.Dk
{
    interface IExcelAdapter
    {
        List<List<string>> ConvertData();
        List<string> GetColumnNames();
    }
}
