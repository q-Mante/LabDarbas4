namespace LabDarbas4_19.App_Class
{
    /// <summary>
    /// Interface for objects that can be merged with another instance of the same type.
    /// </summary>
    /// <typeparam name="T">Type of objects that can be merged.</typeparam>
    public interface IMergable<T>
    {
        /// <summary>
        /// Determines whether the current instance is the same as another instance of the same type.
        /// </summary>
        /// <param name="other">The other instance to compare with the current instance.</param>
        /// <returns>True if the two instances are considered the same, otherwise false.</returns>
        bool Same(T other);

        /// <summary>
        /// Merges the data from another instance of the same type into the current instance.
        /// </summary>
        /// <param name="other">The other instance to merge into the current instance.</param>
        void Merge(T other);
    }
}