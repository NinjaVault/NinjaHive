using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NinjaHive.WebApp.Tests
{

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class ExpectedExceptionWithMessageAttribute : ExpectedExceptionBaseAttribute
    {

        public ExpectedExceptionWithMessageAttribute(Type exceptionType,string messageExpected):
            base()
        {
            ExceptionType = exceptionType;
            ExceptionMessage = messageExpected;
        }
        public ExpectedExceptionWithMessageAttribute(Type exceptionType, string messageExpected, string noExceptionMessage):
            base(noExceptionMessage)
        {
            ExceptionType = exceptionType;
            ExceptionMessage = messageExpected;
        }

        public bool MatchSubstring { get; set; }
        public bool AllowDerivedTypes { get; set; }
        public Type ExceptionType { get; private set; }
        public string ExceptionMessage { get; private set; }


        protected override void Verify(Exception exception)
        {
            RethrowIfAssertException(exception);
            try
            {
                Assert.IsInstanceOfType(exception, ExceptionType, "Exception type does not match.");
                if (MatchSubstring)
                    Assert.IsTrue(exception.Message.Contains(ExceptionMessage),
                        string.Format("Exception message does not contain the expected string. Given: {0} | Expected: {1}",
                            exception.Message,
                            ExceptionMessage));
                else
                    Assert.AreEqual(exception.Message, ExceptionMessage,
                        string.Format("Exception message does not match the expected string. Given: {0} | Expected: {1}",
                            exception.Message,
                            ExceptionMessage));
            }
            catch (AssertFailedException err)
            {
                RethrowIfAssertException(err);
            }
        }
    }
}
