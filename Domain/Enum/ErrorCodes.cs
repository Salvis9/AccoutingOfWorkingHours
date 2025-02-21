using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    public enum ErrorCodes
    {
        // 0-11 = коды ошибок для Report 
        ReportsNotFound = 0,
        ReportNotFound = 1,
        ReportAlreadyExists = 2,
        
        UserNotFound = 11,

        IternalServerError = 10
        
    }
}
