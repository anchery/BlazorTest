using Microsoft.JSInterop;
using OfficeOpenXml;
using System;
using System.Collections.Generic;

namespace BlazorTest.Data
{
    public class ExcelTest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Num { get; set; }
        List<ExcelTest> items;

        public void GenerateExcel(IJSRuntime iJSRuntime)
        {

            try
            {
                byte[] fileContents;

                items = new List<ExcelTest>()
                {
                    new ExcelTest { Id = 1, Name = "John", Num = 10001},
                    new ExcelTest { Id = 2, Name = "Smith", Num = 10002}
                };

                using (var package = new ExcelPackage())
                {
                    var sheet = package.Workbook.Worksheets.Add("sheet");
                    sheet.Cells["C1"].LoadFromCollection(items);
                    fileContents = package.GetAsByteArray();
                }

                iJSRuntime.InvokeAsync<ExcelTest>(
                   "saveFile",
                   "Students.xlsx",
                   Convert.ToBase64String(fileContents)
                   );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}

