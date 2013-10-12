﻿using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using Microsoft.Practices.Unity;

namespace Unity.SelfHostWebApiOwin
{
    public class WebApiUnityActionFilterProvider : ActionDescriptorFilterProvider, IFilterProvider
    {
        private readonly IUnityContainer container;

        public WebApiUnityActionFilterProvider(IUnityContainer container)
        {
            this.container = container;
        }

        public new IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(configuration, actionDescriptor);
            var filterInfoList = new List<FilterInfo>();

            foreach (var filter in filters)
            {
                container.BuildUp(filter.Instance.GetType(), filter.Instance);
            }

            return filters;
        }
    }

}


