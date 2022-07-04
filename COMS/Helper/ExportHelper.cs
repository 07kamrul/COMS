using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

namespace COMS.Helper
{
    public class ExportHelper<T>
    {
        private readonly HttpResponse _response;

        public ExportHelper(HttpResponse response)
        {
            _response = response;
        }

/*        public void ExportToPdf(List<T> exportData, string header)
        {
            byte[] bytes = exportData.ToPdf(
                scheme =>
                {
                    scheme.HeaderFontBold = true;
                    scheme.HeaderFontSize = 25;
                    scheme.Header = header;
                    scheme.HeaderHeight = 15;
                    scheme.HeaderAlignment = ArrayToPdfAlignments.Center;
                    scheme.PageMarginTop = 10;
                });
            _response.Clear();
            _response.Headers.Add("content-diposition", "attachment;filename=Export.pdf");
            _response.ContentType = "application/pdf";
            _response.Body.WriteAsync(bytes, 0, bytes.Length);
        }*/

        public void ExportToExcel(List<T> exportData, string header)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using(MemoryStream stream = new MemoryStream())
            using (var excelFile = new ExcelPackage(stream))
            {
                var worksheet = excelFile.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells["A2"].LoadFromCollection(Collection: exportData, PrintHeaders: true);
                worksheet.Cells[1, 1, 1, worksheet.Dimension.Columns].Merge = true;
                worksheet.Cells[1, 1, 1, worksheet.Dimension.Columns].Style.Font.Bold = true;
                worksheet.Cells[1, 1, 1, worksheet.Dimension.Columns].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, 1, 1, worksheet.Dimension.Columns].Style.Font.Size = 25;
                worksheet.Cells["A1"].Value = header;

                using(var range = worksheet.Cells[2,1,2, worksheet.Dimension.Columns])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.Black);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                worksheet.Cells[worksheet.Dimension.Address].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[worksheet.Dimension.Address].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[worksheet.Dimension.Address].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[worksheet.Dimension.Address].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                _response.Clear();
                _response.Headers.Add("content-disposition", "attachment; filename=Export.xlsx");
                _response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var bytes = excelFile.GetAsByteArray();
                _response.Body.WriteAsync(bytes, 0, bytes.Length);

            }
        }
    }
}
