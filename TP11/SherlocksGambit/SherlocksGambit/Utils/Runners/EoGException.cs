using System;

namespace SherlocksGambit.Utils.Runners
{
    public class EoGException : Exception
    {
        /**
         * <summary> Basic constructor for EoGException </summary>
         * <param name="message"> Message you wish to throw the error with </param>
         * <example>
         * You should throw such exception like so:
         * <code> throw new EoGException("this is my cool message!"); </code>
         * </example>
         */
        public EoGException(string message) : base(message){ }
    }
}