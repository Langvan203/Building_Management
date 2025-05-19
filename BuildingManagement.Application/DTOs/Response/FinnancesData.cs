using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Response
{
    public class FinnancesData
    {
        public int TotalRevenue { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public decimal OutstandingPayments { get; set; }
        public decimal CollectionRate { get; set; }
        public decimal RevenueGrowth { get; set; }
        public decimal revenuePaid { get; set; }
        public decimal MonthlyExpenses { get; set; }
        public List<RevenueByMonth> RevenueByMonth { get; set; }
        public List<ExpenseCategory> ExpenseCategories { get; set; }
        public List<OverduePayment> OverduePayments { get; set; }

    }

    public class RevenueByMonth
    {
        public string Month { get; set; }
        public int Revenue { get; set; }
    }

    public class ExpenseCategory
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class OverduePayment
    {
        public string Id { get; set; }
        public string Apartment { get; set; }
        public string Resident { get; set; }
        public int Amount { get; set; }
        public DateTime DueDate { get; set; }   
        public int DaysOverdue { get; set; }
    }
}
