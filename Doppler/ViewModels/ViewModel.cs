using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Doppler.ViewModels
{
    /// <summary>
    /// Provides a base class from which all ViewModels must derive, implementing INotifyPropertyChanged support.
    /// </summary>
    public abstract class ViewModel :
        INotifyPropertyChanged
    {
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        public ViewModel()
        {
            PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Set a property via its backing field and notify of a change.
        /// </summary>
        /// <typeparam name="T">The type of the field.</typeparam>
        /// <param name="field">The field reference.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="propertyName">The name of the property, populated automatically.</param>
        /// <returns><c>True</c> if a change occurred, otherwise <c>false</c>.</returns>
        protected bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null) // note: this structure is confusing, since one might naturally call one with a substitution for the last term
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            RaisePropertyChanged(propertyName);

            return true;
        }
        
        /// <summary>
        /// Sets an underlying property through its getter and setter and notify of a change.
        /// </summary>
        /// <typeparam name="T">the type of the property.</typeparam>
        /// <param name="getter">The underlying property's getter.</param>
        /// <param name="setter">The underlying property's setter.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="propertyNames">The names of the properties affected by the change.</param>
        /// <returns><c>True</c> if a change occurred, otherwise <c>false</c>.</returns>
        /// <remarks>
        /// Primary use is to set a property on a Model or Data object from a ViewModel property.
        /// Used when a property's change affects other properties.
        /// </remarks>
        protected bool Set<T>(Func<T> getter, Action<T> setter, T value, [CallerMemberName] string propertyName = null)
        {
            //Contract.Requires(getter != null);
            //Contract.Requires(setter != null);

            if (EqualityComparer<T>.Default.Equals(getter.Invoke(), value))
                return false;

            setter.Invoke(value);
            RaisePropertyChanged(propertyName);

            return true;
        }
        
        /// <summary>
        /// Fired when a property on this object has been modified.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event for the given property name.
        /// </summary>
        /// <param name="property">The name of the property.</param>
        protected void RaisePropertyChanged(string property) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e) { }
    }
}
