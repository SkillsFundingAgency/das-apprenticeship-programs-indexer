﻿// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator 0.9.7.0
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Sfa.Das.Sas.Indexer.Infrastructure.CourseDirectory.Models;

namespace Sfa.Das.Sas.Indexer.Infrastructure.CourseDirectory
{
    public static partial class CourseDirectoryProviderDataServiceExtensions
    {
        /// <summary>
        /// Provides the ability to retreive all providers that provide
        /// training for standards and frameworks along with the locations and
        /// areas that they offer to provide it.
        /// </summary>
        /// <param name='operations'>
        /// Reference to the
        /// Sfa.Infrastructure.ICourseDirectoryProviderDataService.
        /// </param>
        /// <param name='version'>
        /// Optional. version of the api
        /// </param>
        public static IList<Provider> Bulkproviders(this ICourseDirectoryProviderDataService operations, int? version = null)
        {
            return Task.Factory.StartNew((object s) => 
            {
                return ((ICourseDirectoryProviderDataService)s).BulkprovidersAsync(version);
            }
            , operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
        }
        
        /// <summary>
        /// Provides the ability to retreive all providers that provide
        /// training for standards and frameworks along with the locations and
        /// areas that they offer to provide it.
        /// </summary>
        /// <param name='operations'>
        /// Reference to the
        /// Sfa.Infrastructure.ICourseDirectoryProviderDataService.
        /// </param>
        /// <param name='version'>
        /// Optional. version of the api
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        public static async Task<IList<Provider>> BulkprovidersAsync(this ICourseDirectoryProviderDataService operations, int? version = null, CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Microsoft.Rest.HttpOperationResponse<System.Collections.Generic.IList<Provider>> result = await operations.BulkprovidersWithOperationResponseAsync(version, cancellationToken).ConfigureAwait(false);
            return result.Body;
        }
    }
}
