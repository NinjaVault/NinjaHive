using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NinjaHive.WebApp.Tests.Exceptions
{

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ExpectedExceptionWithMessageAttribute : ExpectedExceptionBaseAttribute
    {

        public ExpectedExceptionWithMessageAttribute(
            Type exceptionType,
            string messageExpected)
        {
            this.ExceptionType = exceptionType;
            this.ExceptionMessage = messageExpected;
        }

        public ExpectedExceptionWithMessageAttribute(
            Type exceptionType,
            string messageExpected,
            string noExceptionMessage)
            : base(noExceptionMessage)
        {
            this.ExceptionType = exceptionType;
            this.ExceptionMessage = messageExpected;
        }

        public bool MatchSubstring { get; set; }
        public bool AllowDerivedTypes { get; set; }
        public Type ExceptionType { get; private set; }
        public string ExceptionMessage { get; private set; }


        protected override void Verify(Exception exception)
        {
            this.RethrowIfAssertException(exception);

            try
            {
                Assert.IsInstanceOfType(exception, this.ExceptionType, "Exception type does not match.");
                if (this.MatchSubstring)
                {
                    Assert.IsTrue(exception.Message.Contains(this.ExceptionMessage),
                        string.Format(
                            "Exception message does not contain the expected string. Given: {0} | Expected: {1}",
                            exception.Message,
                            this.ExceptionMessage));
                }
                else
                {
                    Assert.AreEqual(exception.Message, this.ExceptionMessage,
                        string.Format("Exception message does not match the expected string. Given: {0} | Expected: {1}",
                            exception.Message,
                            this.ExceptionMessage));
                }
            }
            catch (AssertFailedException failedException)
            {
                this.RethrowIfAssertException(failedException);
            }
        }
    }
}
