using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FTClientApplication.ViewModel.Dk;

namespace FTClientApplication.Service
{
    public class ExcelAssembler
    {
        ExcelOutputter excel = ExcelOutputter.GetExcelOutputter();

        public ExcelAssembler()
        {
            excel.CreateExcel();
        }
        public void OutputToExcel()
        {
            ExportMayorData();
            ExportParliamentData();
            ExportMinisters();
            ExportSelectionData();
        }

        private void ExportMayorData()
        {
            IExcelAdapter mayorAdapter = new MayorVM();
            excel.AddNewSheet("Borgmestre");
            excel.AddColumnsToSheet(mayorAdapter.GetColumnNames());
            excel.AddDataToSheet(mayorAdapter.ConvertData());
        }
        private void ExportParliamentData()
        {
            IExcelAdapter parliamentAdapter = new ParliamentVM();
            excel.AddNewSheet("Folketinget");
            excel.AddColumnsToSheet(parliamentAdapter.GetColumnNames());
            excel.AddDataToSheet(parliamentAdapter.ConvertData());
        }

        private void ExportMinisters()
        {
            IExcelAdapter governmentAdapter = new GovernmentVM();
            excel.AddNewSheet("ministre");
            excel.AddColumnsToSheet(governmentAdapter.GetColumnNames());
            excel.AddDataToSheet(governmentAdapter.ConvertData());
        }

        private void ExportSelectionData()
        {
            IExcelAdapter selectionAdapter = new SelectionVM();
            excel.AddNewSheet("Udvalg");
            excel.AddColumnsToSheet(selectionAdapter.GetColumnNames());
            excel.AddDataToSheet(selectionAdapter.ConvertData());
        }

    }
}
