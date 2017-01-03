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
using NToolbox.ViewModels;

namespace NToolbox.UI
{
    public abstract class FragmentBase : Fragment
    {       
        public abstract Int32 LayoutId { get; }

        protected View m_View;


        protected ArcticFoxConfigurationViewModel ViewModel
        {
            get
            {
                return ArcticFoxConfigurationViewModel.Instance;
            }
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            m_View = inflater.Inflate(LayoutId, container, false);
            InitializeControls();
            SetValuesToControls();

            return m_View;
        }

        public override void OnAttach(Context context)
        {
            base.OnAttach(context);
            if (m_View != null)
            {
                SetValuesToControls();
            }
        }

        public override void OnDetach()
        {
            base.OnDetach();
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            if (m_View != null)
            {
                SetValuesToControls();
            }
        }

        protected abstract void InitializeControls();

        protected abstract void SetValuesToControls();

        public override void OnResume()
        {
            base.OnResume();
            if (m_View != null)
            {
                SetValuesToControls();
            }
        }
    }
}