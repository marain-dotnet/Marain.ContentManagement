// <copyright file="CmsIdentity.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System;

    /// <summary>
    /// A representation of a User Identity within the CMS.
    /// </summary>
    public struct CmsIdentity : IEquatable<CmsIdentity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CmsIdentity"/> struct.
        /// </summary>
        /// <param name="userId">The unique user id of the entity.</param>
        /// <param name="userName">A human-readable user name for the entity.</param>
        public CmsIdentity(string userId, string userName)
            : this()
        {
            this.UserId = userId;
            this.UserName = userName;
        }

        /// <summary>
        /// Gets the unique user id of the entity.
        /// </summary>
        public string UserId { get; }

        /// <summary>
        /// Gets the human-readable user name for the entity.
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// Standard equality operator.
        /// </summary>
        /// <param name="x">The first identity.</param>
        /// <param name="y">The second identity.</param>
        /// <returns>True if the user name and user id both match.</returns>
        public static bool operator ==(CmsIdentity x, CmsIdentity y)
        {
            return x.Equals(y);
        }

        /// <summary>
        /// Standard inequality operator.
        /// </summary>
        /// <param name="x">The first identity.</param>
        /// <param name="y">The second identity.</param>
        /// <returns>True if either the user name or the user id do not match.</returns>
        public static bool operator !=(CmsIdentity x, CmsIdentity y)
        {
            return !x.Equals(y);
        }

        /// <summary>
        /// Get a hashcode for the identity.
        /// </summary>
        /// <returns>The hashcode for the identity.</returns>
        public override int GetHashCode()
        {
            return (this.UserId, this.UserName).GetHashCode();
        }

        /// <summary>
        /// Standard equals method.
        /// </summary>
        /// <param name="obj">The object to compare with this identity.</param>
        /// <returns>True if the user name and user id both match.</returns>
        public override bool Equals(object obj)
        {
            return
                obj is CmsIdentity cmsIdentity
                && this.Equals(cmsIdentity);
        }

        /// <summary>
        /// Standard equals method.
        /// </summary>
        /// <param name="other">The identity to compare with this identity.</param>
        /// <returns>True if the object is a <see cref="CmsIdentity"/> that is equal to this identity.</returns>
        public bool Equals(CmsIdentity other)
        {
            return (this.UserId, this.UserName) == (other.UserId, other.UserName);
        }
    }
}