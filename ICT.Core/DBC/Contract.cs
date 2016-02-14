// ---------------------------------------------------------------------------------
// ICT - ICT.Core
// Contract.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using ICT.Core.Exceptions;

namespace ICT.Core.DBC
{
    /// <summary>
    /// Contract class (provider of static services)
    /// </summary>
    public static class Contract
    {
        /// <summary>
        /// Preconditions
        /// </summary>
        /// <param name="condition">Condition to be checked</param>
        /// <param name="userMessage">Message displayed if condition fails</param>
        public static void Requires(bool condition, string userMessage)
        {
            // Do not invoke the original static method! 
            // Note: We don't want to include additional modules at this stage....
            if (!condition)
            {
                throw new CoreException(string.Format("Precondition failed: {0}", userMessage));
            }
        }

        /// <summary>
        /// Postconditions
        /// </summary>
        /// <param name="condition">Condition to be checked</param>
        /// <param name="userMessage">Message displayed if condition fails</param>
        public static void Ensures(bool condition, string userMessage)
        {
            // Do not invoke the original static method! 
            // Note: We don't want to include additional modules at this stage....
            if (!condition)
            {
                throw new CoreException(string.Format("Precondition failed: {0}", userMessage));
            }
        }

        /// <summary>
        /// Assertion
        /// </summary>
        /// <param name="condition">Condition to be checked</param>
        /// <param name="userMessage">Message displayed if condition fails</param>
        public static void Assert(bool condition, string userMessage)
        {
            // Do not invoke the original static method! 
            // Note: We don't want to include additional modules at this stage....
            if (!condition)
            {
                throw new CoreException(string.Format("Assertion failed: {0}", userMessage));
            }
        }
    }
}
