// ---------------------------------------------------------------------------------
// ICT - ICT.Core
// CoreException.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using System;

namespace ICT.Core.Exceptions
{
    /// <summary>
    /// Collection-related exception class
    /// </summary>
    public class CoreException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CoreException() 
            : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public CoreException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public CoreException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
