using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqStudy
{
    public interface ICalculator
    {
        int Add(int a, int b);
    }


    public class AddClass : ICalculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }

    public class Calculator
    {
        public int Test(ICalculator add, int a, int b)
        {
            ICalculator test = (ICalculator)new AddClass();
            return test.Add(a, b);
        }
    }
}
