using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilites.Results
{
    public class ErrorResult:Result
    {
        public ErrorResult(string message):base(message,false)
        {

        }
        public ErrorResult():base(false)
        {

        }
    }
}
