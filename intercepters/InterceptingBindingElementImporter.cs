//----------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//----------------------------------------------------------------

using System.ServiceModel.Description;

namespace WCF_SERVER.intercepters
{
    public abstract class InterceptingBindingElementImporter: IPolicyImportExtension
    {
        void IPolicyImportExtension.ImportPolicy(MetadataImporter importer, PolicyConversionContext context)
        {
            ChannelMessageInterceptor messageInterceptor = CreateMessageInterceptor();
            messageInterceptor.OnImportPolicy(importer, context);
        }

        protected abstract ChannelMessageInterceptor CreateMessageInterceptor();
    }
}
