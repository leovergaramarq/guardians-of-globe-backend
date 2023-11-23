using GuardiansOfGlobeApi.Interfaces;
using GuardiansOfGlobeApi.Services;

namespace GuardiansOfGlobeApi.Endpoints
{
    public class AlterEgoEndpoints
    {
        public void Init(IServiceCollection services)
        {
            services.AddTransient<IAlterEgoService, AlterEgoService>();
            services.AddMvc().AddRazorPagesOptions(options => {
                options.Conventions.AddPageRoute("", "");
            });
        }
    }
}
