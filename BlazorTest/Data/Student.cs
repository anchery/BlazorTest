using Microsoft.JSInterop;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.Style;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTest.Data
{
    public class Student
    {
        public void GenerateExcel1(IJSRuntime iJSRuntime)
        {
            byte[] fileContents;
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");

                #region Header
                workSheet.Cells[1, 1].Value = "Student Name";
                workSheet.Cells[1, 1].Style.Font.Size = 12;
                workSheet.Cells[1, 1].Style.Font.Bold = true;
                workSheet.Cells[1, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                workSheet.Cells[1, 2].Value = "Student No";
                workSheet.Cells[1, 2].Style.Font.Size = 12;
                workSheet.Cells[1, 2].Style.Font.Bold = true;
                workSheet.Cells[1, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                #endregion

                #region Body
                workSheet.Cells[2, 1].Value = "John Smith";
                workSheet.Cells[2, 1].Style.Font.Size = 12;
                workSheet.Cells[2, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                workSheet.Cells[2, 2].Value = "1001";
                workSheet.Cells[2, 2].Style.Font.Size = 12;
                workSheet.Cells[2, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                #endregion

                fileContents = package.GetAsByteArray();
            }
            iJSRuntime.InvokeAsync<Student>(
                "saveFile",
                "Students.xlsx",
                Convert.ToBase64String(fileContents)
                );
        }

      
    }

}

