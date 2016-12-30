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
        private Dictionary<int, Fragment> mFragmentCache = new Dictionary<int, Fragment>(); 

        private TextView mTxtDevice;
        private TextView mTxtFirmware;
        private CheckBox mChkConnected;
        private TextView mTxtConnected;

        private Fragment m_CurrentFragment;
        private InitialViewFragment m_InitialViewFragment;
        private GeneralViewFragment m_GeneralViewFragment;

        private Boolean m_IsConnected;
        private Boolean m_IsFirstDeviceConnection = true;
        private int m_TxtConnectedStringId = Resource.String.text_disconnected;
       
        private HidUsbReceiver m_HidUsbReceiver;
        private ArcticFoxConfigurationViewModel m_ViewModel;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);          

            _InitializeComponents();           
            _InitializeUsb();
            _InitializeNToolboxWrappers();
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
            m_GeneralViewFragment = new GeneralViewFragment();
            mFrameContent = FindViewById<FrameLayout>(Resource.Id.frame);
            m_InitialViewFragment = new InitialViewFragment();
            _ReplaceFrameContent(m_InitialViewFragment);

        }

        private void _ReplaceFrameContent(Fragment fragment)
        {
            var ft = this.FragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.frame, fragment);
            ft.Commit();
            m_CurrentFragment = fragment;
        }

        private void _InitializeNToolboxWrappers()
        {
            m_ViewModel = new ArcticFoxConfigurationViewModel();
            m_InitialViewFragment.FirmwareMinVersion = m_ViewModel.MinimumBuildNumber;
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

            //Declare fragment to display
            Fragment fragment = null;

            if (!mFragmentCache.TryGetValue(menuItem.ItemId, out fragment))
            {

                //Check to see which item was being clicked and perform appropriate action
                switch (menuItem.ItemId)
                {
                    case Resource.Id.nav_general:
                        fragment = m_GeneralViewFragment;
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

                mFragmentCache.Add(menuItem.ItemId, fragment);
            }

            if (fragment != null)
            {
                _ReplaceFrameContent(fragment);
            }
            else
            {
                Toast.MakeText(ApplicationContext, "Error creating view", 0).Show();
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

        private void _SetDataFromArticFoxConfiguration()
        {
            m_GeneralViewFragment.DeviceName = m_ViewModel.DeviceName;
            m_GeneralViewFragment.Build = m_ViewModel.Build;
            m_GeneralViewFragment.FwVer = m_ViewModel.FwVer;
            m_GeneralViewFragment.HwVer = m_ViewModel.HwVer;
        }

        private void _SetDataToArticFoxConfiguration()
        {
            
        }

        private void RefreshFromDevice()
        {
            m_ViewModel.ReadConfigurationFromDevice();
            _SetDataFromArticFoxConfiguration();

        }

        private void UploadToDevice()
        {  
            try
            {
                _SetDataToArticFoxConfiguration();
                m_ViewModel.WriteConfigurationToDevice();
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

                    if (m_CurrentFragment == m_InitialViewFragment)
                        _ReplaceFrameContent(m_GeneralViewFragment);

                   

                }
                catch
                {
                    Toast.MakeText(Application.Context, "Error reading device data", ToastLength.Long);
                }
            }
        }       

     
    }  
}

