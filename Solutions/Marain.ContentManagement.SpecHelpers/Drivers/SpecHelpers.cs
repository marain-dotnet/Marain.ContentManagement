// <copyright file="SpecHelpers.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Drivers
{
    using System;
    using System.Reflection;
    using Corvus.Extensions;
    using TechTalk.SpecFlow;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

    public static class SpecHelpers
    {
        public static T ParseSpecValue<T>(SpecFlowContext scenarioContext, string property)
        {
            property = PerformSubstitutions(property);

            if (property is null)
            {
                return default;
            }

            if (!(property.StartsWith('{') && property.EndsWith('}')))
            {
                // We assume it must be a string as it has no substitution
                return CastTo<T>.From(property);
            }

            int indexOfDot = property.IndexOf('.');
            string contextKey = property.Substring(1, indexOfDot - 1);
            string propertyName = property.Substring(indexOfDot + 1, property.Length - (indexOfDot + 2));

            object contextValue = scenarioContext.ContainsKey(contextKey) ? scenarioContext[contextKey] : null;
            if (contextValue is null)
            {
                return default;
            }

            PropertyInfo propertyInfo = contextValue.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                throw new InvalidOperationException($"The property called {propertyName} was not found on an instance of the type {contextValue.GetType().Name}");
            }

            if (!typeof(T).IsAssignableFrom(propertyInfo.PropertyType))
            {
                throw new InvalidOperationException($"The property called {propertyName} of type {propertyInfo.PropertyType.Name} is not an instance of the requested type {typeof(T).Name}");
            }

            return CastTo<T>.From(propertyInfo.GetValue(contextValue));
        }

        public static string PerformSubstitutions(string v)
        {
            if (v == "{null}")
            {
                return null;
            }

            return v.Replace("{newguid}", Guid.NewGuid().ToString()).Replace(@"\n", "\n").Replace(@"\r", "\r").Replace(@"\t", "\t");
        }
    }
}
