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
        MainActivity owner;

        public MainMenuActionBarDrawerToggle(MainActivity activity,  DrawerLayout layout, V7Toolbar toolbar, int openRes, int closeRes)
            : base(activity, layout, toolbar, openRes, closeRes)
        {
            owner = activity;           
        }

        public override void OnDrawerClosed(View drawerView)
        {
            base.OnDrawerClosed(drawerView);
        }

        public override void OnDrawerOpened(View drawerView)
        {
            base.OnDrawerOpened(drawerView);
        }
    }   
}