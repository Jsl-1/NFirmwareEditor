using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;

namespace NToolbox
{
    internal class MainMenuActionBarDrawerToggle : ActionBarDrawerToggle
    {
        AppCompatActivity owner;
        V7Toolbar m_toolbar;

        public MainMenuActionBarDrawerToggle(AppCompatActivity activity,  DrawerLayout layout, V7Toolbar toolbar, int openRes, int closeRes)
            : base(activity, layout, toolbar, openRes, closeRes)
        {
            owner = activity;
            m_toolbar = toolbar;
        }

        public override void OnDrawerClosed(View drawerView)
        {
            base.OnDrawerClosed(drawerView);
        }

        public override void OnDrawerOpened(View drawerView)
        {
            //m_toolbar.Menu.GetItem()

            base.OnDrawerOpened(drawerView);
        }
    }   
}