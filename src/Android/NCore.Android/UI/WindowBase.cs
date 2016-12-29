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
using System.Windows.Forms;
using System.Drawing;

namespace NCore.UI
{
    public class EditorDialogWindow : WindowBase { }

    public class WindowBase : Control
    {
        public DialogResult DialogResult { get; set; }

        public SizeF AutoScaleDimensions { get; set; }

        public FormStartPosition StartPosition { get; set; }

        public FormBorderStyle FormBorderStyle { get; set; }

        public string Text { get; set; }

        public Size ClientSize { get; set; }

        public bool MinimizeBox { get; set; }

        public bool MaximizeBox { get; set; }

        public void UpdateUI()
        {

        }

        protected void UpdateUI(Action action, bool supressExceptions = true)
        {
            if (!supressExceptions)
            {
                Invoke(action);
            }
            else
            {
                try
                {
                    Invoke(action);
                }
                catch (Exception)
                {
                    // Ignore
                }
            }
        }

        public void Invoke(Action action)
        {
            Handler mainHandler = new Handler(Application.Context.MainLooper);
            mainHandler.Post(new RunnableAction(action));
        }

        public DialogResult ShowDialog()
        {
            return DialogResult.Cancel;
        }

        public class RunnableAction : Java.Lang.IRunnable
        {
            private Action m_Action;

            public RunnableAction(Action action)
            {
                m_Action = action;
            }

            public IntPtr Handle
            {
                get
                {
                   return (IntPtr)0;
                }
            }

            public void Dispose()
            {
                m_Action = null;
            }

            public void Run()
            {
                m_Action.Invoke();
            }
        }
    }
}