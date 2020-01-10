// <copyright file="BindingSequence.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Bindings
{
    using Corvus.SpecFlow.Extensions;

    /// <summary>
    /// Constants to control the sequencing of binding execution where needed.
    /// </summary>
    public static class BindingSequence
    {
        /// <summary>
        /// Binding order for setting up the transient tenant for a feature.
        /// </summary>
        public const int SetupTransientTenant = ContainerBeforeFeatureOrder.ServiceProviderAvailable;

        /// <summary>
        /// Binding order for creating content data for a feature.
        /// </summary>
        public const int CreateContentTestData = SetupTransientTenant + 1;

        /// <summary>
        /// Binding order for creating content state data for a feature.
        /// </summary>
        public const int CreateContentStateTestData = CreateContentTestData + 1;
    }
}
