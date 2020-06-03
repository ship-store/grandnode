using Autofac;
using Grand.Core.Configuration;
using Grand.Core.Infrastructure;
using Grand.Core.Infrastructure.DependencyManagement;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using Grand.Web.Areas.Maintenance.Services;
using Grand.Web.Infrastructure.Installation;
using Grand.Web.Interfaces;
using Grand.Web.Services;

namespace Grand.Web.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, GrandConfig config)
        {
            //installation localization service
            builder.RegisterType<InstallationLocalizationService>().As<IInstallationLocalizationService>().InstancePerLifetimeScope();

            //blog service
            builder.RegisterType<BlogViewModelService>().As<IBlogViewModelService>().InstancePerLifetimeScope();

            //address service
            builder.RegisterType<AddressViewModelService>().As<IAddressViewModelService>().InstancePerLifetimeScope();

            //catalog service
            builder.RegisterType<CatalogViewModelService>().As<ICatalogViewModelService>().InstancePerLifetimeScope();

            //product service
            builder.RegisterType<ProductViewModelService>().As<IProductViewModelService>().InstancePerLifetimeScope();

            //news service
            builder.RegisterType<NewsViewModelService>().As<INewsViewModelService>().InstancePerLifetimeScope();

            //topic service
            builder.RegisterType<TopicViewModelService>().As<ITopicViewModelService>().InstancePerLifetimeScope();

            //customer service
            builder.RegisterType<CustomerViewModelService>().As<ICustomerViewModelService>().InstancePerLifetimeScope();

            //common service
            builder.RegisterType<CommonViewModelService>().As<ICommonViewModelService>().InstancePerLifetimeScope();

            //shipping service 
            builder.RegisterType<ShoppingCartViewModelService>().As<IShoppingCartViewModelService>().InstancePerLifetimeScope();

            //externalAuth service
            builder.RegisterType<ExternalAuthenticationViewModelService>().As<IExternalAuthenticationViewModelService>().InstancePerLifetimeScope();

            //widgetZone servie
            builder.RegisterType<WidgetViewModelService>().As<IWidgetViewModelService>().InstancePerLifetimeScope();

            //order service
            builder.RegisterType<OrderViewModelService>().As<IOrderViewModelService>().InstancePerLifetimeScope();

            //country service
            builder.RegisterType<CountryViewModelService>().As<ICountryViewModelService>().InstancePerLifetimeScope();

            //checkout service
            builder.RegisterType<CheckoutViewModelService>().As<ICheckoutViewModelService>().InstancePerLifetimeScope();

            //poll service
            builder.RegisterType<PollViewModelService>().As<IPollViewModelService>().InstancePerLifetimeScope();

            //poll service
            builder.RegisterType<BoardsViewModelService>().As<IBoardsViewModelService>().InstancePerLifetimeScope();

            //ReturnRequest service
            builder.RegisterType<ReturnRequestViewModelService>().As<IReturnRequestViewModelService>().InstancePerLifetimeScope();

            //Newsletter service
            builder.RegisterType<NewsletterViewModelService>().As<INewsletterViewModelService>().InstancePerLifetimeScope();

            //vendor service
            builder.RegisterType<VendorViewModelService>().As<IVendorViewModelService>().InstancePerLifetimeScope();

            //Vessel
            builder.RegisterType<VesselViewModelService>().As<IVesselViewModelService>().InstancePerLifetimeScope();

            //Register
            builder.RegisterType<RegisterViewModelService>().As<IRegisterViewModelService>().InstancePerLifetimeScope();

            //breakdown Service
            builder.RegisterType<BreakdownJobViewModelService>().As<IBreakdownJobViewModelService>().InstancePerLifetimeScope();

            //unplanned Service
            builder.RegisterType<UnplannedJobViewModelService>().As<IUnplannedJobViewModelService>().InstancePerLifetimeScope();
            //Job type
            builder.RegisterType<JobTypeViewModelService>().As<IJobTypeViewModelService>().InstancePerLifetimeScope();
            //reported by
            builder.RegisterType<ReportedByViewModelService1>().As<IReportedByViewModelService1>().InstancePerLifetimeScope();
            //job status
            builder.RegisterType<JobStatusViewModelService>().As<IJobStatusViewModelService>().InstancePerLifetimeScope();

            //CBM
            builder.RegisterType<CbmViewModelService>().As<ICbmViewModelService>().InstancePerLifetimeScope();

            //Maker Service
            builder.RegisterType<MakerViewModelService>().As<IMakerViewModelService>().InstancePerLifetimeScope();
            builder.RegisterType<EquipmentTypeViewModelService>().As<IEquipmentTypeViewModelService>().InstancePerLifetimeScope();
           
            builder.RegisterType<MakerViewModelService1>().As<IMakerViewModelService1>().InstancePerLifetimeScope();
            //Equipment Service
            builder.RegisterType<ImportFileService>().As<IImportFileService>().InstancePerLifetimeScope();
            builder.RegisterType<EquipmentImportManger>().As<EquipmentImportManger>().InstancePerLifetimeScope();
            //Jobplan Service
            builder.RegisterType<JobplanImportManger>().As<IJobplanImportManger>().InstancePerLifetimeScope();

            //Sparepart Service
            builder.RegisterType<SparepartImportManger>().As<ISparepartImportManger>().InstancePerLifetimeScope();

            //Jobmaster Service
            builder.RegisterType<JobMasterViewModelService>().As<IJobMasterViewModelService>().InstancePerLifetimeScope();

            //Equipment view model service
            builder.RegisterType<EquipmentViewModelService>().As<IEquipmentViewModelService>().InstancePerLifetimeScope();
           // sparepart view model service
            builder.RegisterType<SparepartViewModelService>().As<ISparepartViewModelService>().InstancePerLifetimeScope();
            // jobplan view model service
            builder.RegisterType<JobPlanViewModelService>().As<IJobPlanViewModelService>().InstancePerLifetimeScope();
            builder.RegisterType<ReportViewModelService>().As<IReportViewModelService>().InstancePerLifetimeScope();
            

        }

        public int Order
        {
            get { return 2; }
        }
    }
}
