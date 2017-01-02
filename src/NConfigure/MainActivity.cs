using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Hardware.Usb;
using System.Collections.Generic;
using NCore.USB;
using System;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Support.V7.View;
using Android.Support.V4.View;
using Android.Graphics.Drawables;
using NCore;
using NToolbox.Models;
using System.Globalization;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;
using NToolbox.ViewModels;
using NToolbox.UI;

namespace NToolbox
{
    [Activity(Label = "NFE Toolbox", MainLauncher = true, Theme = "@style/Theme.NToolbox", Icon = "@drawable/icon", LaunchMode = Android.Content.PM.LaunchMode.SingleInstance)]
    [IntentFilter(new string[] { Android.Hardware.Usb.UsbManager.ActionUsbDeviceAttached })]
    [MetaData(Android.Hardware.Usb.UsbManager.ActionUsbDeviceAttached, Resource = "@xml/usb_device_filter")]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener //Activity, MainMenuAdapter.OnItemClickListener, View.IOnClickListener
    {
        private const string ACTION_USB_PERMISSION = "com.android.example.USB_PERMISSION";

        private DrawerLayout mDrawerLayout;
        private NavigationView mNavigationView;
        private FrameLayout mFrameContent;

        private string m_CurrentFragmentTypeName;
        private Int32 m_CurrentFragmentId;
        private string m_CurrentFragmentTag;

        private Boolean m_IsConnected;
        private Boolean m_IsFirstDeviceConnection = true;
       
        private HidUsbReceiver m_HidUsbReceiver;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            _InitializeComponents();           
            _InitializeUsb();
            
        }

        private void _InitializeComponents()
        {
            SetContentView(Resource.Layout.activity_main);

            //Toolbar
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.main_toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_drawer);

            //Navigation View
            mNavigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            mNavigationView.SetNavigationItemSelectedListener(this);

            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            var actionbarDrawerToggle = new MainMenuActionBarDrawerToggle(this, mDrawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            mDrawerLayout.AddDrawerListener(actionbarDrawerToggle);
            actionbarDrawerToggle.SyncState();

            //Initial Content         
            mFrameContent = FindViewById<FrameLayout>(Resource.Id.frame);
            _restoreSelectedFrame();
        }

        private void _restoreSelectedFrame()
        {
            if (string.IsNullOrWhiteSpace(m_CurrentFragmentTypeName))
            {
                _SetFrame<InitialViewFragment>(InitialViewFragment.LayoutResourceId);
            }
            else
            {
                _SetFrame(m_CurrentFragmentTypeName, m_CurrentFragmentId, m_CurrentFragmentTag);
            }
        }

        private void _SetFrame(String fragmentTypeName, Int32 fragmentId, string tag = null)
        {
            FragmentBase fragment = null;
            if (!String.IsNullOrWhiteSpace(tag))
            {
                fragment = FragmentManager.FindFragmentByTag(tag) as FragmentBase;
            }
            else
            {
                fragment = (FragmentBase)FragmentManager.FindFragmentById(fragmentId);
            }
            if (fragment == null)
            {
                var type = Type.GetType(fragmentTypeName);
                fragment = (FragmentBase)Activator.CreateInstance(type);
                var ft = this.FragmentManager.BeginTransaction();
                if (!String.IsNullOrWhiteSpace(tag))
                {
                    ft.Replace(Resource.Id.frame, fragment, tag);
                }
                else
                {
                    ft.Replace(Resource.Id.frame, fragment);
                }               
                ft.Commit();
                m_CurrentFragmentId = fragmentId;
                m_CurrentFragmentTypeName = fragmentTypeName;
                m_CurrentFragmentTag = tag;
            }
        }

        private void _SetFrame<T>(Int32 fragmentId, string tag = null)
            where T : FragmentBase, new()
        {
            _SetFrame(typeof(T).FullName, fragmentId, tag);
        }

    
        private void _InitializeUsb()
        {
            IntentFilter filter = new IntentFilter(ACTION_USB_PERMISSION);
            filter.AddAction(UsbManager.ActionUsbDeviceAttached);
            filter.AddAction(UsbManager.ActionUsbDeviceDetached);
            filter.AddAction(UsbManager.ActionUsbAccessoryAttached);
            filter.AddAction(UsbManager.ActionUsbAccessoryDetached);
            filter.AddAction(UsbManager.ActionUsbDeviceDetached);

            m_HidUsbReceiver = new HidUsbReceiver();
            RegisterReceiver(m_HidUsbReceiver, filter);

            HidConnector.Instance.DeviceConnected += HidConnector_DeviceConnected;
            HidConnector.Instance.RefreshState();

            //HidConnectorInstance.HidConnector.DeviceConnected += HidConnector_DeviceConnected;
            //HidConnectorInstance.HidConnector.RefreshState();

        }

    

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            ////Checking if the item is in checked state or not, if not make it in checked state
            //if (menuItem.IsChecked) menuItem.SetChecked(false);
            //else menuItem.SetChecked(true);

            //Closing drawer on item click
            mDrawerLayout.CloseDrawers();


                //Check to see which item was being clicked and perform appropriate action
            switch (menuItem.ItemId)
            {
                case Resource.Id.nav_general:
                    _SetFrame<GeneralViewFragment>(GeneralViewFragment.LayoutResourceId);
                    break;
                //case Resource.Id.nav_profile1:
                //    fragment = new ProfileViewFragment();
                //    break;
                //case Resource.Id.nav_profile2:
                //    fragment = new ProfileViewFragment();
                //    break;
                //case Resource.Id.nav_profile3:
                //    fragment = new ProfileViewFragment();
                //    break;
                //case Resource.Id.nav_profile4:
                //    fragment = new ProfileViewFragment();
                //    break;
                //case Resource.Id.nav_profile5:
                //    fragment = new ProfileViewFragment();
                //    break;
                //case Resource.Id.nav_profile6:
                //    fragment = new ProfileViewFragment();
                //    break;
                //case Resource.Id.nav_profile7:
                //    fragment = new ProfileViewFragment();
                //    break;
                //case Resource.Id.nav_profile8:
                //    fragment = new ProfileViewFragment();
                //    break;
                //case Resource.Id.nav_control:
                //    fragment = new ControlViewFragment();
                //    break;
                //case Resource.Id.nav_monitor:
                //    fragment = new MonitorViewFragment();
                //    break;
                //case Resource.Id.nav_debug:
                //    fragment = new DebugViewFragment();
                //    break;
                default:
                    Toast.MakeText(ApplicationContext, "Invalid Menu Item", 0).Show();
                    return false;
            }

               
                 
            return true;
        }



        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    if (mDrawerLayout.IsDrawerOpen(Android.Support.V4.View.GravityCompat.Start))
                        mDrawerLayout.CloseDrawer(Android.Support.V4.View.GravityCompat.Start);
                    else if (m_IsConnected)
                        mDrawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    break;
                case Resource.Id.nav_action_download_settings :
                    RefreshFromDevice();
                    break;
                case Resource.Id.nav_action_upload_device:
                    UploadToDevice();
                    break;
                case Resource.Id.nav_action_reset_device:
                    HidConnector.Instance.ResetDataflash();
                    break;
                case Resource.Id.nav_action_restart_device:
                    HidConnector.Instance.RestartDevice();
                    break;
                default:
                    return false;

            }
            return true;
        }



        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_menu, menu);
            
            return true;
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {

            menu.FindItem(Resource.Id.nav_action_download_settings).SetEnabled(m_IsConnected);
            menu.FindItem(Resource.Id.nav_action_reset_device).SetEnabled(m_IsConnected);
            menu.FindItem(Resource.Id.nav_action_upload_device).SetEnabled(m_IsConnected);
            menu.FindItem(Resource.Id.nav_action_restart_device).SetEnabled(m_IsConnected);
            return base.OnPrepareOptionsMenu(menu);
        }

        protected override void OnResume()
        {
            base.OnResume();
            HidConnector.Instance.RefreshState();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutString("m_CurrentFragmentTypeName", m_CurrentFragmentTypeName);
            outState.PutString("m_CurrentFragmentTag", m_CurrentFragmentTag);
            outState.PutInt("m_CurrentFragmentId", m_CurrentFragmentId);
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);

            m_CurrentFragmentTypeName   = savedInstanceState.GetString("m_CurrentFragmentTypeName");
            m_CurrentFragmentTag        = savedInstanceState.GetString("m_CurrentFragmentTag");
            m_CurrentFragmentId         = savedInstanceState.GetInt("m_CurrentFragmentId");

            _restoreSelectedFrame();
        }

        //private void _SetDataFromArticFoxConfiguration()
        //{
        //    m_GeneralViewFragment.DeviceName = m_ViewModel.DeviceName;
        //    m_GeneralViewFragment.Build = m_ViewModel.Build;
        //    m_GeneralViewFragment.FwVer = m_ViewModel.FwVer;
        //    m_GeneralViewFragment.HwVer = m_ViewModel.HwVer;
        //}

        private void _SetDataToArticFoxConfiguration()
        {
            
        }

        private void RefreshFromDevice()
        {
            ArcticFoxConfigurationViewModel.Instance.ReadConfigurationFromDevice();
            //_SetDataFromArticFoxConfiguration();

        }

        private void UploadToDevice()
        {  
            try
            {
                _SetDataToArticFoxConfiguration();
                ArcticFoxConfigurationViewModel.Instance.WriteConfigurationToDevice();
            }
            catch (TimeoutException)
            {
                Toast.MakeText(Application.Context,"Unable to write configuration.", ToastLength.Long);
            }

        }

        private ArcticFoxConfiguration _ReadConfiguration()
        {
            var data = HidConnector.Instance.ReadConfiguration();
            return BinaryStructure.Read<ArcticFoxConfiguration>(data);
        }

        private void HidConnector_DeviceConnected(bool connected)
        {
            m_IsConnected = connected;

            if (connected)
            {
                if (m_IsFirstDeviceConnection)
                {
                    m_IsFirstDeviceConnection = false;
                }

                try
                {

                    RefreshFromDevice();

                    if (m_CurrentFragmentId == 0 || m_CurrentFragmentId == InitialViewFragment.LayoutResourceId)
                        _SetFrame<GeneralViewFragment>(GeneralViewFragment.LayoutResourceId);
                   

                }
                catch
                {
                    Toast.MakeText(Application.Context, "Error reading device data", ToastLength.Long);
                }
            }
        }       

     
    }  
}

