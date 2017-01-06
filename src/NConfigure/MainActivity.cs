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
    [Activity(Label = "NFE Toolbox", MainLauncher = true, Theme = "@style/AppTheme", Icon = "@drawable/icon", LaunchMode = Android.Content.PM.LaunchMode.SingleInstance)]
    [IntentFilter(new string[] { Android.Hardware.Usb.UsbManager.ActionUsbDeviceAttached })]
    [MetaData(Android.Hardware.Usb.UsbManager.ActionUsbDeviceAttached, Resource = "@xml/usb_device_filter")]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener //Activity, MainMenuAdapter.OnItemClickListener, View.IOnClickListener
    {
        private const string ACTION_USB_PERMISSION = "com.android.example.USB_PERMISSION";

        private DrawerLayout mDrawerLayout;
        private NavigationView mNavigationView;
        private FrameLayout mFrameContent;

        private TextView m_txtDeviceName;
        private TextView m_txtFwVer;
        private TextView m_txtBuild;
        private TextView m_txtHwVer;

        private string m_CurrentFragmentTypeName;
        private Int32 m_CurrentFragmentId;
        private string m_CurrentFragmentTag;

        private Boolean m_IsConnected;
        private Boolean m_IsFirstDeviceConnection = true;
       
        private HidUsbReceiver m_HidUsbReceiver;

        private ArcticFoxConfigurationViewModel m_ViewModel;

        public ArcticFoxConfigurationViewModel ViewModel
        {
            get { return m_ViewModel; }
        }

        public MainActivity()
        {
            m_ViewModel = new ArcticFoxConfigurationViewModel(this);
        }

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
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_drawer);

            //Navigation View
            mNavigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            mNavigationView.SetNavigationItemSelectedListener(this);


            var headerView = mNavigationView.GetHeaderView(0);

            m_txtDeviceName = headerView.FindViewById<TextView>(Resource.Id.header_devicename);
            m_txtFwVer = headerView.FindViewById<TextView>(Resource.Id.header_fw_ver);
            m_txtBuild = headerView.FindViewById<TextView>(Resource.Id.header_build);
            m_txtHwVer = headerView.FindViewById<TextView>(Resource.Id.header_hw_ver);


            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            var actionbarDrawerToggle = new ActionBarDrawerToggle(this, mDrawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            mDrawerLayout.AddDrawerListener(actionbarDrawerToggle);
            actionbarDrawerToggle.SyncState();

            //Initial Content         
            mFrameContent = FindViewById<FrameLayout>(Resource.Id.content_main);
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
            Fragment fragment = null;
            if (!String.IsNullOrWhiteSpace(tag))
            {
                fragment = FragmentManager.FindFragmentByTag(tag) as Fragment;
            }
            else
            {
                fragment = (FragmentBase)FragmentManager.FindFragmentById(fragmentId);
            }
            if (fragment == null)
            {
                var type = Type.GetType(fragmentTypeName);
                fragment = (Fragment)Activator.CreateInstance(type);
                var ft = this.FragmentManager.BeginTransaction();
                if (!String.IsNullOrWhiteSpace(tag))
                {
                    ft.Replace(Resource.Id.content_main, fragment, tag);
                }
                else if(fragmentId == -1)
                {
                    ft.Replace(Resource.Id.content_main, fragment);
                }     
                else
                {
                    ft.Replace(Resource.Id.content_main, fragment);
                }          
                ft.Commit();
                m_CurrentFragmentId = fragmentId;
                m_CurrentFragmentTypeName = fragmentTypeName;
                m_CurrentFragmentTag = tag;
            }
        }

        private void _SetFrame<T>(Int32 fragmentId, string tag = null)
            where T : Fragment, new()
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
        }

    

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            ////Checking if the item is in checked state or not, if not make it in checked state
            //if (menuItem.IsChecked) menuItem.SetChecked(false);
            //else menuItem.SetChecked(true);

          

                //Check to see which item was being clicked and perform appropriate action
            switch (menuItem.ItemId)
            {
                case Resource.Id.nav_general:
                    _SetFrame<GeneralViewFragment>(GeneralViewFragment.LayoutResourceId);
                    break;
                case Resource.Id.nav_profile1:
                    _SetFrame<ViewProfileDetailPreferences>(ViewProfileDetailPreferences.XmlPreferenceId, ProfileSelection.Profile1.ToString());
                    break;
                case Resource.Id.nav_profile2:
                    _SetFrame<ViewProfileDetailPreferences>(ViewProfileDetailPreferences.XmlPreferenceId, ProfileSelection.Profile2.ToString());
                    break;
                case Resource.Id.nav_profile3:
                    _SetFrame<ViewProfileDetailPreferences>(ViewProfileDetailPreferences.XmlPreferenceId, ProfileSelection.Profile3.ToString());
                    break;
                case Resource.Id.nav_profile4:
                    _SetFrame<ViewProfileDetailPreferences>(ViewProfileDetailPreferences.XmlPreferenceId, ProfileSelection.Profile4.ToString());
                    break;
                case Resource.Id.nav_profile5:
                    _SetFrame<ViewProfileDetailPreferences>(ViewProfileDetailPreferences.XmlPreferenceId, ProfileSelection.Profile5.ToString());
                    break;
                case Resource.Id.nav_profile6:
                    _SetFrame<ViewProfileDetailPreferences>(ViewProfileDetailPreferences.XmlPreferenceId, ProfileSelection.Profile6.ToString());
                    break;
                case Resource.Id.nav_profile7:
                    _SetFrame<ViewProfileDetailPreferences>(ViewProfileDetailPreferences.XmlPreferenceId, ProfileSelection.Profile7.ToString());
                    break;
                case Resource.Id.nav_profile8:
                    _SetFrame<ViewProfileDetailPreferences>(ViewProfileDetailPreferences.XmlPreferenceId, ProfileSelection.Profile8.ToString());
                    break;
                default:
                    Toast.MakeText(ApplicationContext, "Invalid Menu Item", 0).Show();
                    return false;
            }

            //Closing drawer on item click
            mDrawerLayout.CloseDrawer(GravityCompat.Start);

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

        private void RefreshFromDevice()
        {
            m_ViewModel.ReadConfigurationFromDevice();

            m_txtDeviceName.Text = m_ViewModel.DeviceName;
            m_txtFwVer.Text = m_ViewModel.FwVer;
            m_txtBuild.Text = m_ViewModel.Build;
            m_txtHwVer.Text = m_ViewModel.HwVer;
        }

        private void UploadToDevice()
        {  
            try
            {
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

