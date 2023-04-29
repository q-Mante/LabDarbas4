namespace LabDarbas4_19.App_Class
{
    /// <summary>
    /// Interface for a filter that determines whether a given criteria passes or not.
    /// </summary>
    /// <typeparam name="ICriteria">The type of the criteria to be filtered.</typeparam>
    public interface IFilter<ICriteria>
    {
        /// <summary>
        /// Determines whether the given criteria passes the filter.
        /// </summary>
        /// <param name="criteria">The criteria to be filtered.</param>
        /// <returns>True if the criteria passes the filter, otherwise false.</returns>
        bool Pass(ICriteria criteria);
    }
}