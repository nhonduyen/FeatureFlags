using Microsoft.FeatureManagement;

namespace FeatureFlags.FeatureFilters
{
    [FilterAlias(nameof(LanguageFilter))]
    public class LanguageFilter : IFeatureFilter
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public LanguageFilter(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var userLanguage = _contextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString();
            var settings = context.Parameters.Get<LanguageFilterSettings>();

            return Task.FromResult(settings.AllowedLanguages.Any(a => userLanguage.Contains(a)));
        }
    }
}
