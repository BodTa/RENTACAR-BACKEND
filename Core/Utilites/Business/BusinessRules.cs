using Core.Utilites.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilites.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics) // params yazınca istenilen öğeden sınırsız gönderebiliyoruz.
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;
                }
            }
            return null;

        }
    }
}
