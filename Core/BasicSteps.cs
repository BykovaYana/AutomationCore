using System;

namespace Deloitte.Automation.InvestorTests.Steps
{
    public class BasicSteps
    {
        public virtual TSteps As<TSteps>(bool @unsafe = false) where TSteps : BasicSteps, new()
        {
            try
            {
                return (TSteps)this;
            }
            catch (InvalidCastException)
            {
                if (@unsafe)
                {
                    throw;
                }
                return new TSteps();
            }
        }
    }
}
