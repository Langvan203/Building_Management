﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Ultility
{
    public class ConvertNumberToString
    {
        public static string ConvertMonth(int month)
        {
            if(month == 1)
                return "Jan";
            else if (month == 2)
                return "Feb";
            else if (month == 3)
                return "Mar";
            else if (month == 4)
                return "Apr";
            else if (month == 5)
                return "May";
            else if (month == 6)
                return "Jun";
            else if (month == 7)
                return "Jul";
            else if (month == 8)
                return "Aug";
            else if (month == 9)
                return "Sep";
            else if (month == 10)
                return "Oct";
            else if (month == 11)
                return "Nov";
            else
                return "Dec";
        }
    }
}
