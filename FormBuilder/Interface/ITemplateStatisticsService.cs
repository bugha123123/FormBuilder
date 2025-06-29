namespace FormBuilder.Interface
{
    public interface ITemplateStatisticsService
    {
        Task<double> GetAverageLikesPerTemplateAsync();
        Task<Dictionary<string, int>> GetLikesHistogramAsync();
        Task<int> GetTimesUsedAsync(int templateId);
        Task<double> GetCompletionRateAsync(int templateId);
    }
}
