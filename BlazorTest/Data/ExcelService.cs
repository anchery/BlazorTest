using BlazorTest.Models;
using Microsoft.JSInterop;
using OfficeOpenXml;
using System;
using System.Collections.Generic;

namespace BlazorTest.Data
{
    public class ExcelService
    {
        public void GenerateExcel(IJSRuntime iJSRuntime, List<DataExportModel> obs)
        {
            try
            {
                byte[] fileContents;

                using (var package = new ExcelPackage())
                {
                    var sheet = package.Workbook.Worksheets.Add("sheet");
                    sheet.Cells["A1"].LoadFromCollection(obs,true);

                    //fix headers
                    for (int i = 66; i < 90; i++)
                    {
                        if (sheet.Cells[Convert.ToChar(i) + "1"].Value != null)
                        {
                            sheet.Cells[Convert.ToChar(i) + "1"].Value = sheet.Cells[Convert.ToChar(i) + "1"].Value.ToString().Replace(" ", "_");
                            //sheet.Cells[Convert.ToChar(i) + "1"].Value = sheet.Cells[Convert.ToChar("B") + "1"].Value.ToString().Substring(0, sheet.Cells[Convert.ToChar("B") + "1"].Value.ToString().Length - 1) + "." +
                                //sheet.Cells[Convert.ToChar(i) + "1"].Value.ToString().Substring(sheet.Cells[Convert.ToChar(i) + "1"].Value.ToString().Length - 1);
                        }
                    }

                    fileContents = package.GetAsByteArray();
                }

                //Save
                iJSRuntime.InvokeAsync<ExcelService>(
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

