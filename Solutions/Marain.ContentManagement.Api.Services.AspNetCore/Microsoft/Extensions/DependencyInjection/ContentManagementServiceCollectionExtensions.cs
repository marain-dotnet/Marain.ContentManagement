// <copyright file="ContentManagementServiceCollectionExtensions.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using System.IO;
    using System.Linq;
    using Corvus.Tenancy.Exceptions;
    using Marain.Cms;
    using Marain.Cms.Api.Services;
    using Marain.Cms.Api.Services.Internal;
    using Menes;
    using Menes.Exceptions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.OpenApi.Models;
    using Microsoft.OpenApi.Readers;

    /// <summary>
    /// Extension methods for configuring DI for the Content Management Open API services.
    /// </summary>
    public static class ContentManagementServiceCollectionExtensions
    {
        /// <summary>
        /// Add services required by the content management API.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="rootTenantDefaultConfiguration">
        /// Configuration section to read root tenant default repository settings from.
        /// </param>
        /// <param name="configureHost">Optional callback for additional host configuration.</param>
        /// <returns>The service collection, to enable chaining.</returns>
        public static IServiceCollection AddTenantedContentManagementApi(
            this IServiceCollection services,
            IConfiguration rootTenantDefaultConfiguration,
            Action<IOpenApiHostConfiguration> configureHost = null)
        {
            Type contentServiceType = typeof(ContentService);

            // If any of the OpenApi services are already installed, assume we've already completed installation and return.
            if (services.Any(services => contentServiceType.IsAssignableFrom(services.ImplementationType)))
            {
                return services;
            }

            services.AddLogging();

            services.AddContentManagementContent();

            services.AddTenantCloudBlobContainerFactory(rootTenantDefaultConfiguration);
            services.AddTenantProviderBlobStore();
            services.AddTenantedAzureCosmosContentStore(rootTenantDefaultConfiguration);

            services.AddContentManagementOpenApiServices();
            services.AddContentManagementHalDocumentMappers();

            services.AddOpenApiHttpRequestHosting<SimpleOpenApiContext>(config =>
            {
                config.AddContentManagementServiceDefinition();

                configureHost?.Invoke(config);

                config.MapTenancyExceptions();
                config.MapContentManagementExceptions();
            });

            return services;
        }

        private static IServiceCollection AddContentManagementOpenApiServices(this IServiceCollection services)
        {
            services.AddSingleton<ContentService>();
            services.AddSingleton<IOpenApiService, ContentService>(s => s.GetRequiredService<ContentService>());

            services.AddSingleton<ContentSummaryService>();
            services.AddSingleton<IOpenApiService, ContentSummaryService>(s => s.GetRequiredService<ContentSummaryService>());

            services.AddSingleton<ContentHistoryService>();
            services.AddSingleton<IOpenApiService, ContentHistoryService>(s => s.GetRequiredService<ContentHistoryService>());

            services.AddSingleton<WorkflowHistoryService>();
            services.AddSingleton<IOpenApiService, WorkflowHistoryService>(s => s.GetRequiredService<WorkflowHistoryService>());

            services.AddSingleton<WorkflowStateService>();
            services.AddSingleton<IOpenApiService, WorkflowStateService>(s => s.GetRequiredService<WorkflowStateService>());

            return services;
        }

        private static IServiceCollection AddContentManagementHalDocumentMappers(this IServiceCollection services)
        {
            services.AddHalDocumentMapper<Content, ResponseMappingContext, ContentResponseMapper>();
            services.AddHalDocumentMapper<ContentSummaries, ContentSummariesResponseMappingContext, ContentSummariesResponseMapper>();
            services.AddHalDocumentMapper<ContentSummary, ResponseMappingContext, ContentSummaryResponseMapper>();
            services.AddHalDocumentMapper<ContentState, ContentStateResponseMappingContext, ContentStateResponseMapper>();
            services.AddHalDocumentMapper<ContentStates, ContentStatesResponseMappingContext, ContentStatesResponseMapper>();

            return services;
        }

        private static void AddContentManagementServiceDefinition(this IOpenApiHostConfiguration config)
        {
            Type contentServiceType = typeof(ContentService);
            OpenApiDocument apiYamlDoc = OpenApiServiceDefinitions.GetOpenApiServiceFromEmbeddedDefinition(
                contentServiceType.Assembly,
                $"{contentServiceType.Namespace}.ContentManagementServices.yaml");

            config.Documents.Add(apiYamlDoc);
        }

        private static void MapTenancyExceptions(this IOpenApiHostConfiguration config)
        {
            config.Exceptions.Map<TenantNotFoundException>(404);
        }

        private static void MapContentManagementExceptions(this IOpenApiHostConfiguration config)
        {
            config.Exceptions.Map<ContentNotFoundException>(404);
            config.Exceptions.Map<ContentConflictException>(409);
        }
    }
}
