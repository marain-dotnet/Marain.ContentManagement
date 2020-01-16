// <copyright file="ContentConflictException.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System;

    /// <summary>
    /// An exception thrown when trying to store a piece of content when there is an
    /// existing piece of content with the same Id and Slug.
    /// </summary>
    [Serializable]
    public class ContentConflictException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentConflictException"/> class.
        /// </summary>
        public ContentConflictException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentConflictException"/> class.
        /// </summary>
        /// <param name="id">The Id of the Content item that caused the conflict.</param>
        /// <param name="slug">The Slug of the Content item that caused the conflict.</param>
        public ContentConflictException(string id, string slug)
            : base(BuildMessage(id, slug))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentConflictException"/> class.
        /// </summary>
        /// <param name="id">The Id of the Content item that caused the conflict.</param>
        /// <param name="slug">The Slug of the Content item that caused the conflict.</param>
        /// <param name="inner">The inner <see cref="Exception"/>.</param>
        public ContentConflictException(string id, string slug, Exception inner)
            : base(BuildMessage(id, slug), inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Exception"></see> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ContentConflictException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Exception"></see> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ContentConflictException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Exception"></see> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="info">info</paramref> parameter is null.</exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"></see> is zero (0).</exception>
        protected ContentConflictException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

        private static string BuildMessage(string id, string slug) =>
            $"A Content item with Id '{id}' and Slug '{slug}' already exists.";
    }
}
