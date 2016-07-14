using System.ComponentModel;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using System;

namespace Infrastructure.PopupBehaviour
{
    /// <summary>
    /// Declares the Attached Properties and Behaviors for implementing Popup regions.
    /// </summary>
    /// <remarks>
    /// Although the fastest way is to create a RegionAdapter for a Window and register it with the RegionAdapterMappings,
    /// this would be conceptually incorrect because we want to create a new popup window everytime a view is added 
    /// (instead of having a Window as a host control and replacing its contents everytime Views are added, as other adapters do).
    /// This is why we have a different class for this behavior, instead of reusing the <see cref="RegionManager.RegionNameProperty"/> attached property.
    /// </remarks>
    public static class RegionMultiplePopupBehaviors
    {
        public static readonly DependencyProperty CreatePopupRegionWithNameProperty =
            DependencyProperty.RegisterAttached("CreatePopupRegionWithName", typeof(string), typeof(RegionMultiplePopupBehaviors), new PropertyMetadata(CreatePopupRegionWithNamePropertyChanged));

        public static readonly DependencyProperty CreateModalPopupRegionWithNameProperty =
            DependencyProperty.RegisterAttached("CreateModalPopupRegionWithName", typeof(string), typeof(RegionMultiplePopupBehaviors), new PropertyMetadata(CreateModalPopupRegionWithNamePropertyChanged));

        public static readonly DependencyProperty ContainerWindowStyleProperty =
          DependencyProperty.RegisterAttached("ContainerWindowStyle", typeof(Style), typeof(RegionMultiplePopupBehaviors), null);


        public static string GetCreatePopupRegionWithName(DependencyObject owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }

            return owner.GetValue(CreatePopupRegionWithNameProperty) as string;
        }


        public static void SetCreatePopupRegionWithName(DependencyObject owner, string value)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }

            owner.SetValue(CreatePopupRegionWithNameProperty, value);
        }

        public static string GetCreateModalPopupRegionWithName(DependencyObject owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }

            return owner.GetValue(CreateModalPopupRegionWithNameProperty) as string;
        }

        public static void SetCreateModalPopupRegionWithName(DependencyObject owner, string value)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }

            owner.SetValue(CreateModalPopupRegionWithNameProperty, value);
        }

        public static Style GetContainerWindowStyle(DependencyObject owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }

            return owner.GetValue(ContainerWindowStyleProperty) as Style;
        }

        public static void SetContainerWindowStyle(DependencyObject owner, Style style)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }

            owner.SetValue(ContainerWindowStyleProperty, style);
        }

        /// <summary>
        /// Creates a new <see cref="IRegion"/> and registers it in the default <see cref="IRegionManager"/>
        /// attaching to it a <see cref="DialogActivationBehavior"/> behavior.
        /// </summary>
        /// <param name="owner">The owner of the Popup.</param>
        /// <param name="regionName">The name of the <see cref="IRegion"/>.</param>
        /// <remarks>
        /// This method would typically not be called directly, instead the behavior 
        /// should be set through the Attached Property <see cref="CreatePopupRegionWithNameProperty"/>.
        /// </remarks>
        public static void RegisterNewPopupRegion(DependencyObject owner, string regionName)
        {
            // Creates a new region and registers it in the default region manager.
            // Another option if you need the complete infrastructure with the default region behaviors
            // is to extend DelayedRegionCreationBehavior overriding the CreateRegion method and create an 
            // instance of it that will be in charge of registering the Region once a RegionManager is
            // set as an attached property in the Visual Tree.

            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();

            if (regionManager != null)
            {
                IRegion region = new Region();

                var behavior = new WindowDialogActivationBehavior {HostControl = owner};

                region.Behaviors.Add(DialogActivationBehavior.BehaviorKey, behavior);
                regionManager.Regions.Add(regionName, region);
            }
        }

        public static void RegisterNewModalPopupRegion(DependencyObject owner, string regionName)
        {
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();

            if (regionManager != null)
            {
                IRegion region = new Region();

                var behavior = new WindowModalDialogActivationBehavior { HostControl = owner };

                region.Behaviors.Add(DialogActivationBehavior.BehaviorKey, behavior);
                regionManager.Regions.Add(regionName, region);
            }
        }

        private static void CreatePopupRegionWithNamePropertyChanged(DependencyObject hostControl, DependencyPropertyChangedEventArgs e)
        {
            if (IsInDesignMode(hostControl))
            {
                return;
            }

            RegisterNewPopupRegion(hostControl, e.NewValue as string);
        }

        private static void CreateModalPopupRegionWithNamePropertyChanged(DependencyObject hostControl, DependencyPropertyChangedEventArgs e)
        {
            if (IsInDesignMode(hostControl))
            {
                return;
            }

            RegisterNewModalPopupRegion(hostControl, e.NewValue as string);
        }

        private static bool IsInDesignMode(DependencyObject element)
        {
            // Due to a known issue in Cider, GetIsInDesignMode attached property value is not enough to know if it's in design mode.
            return DesignerProperties.GetIsInDesignMode(element) || Application.Current == null
                   || Application.Current.GetType() == typeof(Application);
        }
    }
}
