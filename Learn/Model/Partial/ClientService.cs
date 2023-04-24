using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Model
{
    public partial class ClientService
    {
        public string StartTimes
        {
            get
            {
                string result = "";             
                var startTimeHours = Convert.ToString((Convert.ToDateTime(StartTime) - DateTime.Now).Hours);
                var startTimeMinutes = Convert.ToString((Convert.ToDateTime(StartTime) - DateTime.Now).Minutes);
                result = $"До начала {startTimeHours} часов {startTimeMinutes} минут";
                return result ;
            }
        }
        public string Color
        {
            get
            {
                string result = "";
                if ((Convert.ToDateTime(StartTime) - DateTime.Now).Hours < 1)
                {
                    result = "#ff6666";
                }
                else
                {
                    result = "#000000";
                }
                return result;
            }
        }
        public string FullName
        {
            get
            {               
                return $"{Client.LastName} {Client.FirstName} {Client.Patronymic}";
            }
        }
    }
}
