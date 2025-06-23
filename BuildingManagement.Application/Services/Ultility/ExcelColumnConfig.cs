using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Services.Ultility
{
    public class ExcelColumnConfig
    {
        public string HeaderName { get; set; } // Tên hiển thị trên header
        public string PropertyName { get; set; } // Tên property của object
        public Func<object, object> ValueSelector { get; set; } // Hàm lấy giá trị tùy chỉnh
        public string Format { get; set; } // Format hiển thị (ví dụ: "dd/MM/yyyy", "#,##0")
        public double? Width { get; set; } // Độ rộng cột
        public ExcelHorizontalAlignment HorizontalAlignment { get; set; } = ExcelHorizontalAlignment.Left;
        public Color HeaderBackgroundColor { get; set; } = Color.LightGray;

        // Constructor đơn giản
        public ExcelColumnConfig(string headerName, string propertyName)
        {
            HeaderName = headerName;
            PropertyName = propertyName;
        }

        // Constructor với selector
        public ExcelColumnConfig(string headerName, Func<object, object> valueSelector)
        {
            HeaderName = headerName;
            ValueSelector = valueSelector;
        }

        // Constructor đầy đủ
        public ExcelColumnConfig(string headerName, string propertyName, string format = null, double? width = null, ExcelHorizontalAlignment alignment = ExcelHorizontalAlignment.Left)
        {
            HeaderName = headerName;
            PropertyName = propertyName;
            Format = format;
            Width = width;
            HorizontalAlignment = alignment;
        }
    }
}
