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

namespace System.Windows.Forms
{
    public abstract class FileDialog : IDisposable
    {
        public String Filter { get; set; }

        public string FileName { get; set; }

        public DialogResult ShowDialog()
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // Pour d�tecter les appels redondants

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'�tat manag� (objets manag�s).
                }

                // TODO: lib�rer les ressources non manag�es (objets non manag�s) et remplacer un finaliseur ci-dessous.
                // TODO: d�finir les champs de grande taille avec la valeur Null.

                disposedValue = true;
            }
        }

        // TODO: remplacer un finaliseur seulement si la fonction Dispose(bool disposing) ci-dessus a du code pour lib�rer les ressources non manag�es.
        // ~FileDialog() {
        //   // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
        //   Dispose(false);
        // }

        // Ce code est ajout� pour impl�menter correctement le mod�le supprimable.
        public void Dispose()
        {
            // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
            Dispose(true);
            // TODO: supprimer les marques de commentaire pour la ligne suivante si le finaliseur est remplac� ci-dessus.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }

    public class SaveFileDialog : FileDialog
    {
      
    }

    public class OpenFileDialog : FileDialog
    {

    }
}