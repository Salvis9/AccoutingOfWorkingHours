﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    public enum ErrorCodes
    {
        // 0-9 = коды ошибок для Report 
        ReportsNotFound = 0,
        ReportNotFound = 1,
        ReportAlreadyExists = 2,
        ReportInactive = 3,

        // 10-19 = коды ошибок для User
        UserNotFound = 10,
        PasswordNotEqualsPasswordConfirm = 11,
        UserAlreadyExists = 12,
        PasswordIsWrong = 13,

        IternalServerError = 19,

        // 20-29 = коды ошибок для TaskEntity 
        TasksEntityNotFound = 20,
        TaskEntityNotFound = 21,
        TaskEntityAlreadyExists = 22,
        TaskInactive = 23,

        // 30-39 = коды ошибок для TimeSheet
        TimeSheetsNotFound = 30,
        TimeSheetNotFound = 31,
        TimeSheetAlreadyExists = 32,


    }
}
