using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockBot.Helpers
{
    public class CalcularIdade
    {
        public static int Calcular(DateTime DataNascimento)
        {
            int anos = DateTime.Now.Year - DataNascimento.Year;

            if (DateTime.Now.Month < DataNascimento.Month || (DateTime.Now.Month == DataNascimento.Month && DateTime.Now.Day < DataNascimento.Day))
                anos--;

            return anos;
        }
    }
}