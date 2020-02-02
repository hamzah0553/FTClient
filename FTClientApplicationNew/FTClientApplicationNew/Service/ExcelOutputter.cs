using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTClientApplication.Model;
using FTClientApplication.ViewModel.Dk;
using Microsoft.Office.Interop.Excel;

namespace FTClientApplication.Service
{
    sealed class ExcelOutputter
    {
        Workbook workbook;
        Application excel;
        Worksheet worksheet;
        private static ExcelOutputter excelOutputter;

        private ExcelOutputter()
        {
            
        }

        public void CreateExcel()
        {
            excel = new Application();
            workbook = excel.Workbooks.Add(Type.Missing);
            excel.Visible = true;
        }
        public void AddNewSheet(string sheetName)
        {
            Worksheet newWorksheet = new Worksheet(); 
            newWorksheet = workbook.Worksheets.Add();
            newWorksheet.Name = sheetName;
            ChangeCurrentSheet();
        }
        public void AddColumnsToSheet(List<string> columns)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                worksheet.Cells[2, i + 1] = columns[i];
            }
        }

        public void AddDataToSheet(List<List<string>> data)
        {
            int row = 3;
            int column = 0;
            for (int i = 0; i < data.Count; i++)
            {
                column = 1;
                row++;
                for (int j = 0; j < data[i].Count; j++)
                {
                    worksheet.Cells[row, column] = data[i][j];
                    column++;
                }
            }
            worksheet.Columns.AutoFit();
        }
        public void ChangeCurrentSheet()
        {
            worksheet = (Worksheet)workbook.ActiveSheet;
        }

        public static ExcelOutputter GetExcelOutputter()
        {
            if (excelOutputter == null)
            {
                excelOutputter = new ExcelOutputter();
                return excelOutputter;
            }
            return excelOutputter;
        }
    }
}
