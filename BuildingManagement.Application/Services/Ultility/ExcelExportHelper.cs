using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Services.Ultility
{
    public static class ExcelExportHelper
    {

        public static byte[] ExportToExcel<T>(
        List<T> data,
        List<ExcelColumnConfig> columnConfigs,
        string sheetName = "Sheet1",
        string title = null)
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add(sheetName);

            int startRow = 1;

            // Thêm title nếu có
            if (!string.IsNullOrEmpty(title))
            {
                worksheet.Cells[1, 1].Value = title;
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells[1, 1].Style.Font.Size = 16;
                worksheet.Cells[1, 1, 1, columnConfigs.Count].Merge = true;
                worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                startRow = 2;
            }

            // Tạo headers
            for (int i = 0; i < columnConfigs.Count; i++)
            {
                var config = columnConfigs[i];
                worksheet.Cells[startRow, i + 1].Value = config.HeaderName;

                // Style cho header
                var headerCell = worksheet.Cells[startRow, i + 1];
                headerCell.Style.Font.Bold = true;
                headerCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                headerCell.Style.Fill.BackgroundColor.SetColor(config.HeaderBackgroundColor);
                headerCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                headerCell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }

            // Fill dữ liệu
            var dataList = data.ToList();
            for (int row = 0; row < dataList.Count; row++)
            {
                var item = dataList[row];
                var excelRow = startRow + row + 1;

                for (int col = 0; col < columnConfigs.Count; col++)
                {
                    var config = columnConfigs[col];
                    var cell = worksheet.Cells[excelRow, col + 1];

                    // Lấy giá trị theo property name hoặc selector
                    object value = null;
                    if (config.ValueSelector != null)
                    {
                        value = config.ValueSelector(item);
                    }
                    else if (!string.IsNullOrEmpty(config.PropertyName))
                    {
                        value = GetPropertyValue(item, config.PropertyName);
                    }

                    // Set giá trị và format
                    if (value != null)
                    {
                        if (!string.IsNullOrEmpty(config.Format))
                        {
                            if (value is DateTime dateValue)
                            {
                                cell.Value = dateValue;
                                cell.Style.Numberformat.Format = config.Format;
                            }
                            else if (IsNumericType(value.GetType()))
                            {
                                cell.Value = value;
                                cell.Style.Numberformat.Format = config.Format;
                            }
                            else
                            {
                                cell.Value = string.Format(config.Format, value);
                            }
                        }
                        else
                        {
                            cell.Value = value;
                        }
                    }

                    // Apply styles
                    if (config.Width.HasValue)
                    {
                        worksheet.Column(col + 1).Width = config.Width.Value;
                    }

                    cell.Style.HorizontalAlignment = config.HorizontalAlignment;
                    cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }
            }

            // Auto-fit columns nếu không có width cụ thể
            for (int i = 0; i < columnConfigs.Count; i++)
            {
                if (!columnConfigs[i].Width.HasValue)
                {
                    worksheet.Column(i + 1).AutoFit();
                }
            }

            // Add borders cho toàn bộ dữ liệu
            var totalRows = startRow + dataList.Count;
            var range = worksheet.Cells[startRow, 1, totalRows, columnConfigs.Count];
            ApplyBorders(range);

            return package.GetAsByteArray();
        }

        private static object GetPropertyValue(object obj, string propertyName)
        {
            var properties = propertyName.Split('.');
            object value = obj;

            foreach (var prop in properties)
            {
                if (value == null) return null;
                var propertyInfo = value.GetType().GetProperty(prop);
                value = propertyInfo?.GetValue(value);
            }

            return value;
        }

        private static bool IsNumericType(Type type)
        {
            return type == typeof(int) || type == typeof(long) || type == typeof(float) ||
                   type == typeof(double) || type == typeof(decimal) ||
                   type == typeof(int?) || type == typeof(long?) || type == typeof(float?) ||
                   type == typeof(double?) || type == typeof(decimal?);
        }

        private static void ApplyBorders(ExcelRange range)
        {
            range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }
    }
}
