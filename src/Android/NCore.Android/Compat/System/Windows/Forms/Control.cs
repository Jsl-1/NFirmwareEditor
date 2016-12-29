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
using System.Drawing;
using System.ComponentModel;
using NCore.Compat;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms
{
    public class Control : IDisposable, System.ComponentModel.ISupportInitialize, IWrappableObject<Control>
    { 
        public virtual IntPtr Handle { get; }

        public virtual String Name { get; set; }

        public virtual Int32 TabIndex { get; set; }

        public bool TabStop { get; set; }

        public virtual Font Font { get; set; }
                
        public virtual Color ForeColor { get; set; } = Color.White;
                
        public virtual Color BackColor { get; set; } = Color.Black;
                
        public virtual Rectangle DisplayRectangle { get; }
                
        public virtual Padding Padding { get; set; } = new Padding(0);

        public virtual DockStyle Dock { get; set; }

        public virtual int Width { get; set; }
                
        public virtual int Height { get; set; }
                
        public virtual Boolean Visible { get; set; }

        public virtual Boolean AutoSize { get; set; }

        public virtual Size Size { get; set; } = new Size(0, 0);

        public virtual Point Location { get; set; } = new Point(0, 0);

        public virtual object Tag { get; set; }

        public virtual Boolean Enabled { get; set; }

        public AutoScaleMode AutoScaleMode { get; set; }

        public AnchorStyles Anchor { get; set; }

        public virtual Rectangle ClientRectangle { get; set; } = new Rectangle(0, 0, 0, 0);

        public List<Control> Controls { get; } = new List<Control>();

        public bool IsWrapped
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected virtual void OnPaint(PaintEventArgs e) { }

        public void Invalidate()
        {

        }

        public void SuspendLayout()
        {

        }

        public void PerformLayout()
        {

        }

        public void BeginUpdate()
        {

        }

        public void EndUpdate()
        {

        }

        public void ResumeLayout(bool force)
        {
          
        }

        protected void SetStyle(ControlStyles allPaintingInWmPaint, bool v)
        {

        }


        protected virtual void OnSizeChanged(EventArgs e)
        {

        }

        protected virtual void OnScroll(ScrollEventArgs se)
        {

        }

        public event EventHandler Click;
        public event EventHandler Shown;
        public event EventHandler Load;
        public event MouseEventHandler MouseMove;
        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseUp;

        #region IDisposable Support
        private bool disposedValue = false; // Pour détecter les appels redondants

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés).
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous.
                // TODO: définir les champs de grande taille avec la valeur Null.

                disposedValue = true;
            }
        }

        // TODO: remplacer un finaliseur seulement si la fonction Dispose(bool disposing) ci-dessus a du code pour libérer les ressources non managées.
        // ~Control() {
        //   // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
        //   Dispose(false);
        // }

        // Ce code est ajouté pour implémenter correctement le modèle supprimable.
        public void Dispose()
        {
            // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
            Dispose(true);
            // TODO: supprimer les marques de commentaire pour la ligne suivante si le finaliseur est remplacé ci-dessus.
            // GC.SuppressFinalize(this);
        }

        void ISupportInitialize.BeginInit()
        {

        }

        void ISupportInitialize.EndInit()
        {

        }

        public void SetPropertyValue(object value, [CallerMemberName] string propertyName = null)
        {
            throw new NotImplementedException();
        }


        public T GetPropertyValue<T>([CallerMemberName] string propertyName = null)
        {
            throw new NotImplementedException();
        }

        public IWrappableObject<Control> WrapTo<TTarget>(TTarget destination)
        {
            throw new NotImplementedException();
        }

        public IWrappableObject<Control> WrapProperty<TTarget>(Expression<Func<Control, object>> sourceProperty, Expression<Func<TTarget, object>> targetProperty)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}