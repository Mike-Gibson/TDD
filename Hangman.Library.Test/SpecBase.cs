using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Hangman.Library.Test
{
    public abstract class SpecBase
    {
        public Exception Exception { get; set; }

        protected SpecBase()
        {
            Given();
            try
            {
                When();
            }
            catch (Exception e)
            {
                Exception = e;
            }
        }

        public virtual void When()
        {
        }

        public virtual void Given()
        {
        }
    }

    public class ThenAttribute : TestAttribute
    {
    }
}
