// <copyright file="ContentNotFoundException.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// An exception thrown when content was expected, but was not found.
    /// </summary>
    public class ContentNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentNotFoundException"/> class.
        /// </summary>
        public ContentNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message for the exception.</param>
        public ContentNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message for the exception.</param>
        /// <param name="innerException">The orgiinal exception that resulted in this exception.</param>
        public ContentNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentNotFoundException"/> class.
        /// </summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The serialization context.</param>
        protected ContentNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
