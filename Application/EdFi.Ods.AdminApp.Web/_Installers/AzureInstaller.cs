// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EdFi.Ods.AdminApp.Management.Azure;
using EdFi.Ods.AdminApp.Management;
using EdFi.Ods.AdminApp.Management.Database.Setup;
using EdFi.Ods.AdminApp.Management.Services;
using EdFi.Ods.AdminApp.Web.Display.DisplayService;
using EdFi.Ods.AdminApp.Web.Display.HomeScreen;
using EdFi.Ods.AdminApp.Web.Display.TabEnumeration;
using EdFi.Ods.AdminApp.Web.Infrastructure;

namespace EdFi.Ods.AdminApp.Web._Installers
{
    public class AzureInstaller : CommonConfigurationInstaller
    {
        protected override void InstallHostingSpecificClasses(IWindsorContainer services)
        {
            InstallAzureSpecificServices(services);
        }

        private void InstallAzureSpecificServices(IWindsorContainer services)
        {
            services.Register(Component.For<AzureActiveDirectoryClientInfo>()
                .Instance(CloudOdsAzureActiveDirectoryClientInfo.GetActiveDirectoryClientInfoForUser())
                .LifestyleSingleton());

            services.Register(Component.For<IGetCloudOdsHostedComponentsQuery, IGetAzureCloudOdsHostedComponentsQuery, GetAzureCloudOdsHostedComponentsQuery>()
                .ImplementedBy<GetAzureCloudOdsHostedComponentsQuery>()
                .LifestyleTransient());

            services.Register(Component.For<IGetAzureCloudOdsWebsitePerformanceLevelQuery, GetAzureCloudOdsWebsitePerformanceLevelQuery>()
                .ImplementedBy<GetAzureCloudOdsWebsitePerformanceLevelQuery>()
                .LifestyleTransient());

            services.Register(Component.For<IGetCloudOdsApiWebsiteSettingsQuery, GetAzureCloudOdsApiWebsiteSettingsQuery>()
                .ImplementedBy<GetAzureCloudOdsApiWebsiteSettingsQuery>()
                .LifestyleTransient());

            services.Register(Component.For<AzureDatabaseManagementService>()
                .ImplementedBy<AzureDatabaseManagementService>()
                .LifestyleTransient());

            services.Register(Component.For<ICloudOdsDatabaseSecurityConfigurator, SqlServerCloudOdsDatabaseSecurityConfigurator>()
                .ImplementedBy<SqlServerCloudOdsDatabaseSecurityConfigurator>()
                .LifestyleTransient());

            services.Register(Component.For<AzureDatabaseLifecycleManagementService>()
                .ImplementedBy<AzureDatabaseLifecycleManagementService>()
                .LifestyleTransient());

            services.Register(Component.For<GetAzureCloudOdsHostedInstanceQuery>()
                .ImplementedBy<GetAzureCloudOdsHostedInstanceQuery>()
                .LifestyleTransient());

            services.AddTransient<ICompleteOdsPostUpdateSetupCommand, CompleteAzureOdsPostUpdateSetupCommand>();
            services.AddTransient<IRestartAppServicesCommand, RestartAzureAppServicesCommand>();
            services.AddTransient<IUpdateCloudOdsApiWebsiteSettingsCommand, UpdateAzureCloudOdsApiWebsiteSettingsCommand>();
            services.AddTransient<IGetLatestPublishedOdsVersionQuery, GetLatestPublishedOdsVersionQuery>();
            services.AddTransient<IGetProductionApiProvisioningWarningsQuery, GetAzureProductionApiProvisioningWarningsQuery>();
            services.AddTransient<ICloudOdsProductionLifecycleManagementService, AzureProductionLifecycleManagementService>();
            services.AddTransient<IGetCloudOdsInstanceQuery, GetAzureCloudOdsInstanceQuery>();
            services.AddTransient<ICloudOdsDatabaseSqlServerSecurityConfiguration, AzureCloudOdsDatabaseSqlServerSecurityConfiguration>();
            services.AddTransient<IFirstTimeSetupService, AzureFirstTimeSetupService>();
            services.AddTransient<ICloudOdsDatabaseNameProvider, AzureCloudOdsDatabaseNameProvider>();
            services.AddTransient<ITabDisplayService, AzureTabDisplayService>();
            services.AddTransient<IHomeScreenDisplayService, AzureHomeScreenDisplayService>();
            services.AddTransient<ICompleteOdsFirstTimeSetupCommand, CompleteOdsFirstTimeSetupCommand>();
        }
    }
}
