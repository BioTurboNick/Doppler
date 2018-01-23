namespace Doppler.ViewModels
{
    /// <summary>
    /// Provides a base class for ViewModels that are mapped to an underlying Model,
    /// allowing implicit conversion from ViewModel to Model.
    /// </summary>
    /// <typeparam name="T">The type of the underlying Model.</typeparam>
    public class ViewModel<T> : ViewModel
        where T : class, new()
    {
        protected T Model { get; }

        /// <summary>
        /// Provides the underlying Model for this ViewModel.
        /// </summary>
        /// <param name="viewModel">The ViewModel.</param>
        public static implicit operator T(ViewModel<T> viewModel) =>
            viewModel?.Model;

        /// <summary>
        /// Creates a new ViewModel around a new Model or around an existing Model.
        /// </summary>
        /// <param name="model">The Model to build the ViewModel around.</param>
        public ViewModel(T model = null)
        {
            Model = model ?? new T();
        }
    }
}
