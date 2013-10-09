using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using Microsoft.Practices.Unity;

namespace Unity.SelfHostWebApiOwin
{
    /// <summary>
    /// Defines a filter provider for filter attributes that support injection of Unity dependencies.
    /// </summary>
    public class UnityFilterAttributeFilterProvider : IFilterProvider
    {
        private readonly IUnityContainer container;
        private readonly IEnumerable<IFilterProvider> filterProviders;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityFilterAttributeFilterProvider"/> class.
        /// </summary>
        /// <param name="container">The <see cref="IUnityContainer"/> that will be used to inject the filters.</param>
        public UnityFilterAttributeFilterProvider(IUnityContainer container, IEnumerable<IFilterProvider> filterProviders)
        {
            this.container = container;
            this.filterProviders = filterProviders;
        }

        /// <summary>
        /// Gets and injects the default filters.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>The default filters.</returns>
        public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            var filters = this.filterProviders.SelectMany(fp => fp.GetFilters(configuration, actionDescriptor)).ToList();

            foreach (var filter in filters)
            {
                this.container.BuildUp(filter.GetType(), filter.Instance);
            }

            return filters;
        }

    }
}
