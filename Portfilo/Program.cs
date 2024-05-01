using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Portfilo.Data;
using Portfilo.Models;
using Portfilo.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(x => x.EnableEndpointRouting = false);
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IRepository<Menu>, MenuRepository>();
builder.Services.AddScoped<IRepository<Header>, HeaderRepository>();
builder.Services.AddScoped<IRepository<About>, AboutRepository>();
builder.Services.AddScoped<IRepository<SocialMedia>, SocialMediaRepository>();
builder.Services.AddScoped<IRepository<Experience>, ExperienceRepository>();
builder.Services.AddScoped<IRepository<Education>, EducationRepository>();
builder.Services.AddScoped<IRepository<ProfessionalSkill>, ProfessionalSkillRepository>();
builder.Services.AddScoped<IRepository<Language>, LanguageRepository>();
builder.Services.AddScoped<IRepository<Project>, ProjectRepository>();
builder.Services.AddScoped<IRepository<CallToAction>, CallToActionRepository>();
builder.Services.AddScoped<IRepository<TransactionContactUs>, ContactUsRepository>();

builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = $"/Admin/Account/Login";
});


var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{

	endpoints.MapAreaControllerRoute(
		name: "areas",
		areaName: "Admin",
		pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");

	

});
//app.UseEndpoints(endpoints =>
//{


//	app.MapControllerRoute(
//			name: "areas",
//			pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//			);

//	app.MapControllerRoute(
//			name: "default",
//			pattern: "{controller=Home}/{action=Index}/{id?}"
//			);

//});

app.Run();
